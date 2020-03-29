using System;

namespace Escapa.Assets.Scripts.Utility
{
    [Serializable]
    public sealed class AchievementsConfig
    {
        public TimeConfig Time;
    }

    [Serializable]
    public sealed class TimeConfig
    {
        public float BlackHawk;
        public float Zen;
        public float Jagger;
    }
}
