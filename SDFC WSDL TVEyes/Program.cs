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
            string strUseRT = "";
            // get the correct record type and build query
            force.QueryResult qrCaseRT = forceTools.RunQuery("SELECT id FROM recordtype where name = 'Provisioning'");
            if(qrCaseRT.records.Length > 0)
            {
                strUseRT = qrCaseRT.records[0].Id;
            }
            else
            {
                throw new Exception("Could not find the specified Record Type");
            }
            // get cases
            force.QueryResult qrCases = forceTools.RunQuery("SELECT id FROM case WHERE recordtypeid = '" + strUseRT + "'");
            // process
            force.sObject[] cases = qrCases.records;
            // update cases

            return result;
        }
    }

}
