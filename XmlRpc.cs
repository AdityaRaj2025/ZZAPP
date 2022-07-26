using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace SSAPP.XmlRpc
{
    internal class XmlRpc
    {
        private class AsyncObject
        {
            public HttpWebRequest Request;
            public byte[] Data;
            public Exception Exception;
            public ManualResetEvent Finished = new ManualResetEvent(false);

            public AsyncObject(HttpWebRequest req, byte[] buf)
            {
                Request = req;
                Data = buf;
            }

            public AsyncObject(HttpWebRequest req)
            {
                Request = req;
            }
        }

        private static Dictionary<string, string> Headers = new Dictionary<string, string>();

        public static XmlRpcData[] Execute(string target, int timeout, string methodname, params XmlRpcData[] @params)
        {
            HttpWebRequest req;
            byte[] sendbuf;

            req = (HttpWebRequest)HttpWebRequest.Create(target);
            req.Method = "POST";
            req.ContentType = "text/xml";

            try
            {
                foreach (var kvp in Headers)
                {
                    req.Headers[kvp.Key] = kvp.Value;
                }

                sendbuf = XmlRpc.GenerateRequestBody(methodname, @params);
            }
            catch (Exception ex)
            {
                throw new XmlRpcException("An exception occurred in the creation of the request. (XmlRpc.Execute/Gen)", ex);
            }

            try
            {
                var sendstr = Encoding.UTF8.GetString(sendbuf);
                AsyncObject ao = new AsyncObject(req, sendbuf);
                var ar = req.BeginGetRequestStream(WriteAsync, ao);
                if (!ar.AsyncWaitHandle.WaitOne(timeout, false))
                    throw new XmlRpcException("Request transmission timed out.");
                if (!ao.Finished.WaitOne(timeout, false))
                    throw new XmlRpcException("Request data write timed out.");
                if (ao.Exception != null)
                    throw new XmlRpcException("An exception occurred in sending the request. (XmlRpc.Execute/Write)", ao.Exception);
            }
            catch (XmlRpcException _ex)
            {
                Console.WriteLine(_ex);
                throw;
            }
            catch (Exception ex)
            {
                throw new XmlRpcException("An exception occurred in sending the request. (XmlRpc.Execute/Req)", ex);
            }

            byte[] recvbuf;
            try
            {
                AsyncObject ao = new AsyncObject(req);
                var ar = req.BeginGetResponse(ReadAsync, ao);
                if (!ao.Finished.WaitOne(timeout, false))
                    throw new XmlRpcException("Receipt of response timed out.");
                if (ao.Exception != null)
                    throw new XmlRpcException("An exception occurred in receiving the response.(XmlRpc.Execute/Read)", ao.Exception);
                recvbuf = ao.Data;
                Console.WriteLine("Show the value of recvbuf ####################################", "\n");
                Console.WriteLine(Encoding.UTF8.GetString(recvbuf));

            }
            catch (XmlRpcException _ex)
            {
                Console.WriteLine(_ex);
                throw;
            }
            catch (Exception ex)
            {
                throw new XmlRpcException("An exception occurred in receiving the response.(XmlRpc.Execute/Res)", ex);
            }

            try
            {
                return ParseResponseBody(recvbuf);
            }
            catch (XmlRpcException _ex)
            {
                Console.WriteLine(_ex);
                throw;
            }
            catch (Exception ex)
            {
                throw new XmlRpcException("An exception occurred in processing the response.(XmlRpc.Execute/Parse)", ex);
            }
        }

        public static byte[] GenerateRequestBody(string methodname, params XmlRpcData[] @params)
        {
            XmlDocument doc = new XmlDocument();

            var methodroot = doc.CreateElement("methodCall");
            var namenode = doc.CreateElement("methodName");
            namenode.InnerText = methodname;
            methodroot.AppendChild(namenode);

            if (@params.Length > 0)
            {
                var paramsnode = doc.CreateElement("params");
                foreach (XmlRpcData p in @params)
                {
                    var param = doc.CreateElement("param");
                    var value = doc.CreateElement("value");
                    param.AppendChild(value);
                    p.WriteTo(doc, value);
                    paramsnode.AppendChild(param);
                }
                methodroot.AppendChild(paramsnode);
            }
            doc.AppendChild(methodroot);

            using (MemoryStream ms = new MemoryStream())
            {
                using (XmlTextWriter xw = new XmlTextWriter(ms, new UTF8Encoding(false)))
                {
                    doc.Save(xw);
                }
                return ms.ToArray();
            }
        }

        public static XmlRpcData[] ParseResponseBody(byte[] buf)
        {
            XmlDocument doc = new XmlDocument();

            using (MemoryStream ms = new MemoryStream(buf))
            {
                doc.Load(ms);
            }

            var responseroot = doc.SelectSingleNode("/methodResponse");
            var fault = responseroot["fault"];
            if (fault != null)
            {
                var faultdata = (XmlRpcStruct)XmlRpcData.ReadValue(fault["value"]);
                var code = int.Parse(faultdata["faultCode"].Value);
                var str = faultdata["faultString"].Value;

                throw new XmlRpcFaultException(code, str);
            }

            List<XmlRpcData> ret = new List<XmlRpcData>();
            var @params = responseroot.SelectNodes("/methodResponse/params/param");
            foreach (XmlNode param in @params)
                ret.Add(XmlRpcData.ReadValue(param.FirstChild));

            return ret.ToArray();
        }

        private static void WriteAsync(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;
            HttpWebRequest req = ao.Request;

            try
            {
                //Logger.WriteDump("SEND:", ao.Data);
                using (var st = req.EndGetRequestStream(ar))
                {
                    st.Write(ao.Data, 0, ao.Data.Length);
                }
            }
            catch (Exception ex)
            {
                ao.Exception = ex;
            }

            ao.Finished.Set();
        }

        private static void ReadAsync(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;
            HttpWebRequest req = ao.Request;

            try
            {
                HttpWebResponse res = (HttpWebResponse)req.EndGetResponse(ar);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (var st = res.GetResponseStream())
                    {
                        var readbuf = new byte[4096];
                        int bytes;

                        while(true)
                        {
                            bytes = st.Read(readbuf, 0, readbuf.Length);
                            if (bytes == 0)
                                break;
                            ms.Write(readbuf, 0, bytes);
                        }
                    }

                    ao.Data = ms.ToArray();
                    
                }
                //Logger.WriteDump("RECV:", ao.Data);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    using (StreamReader sr = new StreamReader(ex.Response.GetResponseStream()))
                    {
                        //Logger.Write("RECV: {0}", sr.ReadToEnd());
                    }
                }
                ao.Exception = ex;
            }
            catch (Exception ex)
            {
                ao.Exception = ex;
            }

            ao.Finished.Set();
        }

        public static void SetHeader(string name, string value)
        {
            if (value != null)
            {
                Headers[name] = value;
            }
            else if (Headers.ContainsKey(name))
            {
                Headers.Remove(name);
            }
        }
    }
}
