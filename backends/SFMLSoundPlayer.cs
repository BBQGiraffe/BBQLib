using System.Collections.Generic;
using System.IO;
using SFML.Audio;
namespace BBQLib
{
    internal class SFMLSoundPlayer : SoundPlayer
    {
        Dictionary<string, SFML.Audio.Sound> sfSounds = new Dictionary<string, SFML.Audio.Sound>(); 
        
        
        public override void PlaySound(string sound)
        {
            sfSounds[soundDir + "/" + sound].Play();
        }

        public override void LoadSound(string filename)
        {
            var sfSound = new SFML.Audio.Sound(new SoundBuffer(filename));
            sfSounds.Add(filename, sfSound);
        }
    }
}