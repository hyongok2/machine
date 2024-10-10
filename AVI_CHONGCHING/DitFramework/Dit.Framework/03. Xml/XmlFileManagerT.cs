using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;

namespace Dit.Framework.Xml
{
    public static class XmlFileManager<TState> where TState : class
    {
        // 정적 메서드
        public static bool TrySaveXml(string xmlFilePath, TState data)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TState));
                using (TextWriter textWriter = new StreamWriter(xmlFilePath))
                {
                    xmlSerializer.Serialize(textWriter, data);
                    textWriter.Close();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool TryLoadData(string xmlFilePath, out TState data)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TState));
                string temp = null;
                Stream streamTest = null;
                using (TextReader textReader = new StreamReader(xmlFilePath))
                {
                    temp = textReader.ReadToEnd();
                    if (temp.IndexOf("&") > 0)
                    {
                        temp = Regex.Replace(temp, @"&amp;", "", RegexOptions.ExplicitCapture);  //특수문자중 &가 있는경우 못읽어옴, &는 공백 처리
                        temp = Regex.Replace(temp, @"[&]", "", RegexOptions.ExplicitCapture);    //특수문자중 &가 있는경우 못읽어옴, &는 공백 처리
                        streamTest = Convertfile(temp, streamTest);

                        data = (TState)xmlSerializer.Deserialize(streamTest);                   //Deserialize 하기위한 변환
                        textReader.Close();
                    }
                    else
                    {
                        streamTest = Convertfile(temp, streamTest);
                        data = (TState)xmlSerializer.Deserialize(streamTest);                   //TextRead 2번 수행시 미작동. 미작동 방지 위한 변환
                        textReader.Close();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                data = null;
                return false;
            }
        }

        // 정적 메서드
        public static bool TrySaveToXmlText(TState data, out string text)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TState));
                using (StringWriter sw = new StringWriter())
                {
                    xmlSerializer.Serialize(sw, data);
                    text = sw.ToString();
                }

                return true;
            }
            catch (Exception)
            {
                text = null;

                return false;
            }
        }
        public static bool TryLoadFromXmlText(string text, out TState data)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(TState));
                using (StringReader sr = new StringReader(text))
                {
                    data = (TState)xmlSerializer.Deserialize(sr);
                }

                return true;
            }
            catch (Exception)
            {
                data = null;
                return false;
            }
        }



        // 정적 메서드
        public static byte[] SerializeBin(TState data)
        {
            BinaryFormatter binFmt = new BinaryFormatter();
            using (MemoryStream fs = new MemoryStream())
            {
                binFmt.Serialize(fs, data);
                return fs.ToArray();
            }
        }
        public static TState DeserializeBin(byte[] bin)
        {
            TState p;
            BinaryFormatter binFmt = new BinaryFormatter();
            using (MemoryStream rdr = new MemoryStream(bin, false))
            {
                p = (TState)binFmt.Deserialize(rdr);
            }
            return p;
        }
        public static Stream Convertfile(string test, Stream data)
        {
            byte[] ByteArray = Encoding.UTF8.GetBytes(test);
            MemoryStream Stream1 = new MemoryStream(ByteArray);

            return Stream1;

        }
    }
}