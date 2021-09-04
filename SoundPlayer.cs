namespace BBQLib
{
    public abstract class SoundPlayer
    {
        public string soundDir;
        public abstract void PlaySound(string sound);
        public abstract void LoadSound(string filename);
    }
}