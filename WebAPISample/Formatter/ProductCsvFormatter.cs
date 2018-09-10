
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using WebAPISample.Models;

namespace WebAPISample.Formatter
{
    //定义Product的Formatter
    public class ProductCsvFormatter : BufferedMediaTypeFormatter
    {

        public ProductCsvFormatter()
        {
            //在其构造器中，要添加一个该格式化器所支持的媒体类型。在这个例子中，该格式化器只支持单一的媒体类型：“text/csv”：
            // 添加所支持的媒体类型
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/csv"));
        }

        //可以反序列化哪种类型
        public override bool CanReadType(Type type)
        {
            //不需要反序列化
            return false;
        }

        //重写这个CanWriteType方法，以指示该格式化器可以序列化哪种类型：
        public override bool CanWriteType(Type type)
        {
            if(type==typeof(Product))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Product>);
                return enumerableType.IsAssignableFrom(type);
            }
        }
        // 重写WriteToStream方法。通过将一种类型写成一个流，该方法对该类型进行序列化。如果你的格式化器要支持反序列化，也可以重写ReadFromStream方法
        public override void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            using (var writer = new StreamWriter(writeStream))
            {
                var products = value as IEnumerable<Product>;

                if (products != null)
                {
                    foreach (var product in products)
                    {
                        WriteItem(product, writer);
                    }
                }
                else
                {
                    var singleProduct = value as Product;
                    if (singleProduct == null)
                    {
                        throw new InvalidOperationException("Cannot serialize type");
                    }
                    WriteItem(singleProduct, writer);
                }
            }
            writeStream.Close();
        }

        // 将Product序列化成CSV格式的辅助器方法
        private void WriteItem(Product product, StreamWriter writer)
        {
            writer.WriteLine("{0},{1},{2},{3}", Escape(product.Id),
                Escape(product.Name), Escape(product.Category), Escape(product.Price));
        }

        static char[] _specialChars = new char[] { ',', '\n', '\r', '"' };

        private string Escape(object o)
        {
            if (o == null)
            {
                return "";
            }
            string field = o.ToString();
            if (field.IndexOfAny(_specialChars) != -1)
            {
                return String.Format("\"{0}\"", field.Replace("\"", "\"\""));
            }
            else return field;
        }
    }
}