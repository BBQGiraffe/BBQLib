using System.Collections.Generic;
using System;
namespace BBQLib
{
    public static class Input
    {
        static Dictionary<string, InputAxis> axes = new Dictionary<string, InputAxis>();
        

        public static bool IsKeyDown(KeyboardKey key)
        {
            return BBQLib.window.IsKeyDown(key);
        }

        public static void LoadAxisFile(string filename)
        {
            axes = Json.Deserialize<Dictionary<string, InputAxis>>(filename);
        }

        public static void ProcessInputs()
        {
            foreach(var axis in axes.Values)
            {
                axis.currentValue = 0;
                if(IsKeyDown(axis.up))
                {
                    axis.currentValue = 1;
                }
                if(IsKeyDown(axis.down))
                {
                    axis.currentValue = -1;
                }

            }
        }

        public static float GetAxis(string name)
        {
            return axes[name].currentValue;
        }

    }
}