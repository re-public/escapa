using System;

namespace Escapa.Utility
{
    [Serializable]
    public class Levels
    {
        public Level[] levels;
    }

    [Serializable]
    public class Level
    {
        public Difficulties difficulty;
        public float minSpeed;
        public float maxSpeed;
    }
}