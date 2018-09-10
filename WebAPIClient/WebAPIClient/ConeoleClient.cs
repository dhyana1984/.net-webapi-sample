using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebAPIClient.Model;

namespace WebAPIClient.ConeoleClient
{
   public  class MyClient
    {
        public HttpClient SetHead()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public void GetProducts()
        {
            HttpClient client = SetHead();
            HttpResponseMessage response = client.GetAsync("api/products").Result;
            if(response.IsSuccessStatusCode)
            {
                var products = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                foreach (var item in products)
                {
                    Console.WriteLine("{0}\t\t{1};\t{2}", item.Name, item.Price, item.Category);
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }


        public void GetProductsByID(int id)
        {
            HttpClient client = SetHead();
            HttpResponseMessage response = client.GetAsync("api/products/"+id.ToString()).Result;
            if (response.IsSuccessStatusCode)
            {
                var product = response.Content.ReadAsAsync<Product>().Result;
                Console.WriteLine("{0}\t\t{1};\t{2}", product.Name, product.Price, product.Category); 
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
        }

        public void PostProduct()
        {
            HttpClient client = SetHead();
            var gizmo = new Product() { Name = "Gizmo", Price = 100, Category = "Widget" };
            Uri gizmoUri = null;
            HttpResponseMessage response = client.PostAsJsonAsync("api/products", gizmo).Result;
            if(response.IsSuccessStatusCode)
            {
                gizmoUri = response.Headers.Location;
                Console.WriteLine(gizmoUri);
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }
            gizmo.Price = 99.9;
            response = client.PutAsJsonAsync(gizmoUri.PathAndQuery, gizmo).Result;
            Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
        }


    }
}
