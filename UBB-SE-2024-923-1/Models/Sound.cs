namespace UBB_SE_2024_923_1.Models
{
    public enum SoundType
    {
        DRUMS,
        INSTRUMENT,
        FX,
        VOICE
    }

    public class Sound
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public SoundType Type { get; set; }
        public string SoundFilePath { get; set; } = string.Empty;

        public Sound()
        {
        }

        public Sound(int id, string name, SoundType type, string soundFilePath = "")
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.SoundFilePath = soundFilePath;
        }
    }
}