using Galaga.Core.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;

namespace Galaga.Managers
{
    public class AudioManager
    {
        private readonly string _pathSounds;
        
        private SoundEffect sfFire;
        private SoundEffect sfExplosion;
        public AudioManager(string pathSounds)
        {
            _pathSounds = pathSounds;
            EventManager.OnPlayerShoot += SoundFire;
            EventManager.OnEnemyDeath += SoundExplosion;
        }

        public void LoadSounds(ContentManager cm)
        {
            sfFire = cm.Load<SoundEffect>($"{_pathSounds}/fire");
            sfExplosion = cm.Load<SoundEffect>($"{_pathSounds}/explosion");
        }

        private void SoundFire(Vector2 _) => sfFire.Play();
        private void SoundExplosion(int _) => sfExplosion.Play();
    }
}