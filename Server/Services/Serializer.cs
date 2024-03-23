using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Server.Services
{
    public static class Serializer
    {
        private static readonly JsonSerializer _serializer;
        private static JsonSerializer GetSerializer()
        {
            JsonSerializerSettings settings = new()
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = new ContractResolverWithPrivates()
            };

            return JsonSerializer.Create(settings);
        }
        static Serializer()
        {
            _serializer = GetSerializer();
        }
        public static string Serialize(dynamic obj)
        {
            StringWriter sw = new();
            _serializer.Serialize(sw, obj);
            string serObj = sw.ToString();
            //Console.WriteLine(serObj);
            sw.Close();
            return serObj;
        }
        public static T? Deserialize<T>(string serObj)
        {
            JsonTextReader sr = new(new StringReader(serObj));
            T? objNullable = _serializer.Deserialize<T>(sr);
            sr.Close();
            return objNullable;
        }
    }
    internal class ContractResolverWithPrivates : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            if (member is PropertyInfo)
            {
                prop.Writable = true;
                prop.Readable = true;
            }
            return prop;
        }
    }

}
