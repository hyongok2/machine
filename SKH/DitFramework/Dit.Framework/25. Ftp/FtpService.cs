using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;

namespace Dit.Framework.Ftp
{
    public class FtpService
    {
        public string FtpUser = "";
        public string FtpPassword = "";
        public string HostIp = "127.0.0.1";
        public int HostPort = 0;
        public bool IsPassveMode { get; set; }

        private const int BufferSize = 2048;

        public FtpService(string ftpUser, string ftpPassword, string hostIp, bool isPassive = true)
        {
            this.FtpUser = ftpUser;
            this.FtpPassword = ftpPassword;
            this.HostIp = hostIp;
            this.HostPort = 21;
            this.IsPassveMode = isPassive;
        }

        #region FtpFileDownLoad
        public bool FtpFileDownLoad(string localFilepath, string ftpFilepath, string fileName)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}/{1}/{2}", this.HostIp, ftpFilepath, fileName)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                using (Stream ftpStream = response.GetResponseStream())
                {
                    using (FileStream fileStream = new FileStream(localFilepath, FileMode.Create))
                    {
                        byte[] byteBuffer = new byte[BufferSize];
                        int bytesRead = ftpStream.Read(byteBuffer, 0, BufferSize);
                        while (bytesRead != 0)
                        {
                            fileStream.Write(byteBuffer, 0, bytesRead);
                            bytesRead = ftpStream.Read(byteBuffer, 0, BufferSize);
                        }
                        fileStream.Close();
                    }
                    ftpStream.Close();
                }
                response.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region Ftp File Delete
        public bool FtpFileDelete(string localFilepath, string ftpFilepath)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}/{1}/{2}", this.HostIp, ftpFilepath, localFilepath)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();

                string result = string.Empty;
                using (Stream ftpStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(ftpStream))
                    {
                        result = reader.ReadToEnd();
                        reader.Close();
                    }
                    ftpStream.Close();
                }
                response.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion
        #region FtpFileUpload
        public bool FtpFileUpload(string ftpFilePath, string localFileFullPath, string fileName)
        {
            IPStatus ipstatus = PingTest(HostIp);
            if (ipstatus == IPStatus.Success)
            {
                bool dirExist = DirExist(ftpFilePath + "/");
                if (!dirExist) CreatDirectory(ftpFilePath);

                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}/{2}/{3}", this.HostIp, this.HostPort, ftpFilePath, fileName)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;

                using (Stream ftpStream = ftpRequest.GetRequestStream())
                {
                    using (FileStream localFileStream = new FileStream(localFileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        byte[] byteBuffer = new byte[BufferSize];
                        int bytesRead = localFileStream.Read(byteBuffer, 0, BufferSize);

                        while (bytesRead != 0)
                        {
                            ftpStream.Write(byteBuffer, 0, bytesRead);
                            bytesRead = localFileStream.Read(byteBuffer, 0, BufferSize);
                        }
                        localFileStream.Close();
                    }
                    ftpStream.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
        #region Directory Get
        public IList<string> GetDirectoryList(string directory)
        {
            try
            {
                List<string> list = new List<string>();
                string line = null;
                FtpWebRequest ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, directory)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                StreamReader reader = new StreamReader(ftpResponse.GetResponseStream(), System.Text.Encoding.Default);

                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }
                ftpResponse.Close();

                return list;
            }
            catch (Exception)
            {
                return new List<string>();
            }
        }
        public bool DirExist(string directory)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, directory)));
                request.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                request.UseBinary = true;
                request.UsePassive = IsPassveMode;
                request.KeepAlive = true;
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {

                    // Okay.   
                    return true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        // Directory not found.   

                    }
                }
                return false;
            }

        }
        #endregion
        #region CheckFtpDir
        public void CreatDirectory(string directory)
        {
            try
            {
                string[] dirList = directory.Replace("//", "/").Replace('\\', '/').Split('/');
                string curDir = "";
                for (int i = 0; i < dirList.Length; i++)
                {
                    curDir += string.Format("{0}/", dirList[i]);
                    bool dirExist = DirExist(curDir);
                    if (!dirExist)
                    {
                        FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, curDir)));
                        ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                        ftpRequest.UseBinary = true;
                        ftpRequest.UsePassive = IsPassveMode;
                        ftpRequest.KeepAlive = true;
                        ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                        ftpRequest.Timeout = 5000;
                        FtpWebResponse ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                        ftpResponse.Close();
                    }
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
            }
        }
        #endregion
        #region PingTest
        private IPStatus PingTest(string hostip)
        {
            //if (string.IsNullOrEmpty(hostip))
            //{
            //    return false;
            //}

            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 5;
            string[] hostIp = hostip.Split(':');

            PingReply reply = pingSender.Send(hostIp[0], timeout, buffer, options);

            return reply.Status;
        }
        #endregion

        public bool IsValidConnection(string url)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, url)));
                request.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                request.UseBinary = true;
                request.UsePassive = IsPassveMode;
                request.KeepAlive = true;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }


        public bool FileExists(string file)
        {
            try
            {
                string dirList = file.Replace("//", "/").Replace('\\', '/');
                long fileSize = 0;
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, dirList)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpRequest.Timeout = 5000;

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string fileSizeString = streamReader.ReadToEnd();  // 

                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();

                return true;
            }
            catch (WebException ex)
            {
                return false;
            }
        }



        public long GetFileSize(string file)
        {
            try
            {
                string dirList = file.Replace("//", "/").Replace('\\', '/');
                long fileSize = 0;
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("ftp://{0}/{2}", this.HostIp, this.HostPort, dirList)));
                ftpRequest.Credentials = new NetworkCredential(this.FtpUser, this.FtpPassword);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = IsPassveMode;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                ftpRequest.Timeout = 5000;

                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                Stream ftpStream = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string fileSizeString = streamReader.ReadToEnd();  // 

                fileSize = response.ContentLength;

                ftpStream.Close();
                response.Close();

                return fileSize;
            }
            catch (WebException ex)
            {
                return -1;
            }
        }
    }
}
