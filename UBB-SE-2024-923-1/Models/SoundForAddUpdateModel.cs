namespace UBB_SE_2024_923_1.Models
{
    public class SoundForAddUpdateModel
    {
        public string Name { get; set; } = null!;
        public SoundType Type { get; set; }
        public string SoundFilePath { get; set; } = null!;
    }
}
