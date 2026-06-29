using Galaga.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Galaga.Managers
{
    public class WaveManager
    {
        public static readonly List<Wave> Waves = [];

        public static void LoadWavesJson()
        {
            var json = File.ReadAllText("Content/waves.json");
            var wavesJson = JsonSerializer.Deserialize<WavesJson>(json);

            foreach (var wave in wavesJson.Waves)
            {
                Waves.Add(wave);
            }
        }
    }
}
