using Newtonsoft.Json;
namespace BBQLib
{
    public class Font
    {
        [JsonIgnore]
        public string jsonFilename = "";
        public string ttf = "";
        public uint size = 24;
    }
}