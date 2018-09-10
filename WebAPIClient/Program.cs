using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebAPIClient.ConeoleClient;

namespace WebAPIClient
{
   public class Program
    {
        static void Main(string[] args)
        {

            MyClient myClient = new MyClient();
            myClient.PostProduct();
            Console.ReadLine();

        } 
    }
}
