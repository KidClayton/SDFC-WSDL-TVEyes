using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDFC_WSDL_TVEyes
{
    class forceTools
    {
        static private string sessionId;
        static private string serverURL;
        static private force.SessionHeader sheader;

        private static void ConnectToSFDC()
        {
            // make the contection
            force.LoginResult lr = new force.LoginResult();
            using (force.SoapClient sc = new force.SoapClient())
            {
                if (sessionId == null && serverURL == null)
                {
                    lr = sc.login(null, "username", "pasword" + "security token");
                    // TODO: check for expired

                    // store the id
                    sessionId = lr.sessionId.ToString().Trim();
                    serverURL = lr.serverUrl.ToString().Trim();
                }
            }
            sheader = new force.SessionHeader();
            sheader.sessionId = sessionId;
        }

        public static force.QueryResult RunQuery(string Query)
        {
            forceTools.ConnectToSFDC();
            force.QueryResult result = new force.QueryResult();
            using (force.SoapClient sc = new force.SoapClient())
            {
                sc.ChannelFactory.Endpoint.Address = new System.ServiceModel.EndpointAddress(serverURL);
                sc.query(sheader, null, null, null, Query, out result);
            }
            return result;
        }
    }
}
