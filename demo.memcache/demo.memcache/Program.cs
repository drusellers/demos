namespace demo.memcache
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Enyim.Caching;
    using Enyim.Caching.Configuration;
    using Enyim.Caching.Memcached;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using Newtonsoft.Json.Converters;

    internal class Program
    {
        static void Main(string[] args)
        {
            var cfga = new MemcachedClientConfiguration();
            cfga.Protocol = MemcachedProtocol.Binary;
            cfga.Servers.Add(new IPEndPoint(IPAddress.Loopback, 11211));



            var cfg = new Config
            {
                Things = new List<IThing>
                {
                    new Bill
                    {
                        Name = "Bill",
                        Mary = Joe.Abc
                    },
                    new Bob
                    {
                        Name = "Bob",
                        Age = 22
                    }
                }
            };

            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            };


            var jsonString = JsonConvert.SerializeObject(cfg, settings);
            var jsonBytes = Encoding.UTF8.GetBytes(jsonString);
            var jsonLength = jsonBytes.Length;

            Console.WriteLine("JSON string Length: {0}", jsonString.Length);
            Console.WriteLine("JSON Byte Length: {0}", jsonLength);

            
            var bytes = BsonConverter.Serialize(cfg);
            var bsonLength = bytes.Length;

            Console.WriteLine("BSON Byte Length: {0}", bsonLength);
            Console.WriteLine(jsonString);

            Console.ReadLine();

            using (var client = new MemcachedClient(cfga))
            {
                var ooo = client.Store(StoreMode.Set, "bob", bytes);
                Console.WriteLine("SET: {0}", ooo);

                var a = client.Get("bob") as byte[];
                var aaaa = BsonConverter.Deserialize<Config>(a);
            }
            Console.WriteLine(Convert.ToBase64String(bytes));




            Console.ReadLine();

        }
    }

    public static class BsonConverter
    {
        static JsonSerializer js = new JsonSerializer();

        public static byte[] Serialize<TObject>(TObject input)
        {
            using (var str = new MemoryStream())
            {
                var bw = new BsonWriter(str);
                js.TypeNameHandling = TypeNameHandling.Objects;
                js.Serialize(bw, input);

                return str.ToArray();
            }
        }

        public static TObject Deserialize<TObject>(byte[] input)
        {
            using (var ims = new MemoryStream(input))
            {
                var br = new BsonReader(ims);
                var output = js.Deserialize<TObject>(br);
                return output;
            }
        }   
    }

    public enum Joe
    {
        Abc, Def
    }
    public class Config
    {
        public List<IThing> Things { get; set; }
    }

    public interface IThing
    {
        string Name { get; set; }
    }

    public class Bob : IThing
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    public class Bill : IThing
    {
        public string Name { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public Joe Mary { get; set; }
    }
}
