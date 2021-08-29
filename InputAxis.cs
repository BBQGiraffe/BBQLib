using Newtonsoft.Json;
namespace BBQLib
{
    public class InputAxis
    {

        public KeyboardKey up, down;
        [JsonIgnore]
        public float currentValue;
    }
}
