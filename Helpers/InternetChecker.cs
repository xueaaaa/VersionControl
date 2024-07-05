#pragma warning disable SYSLIB0014
using System.Net;

namespace VersionControl.Helpers
{
    /// <summary>
    /// A class that checks if the device is connected to the Internet
    /// </summary>
    internal static class InternetChecker
    {
        internal enum ConnectionStatus
        {
            NotConnected,
            LimitedAccess,
            Connected
        }

        internal static ConnectionStatus Check()
        {
            try
            {
                IPHostEntry entry = Dns.GetHostEntry("dns.msftncsi.com");
                if (entry.AddressList.Length == 0)
                    return ConnectionStatus.NotConnected;
                else if (!entry.AddressList[0].ToString().Equals("131.107.255.255"))
                    return ConnectionStatus.LimitedAccess;
            }
            catch
            {
                return ConnectionStatus.NotConnected;
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://www.msftncsi.com/ncsi.txt");
            try
            {
                HttpWebResponse responce = (HttpWebResponse)request.GetResponse();

                if (responce.StatusCode != HttpStatusCode.OK)
                    return ConnectionStatus.LimitedAccess;

                using StreamReader sr = new(responce.GetResponseStream());
                if (sr.ReadToEnd().Equals("Microsoft NCSI"))
                    return ConnectionStatus.Connected;
                else
                    return ConnectionStatus.LimitedAccess;
            }
            catch
            {
                return ConnectionStatus.NotConnected;
            }
        }
    }
}
#pragma warning restore SYSLIB0014