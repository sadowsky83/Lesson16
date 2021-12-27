using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Text.Encodings.Web;


namespace task1
{
    internal class Program
    {
        // Список товаров
        // Пакет System.Text.Json
        static void Main(string[] args)
        {
            string path = "C:/Any directory/Products.json";
            string jsonStr;
          
            StreamWriter streamWriter = new StreamWriter(path);
            Product[] products = new Product[5];
            Console.WriteLine($"Введите данные по {products.Length} товарам\n");
            for (int i = 0; i <= 4; i++)
            {
                Console.WriteLine($"Товар номер {i + 1}");
                Console.WriteLine($"Введите код товара:");
                int code = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine($"Введите название товара:");
                string name = Convert.ToString(Console.ReadLine());
                Console.WriteLine($"Введите цену товара:");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                Product product = new Product(code, name, price);
                products[i] = product;
                Console.WriteLine();
            }
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            for (int i = 0; i < products.Length; i++)
            {
                jsonStr = JsonSerializer.Serialize(products[i], options);
                streamWriter.Write(jsonStr);
            }
            streamWriter.Close();
            StreamReader sr = new StreamReader(path);
            string jsonStrRead = sr.ReadToEnd();
            sr.Close();
            string str = jsonStrRead.Replace("{", "\t{");
            string[] strArr = str.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
            decimal maxPrice = 0;
            int count = 0;
            Product[] productsRead = new Product[5];
            for (int i = 0; i < strArr.Length; i++)
            {
                productsRead[i] = JsonSerializer.Deserialize<Product>(strArr[i], options);
                if (maxPrice < productsRead[i].Price)
                {
                    maxPrice = productsRead[i].Price;
                    count = i;
                }
            }
            Console.WriteLine($"{productsRead[count].Name} - самый дорогой товар");
            Console.ReadKey();
        }
    }
    class Product
    {
        [JsonPropertyName("Код товара")]
        public int Code { get; set; }
        [JsonPropertyName("Название товара")]
        public string Name { get; set; }
        [JsonPropertyName("Цена товара")]
        public decimal Price { get; set; }
        public Product(int code, string name, decimal price)
        {
            Code = code;
            Name = name;
            Price = price;
        }
    }
}

