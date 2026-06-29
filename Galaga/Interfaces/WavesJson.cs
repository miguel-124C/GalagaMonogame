
using System.Text.Json.Serialization;

namespace Galaga.Interfaces
{
    public partial class WavesJson
    {
        [JsonPropertyName("waves")]
        public Wave[] Waves {get; set;}
    }

    public partial class Wave
    {
        [JsonPropertyName("subwaves")]
        public Subwave[] Subwaves {get; set;}
    }

    public partial class Subwave
    {
        [JsonPropertyName("points")]
        public EnemyPositionFinal[] Points {get; set;}
        [JsonPropertyName("enemies")]
        public EnemyPositionFinal[] Enemies {get; set;}
    }

    public partial class EnemyPositionFinal
    {
        [JsonPropertyName("x")]
        public long X {get; set;}
        [JsonPropertyName("y")]
        public long Y {get; set;}
    }
}