namespace demo.json
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Bson;
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;

    internal class Program
    {
        static void Main(string[] args)
        {
            var cfg = new JsonSerializerSettings
            {
                //TypeNameHandling = TypeNameHandling.Objects,
            };
            cfg.Converters.Add(new SkuConverter());

            var o = JsonConvert.SerializeObject(new Line {Sku = new CrossCode() {Type = "jpc", Xref = "123"}}, cfg);
            Console.WriteLine(o);


            var o2 = JsonConvert.SerializeObject(new Line { Sku = new ShortCode() { Value=123} }, cfg);
            Console.WriteLine(o2);


            var a = "{\"Sku\": { \"xref\":\"a123\", \"type\":\"jpc\" }}";
            //var a = "{\"Sku\": 123}"; 
            var b = JsonConvert.DeserializeObject<Line>(a, cfg);

            Console.ReadLine();
        }

        static void Bson()
        {
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

            var o = JsonConvert.SerializeObject(cfg, settings);
            var ob = Encoding.UTF8.GetBytes(o).Length;

            Console.WriteLine("JSON Length: {0}", ob);
            Console.WriteLine(o);

            using (var str = new MemoryStream())
            {
                var bw = new BsonWriter(str);
                JsonSerializer js = new JsonSerializer();
                js.Serialize(bw, cfg);

                var bytes = str.ToArray();
                var bsonLength = bytes.Length;
                Console.WriteLine("BSON Length: {0}", bsonLength);
                Console.WriteLine(Convert.ToBase64String(bytes));
            }
        }
    }



    public class SkuConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var s = value as ShortCode;
            var c = value as CrossCode;

            if (s != null)
            {
                writer.WriteValue(s.Value);
            }
            else
            {
                var jo = new JObject();
                jo.Add("xref", JToken.FromObject(c.Xref));
                jo.Add("type", JToken.FromObject(c.Type));
                jo.WriteTo(writer);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var a = reader.TokenType;
            if (a == JsonToken.StartObject)
            {
                var c = new CrossCode();
                //move off of the start object
                reader.Read();
                while (reader.TokenType != JsonToken.EndObject)
                {
                    var prop = reader.Value.ToString();
                    var value = reader.ReadAsString();
                    var pi = typeof (CrossCode).GetProperty(prop, BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    pi.SetValue(c, value);

                    //advance the reader
                    reader.Read();
                }

                return c;
            }

            return new ShortCode
            {
                Value = (int)reader.Value
            };
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType.GetInterfaces().Contains(typeof (ISku)) || objectType == typeof(ISku);
        }
    }
    public interface ISku
    {
        
    }

    public class ShortCode : ISku
    {
        public int Value { get; set; }
    }

    public class CrossCode : ISku
    {
        public string Xref { get; set; }
        public string Type { get; set; }
    }


    public class Line
    {
        public ISku Sku { get; set; }
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
