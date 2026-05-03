using Galaga.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Galaga.Helpers
{
    public static class Constants
    {
        public const int WidthScreen  = 1024;
        public const int HeightScreen = 768;
        
        public const int WidthGame    = 600;
        public const int HeightGame   = HeightScreen;

        public const int WidthUIGame  = WidthScreen - WidthGame;
        public const int HeightUIGame = HeightScreen;

        public const int EnemiesGap   = 12;
        public const float Scale      = 3;

        public static readonly IReadOnlyCollection<BezierPoints> PointsSubWaveOne = Array.AsReadOnly(
            new BezierPoints[] {
                new(){
                    Points = [
                        new Vector2(200,0),
                        new Vector2(200,200),
                        new Vector2(600,-70),
                        new Vector2(600,400),
                        new Vector2(300,400),
                        new Vector2(300,265),
                    ]
                }, new (){
                    Points = [
                        new Vector2(400, 0),
                        new Vector2(400, 200),
                        new Vector2(0, -70),
                        new Vector2(0, 400),
                        new Vector2(300, 400),
                        new Vector2(300, 265)
                    ]
                }
            }
        );

        public const int SubWaveTwo   = 0;
        public const int SubWaveThree = 0;
        public const int SubWaveFour  = 0;
        public const int SubWaveFive  = 0;
    }
}