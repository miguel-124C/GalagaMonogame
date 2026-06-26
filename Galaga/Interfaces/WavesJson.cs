
namespace Galaga.Interfaces
{
    public partial struct WavesJson
    {
        public Wave[] Waves;
    }

    public partial struct Wave
    {
        public Subwave[] Subwaves;
    }

    public partial struct Subwave
    {
        public EnemyPositionFinal[] Points;
        public EnemyPositionFinal[] Enemies;
    }

    public partial struct EnemyPositionFinal
    {
        public long X;
        public long Y;
    }
}