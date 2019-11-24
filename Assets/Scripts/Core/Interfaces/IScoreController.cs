using Escapa.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escapa.Core.Interfaces
{
    public interface IScoreController
    {
        float CurrentTime { get; }
        float LastTime { get; }
        bool IsHighScore { get; }

        float BlackHawkTime { get; }
        float ZenTime { get; }
        float JaggerTime { get; }

        void StartCount();
        void StopCount(Difficulties difficulty);
        float GetHigh(Difficulties difficulty);
        void Save();
    }
}
