using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;
using System.Net;

namespace SDFC_WSDL_TVEyes
{
    class Program
    {
        static private string sessionId;
        static private string serverURL;
        static void Main(string[] args)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                bool success = ProcessCases();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        static bool ProcessCases()
        {
            bool result = true;

            // make the contection
            force.LoginResult lr = new force.LoginResult();
            using (force.SoapClient sc = new force.SoapClient()) {
                if (sessionId == null && serverURL == null)
                {
                    lr = sc.login(null, "[username]", "[password]" + "[security token]");
                    // TODO: check for expired

                    // store the id
                    sessionId = lr.sessionId.ToString().Trim();
                    serverURL = lr.serverUrl.ToString().Trim();
                }
            }

            
            
            force.SessionHeader sheader = new force.SessionHeader();
            sheader.sessionId = sessionId;

            // get cases
            force.QueryResult qrCases = new force.QueryResult();
            using (force.SoapClient sc = new force.SoapClient())
            {
                sc.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverURL);
                sc.query(sheader, null, null, null, "SELECT id FROM case WHERE recordtypeid = '012R00000009UE9IAM'", out qrCases);
            }
            // process
            force.sObject[] cases = qrCases.records;
            // update cases

            return result;
        }
    }

}
