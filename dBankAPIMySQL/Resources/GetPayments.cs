using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace dBankAPIMySQL.Resources
{
    public class GetPayments
    {
        public string JsonString { get; set; }
        public AllPayments AllPayments { get; set; }
        public GetPayments()
        {
            DownloadAndDeserializeString();
        }
        private void DownloadAndDeserializeString()
        {
            
            try
            {
                using (var webClient = new System.Net.WebClient())
                {
                    JsonString = webClient.DownloadString($"https://jrozliczajaca.herokuapp.com/przelewy?nr_banku=26124027600000000000000000");

                    AllPayments = JsonConvert.DeserializeObject<AllPayments>(JsonString);
                    
                    Console.WriteLine();
                }


            }
            catch
            {
                Console.WriteLine("Error while downloading data from the internet!");
            }

        }
        public void SendPaymants()
        {
            HttpClient client = new HttpClient();
            
        }
    }
}
