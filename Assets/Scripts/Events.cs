using Escapa.Utility;
using System;

namespace Escapa.Events
{
    public delegate void ButtonEvent();

    public delegate void GameEvent(object sender, GameEventArgs e);
    public sealed class GameEventArgs : EventArgs
    {
        public int Difficulty { get; private set; }
        public float Time { get; private set; }

        public GameEventArgs(int difficulty, float time)
        {
            Difficulty = difficulty;
            Time = time;
        }
    }

    public delegate void PlayerEvent();

    public delegate void SystemEvent(object sender, SystemEventArgs e);
    public sealed class SystemEventArgs : EventArgs
    {
        public GameScenes Scene { get; private set; }

        public SystemEventArgs(int sceneIndex)
        {
            Scene = (GameScenes)sceneIndex;
        }
    }
}
