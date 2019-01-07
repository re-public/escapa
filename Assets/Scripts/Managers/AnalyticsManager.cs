using System.Collections.Generic;
using UnityEngine.Analytics;

namespace Escapa.Managers
{
    public static class AnalyticsManager
    {
        public static void SendGameStartEvent()
        {
            AnalyticsEvent.GameStart(new Dictionary<string, object>
            {
                { "Difficulty", DifficultyManager.Level }
            });
        }

        public static void SendGameOverEvent()
        {
            AnalyticsEvent.GameOver(eventData: new Dictionary<string, object>
            {
                { "Time", ScoreManager.CurrentRecord },
                { "Difficulty", DifficultyManager.Level }
            });
        }
    }
}
