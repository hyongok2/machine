using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dit.Framework.Comm
{
    public class ComResult
    {
        public int Code { get; private set; }
        public string Description { get; private set; }

        public static ComResult Success = new ComResult(0, "Success");
        public static ComResult Fail = new ComResult(-1, "Fail ");
        public static ComResult Null = new ComResult(-9999, "Null");

        public static ComResult PLCError(int errorCode)
        {
            return new ComResult(errorCode, "PLC Error");
        }
        public static ComResult NotConnected(string format, params object[] args)
        {
            return new ComResult(-10000, string.Format(format, args));
        }
        public static ComResult UndefinedItem(string format, params object[] args)
        {
            return new ComResult(-10001, string.Format(format, args));
        }
        public static ComResult Exception(Exception ex)
        {
            return new ComResult(-10002, ex.Message);
        }
        public static ComResult OperationFailed(string format, params object[] args)
        {
            return new ComResult(-10003, string.Format(format, args));
        }
        public static ComResult ReplyTimeout(string format, params object[] args)
        {
            return new ComResult(-10004, string.Format(format, args));
        }

        protected ComResult(int code, string description)
        {
            this.Code = code;
            this.Description = description;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is ComResult)
            {
                ComResult result = (ComResult)obj;
                return (this.Code == result.Code && this.Description == result.Description);
            }

            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("0x{0:X}({1})", this.Code, this.Description);
        }

        public static implicit operator bool(ComResult result)
        {
            try
            {
                return result == ComResult.Success;
            }
            catch
            {
                throw;
            }
        }
    }
}
