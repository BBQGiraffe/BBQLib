using Newtonsoft.Json;
using System;
using System.IO;
namespace BBQLib
{
    public static class Json
    {
        static JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            TypeNameHandling = TypeNameHandling.Auto
        };

        public static void Serialize(object o, string filename)
        {
            File.WriteAllText(filename, JsonConvert.SerializeObject(o, settings));
        }

        public static T Deserialize<T>(string filename)
        {
            if(!File.Exists(filename))
            {
                Serialize(Activator.CreateInstance<T>(), filename);
            }
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(filename), settings);
        }
    }
}