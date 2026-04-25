using System.Text.Json.Serialization;

namespace Galaga.Interfaces
{

    public partial class SpriteAtlasJson
    {
        [JsonPropertyName("texturePath")]
        public string TexturePath { get; set; }

        [JsonPropertyName("gap")]
        public long Gap { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites[] Sprites { get; set; }
    }

    public partial class Sprites
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("x")]
        public long X { get; set; }

        [JsonPropertyName("y")]
        public long Y { get; set; }

        [JsonPropertyName("w")]
        public long W { get; set; }

        [JsonPropertyName("h")]
        public long H { get; set; }

        [JsonPropertyName("totalFrames")]
        public long TotalFrames { get; set; }
    }
}