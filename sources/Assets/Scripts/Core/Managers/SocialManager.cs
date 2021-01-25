using Escapa.Utility;
#if GPGS_ENABLED
using GooglePlayGames;
using GooglePlayGames.BasicApi;
#endif
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        public static void CompleteAchievement(string achievementGuid)
        {
            if (!Social.localUser.authenticated) return;

            if (!string.IsNullOrWhiteSpace(achievementGuid))
                Social.ReportProgress(achievementGuid, 100d, success => { });
        }

        public static void SendScore()
        {
            if (!Social.localUser.authenticated) return;

            var time = (long)(ScoreManager.LastTime * 1000);
            var leaderboardGuid = string.Empty;

            switch (DifficultyManager.Current.difficulty)
            {
                case Difficulties.Easy:   leaderboardGuid = GooglePlayIds.leaderboard_easy;        break;
                case Difficulties.Medium: leaderboardGuid = GooglePlayIds.leaderboard_medium;      break;
                case Difficulties.Hard:   leaderboardGuid = GooglePlayIds.leaderboard_hard;        break;
                case Difficulties.Insane: leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            Social.ReportScore(time, leaderboardGuid, success => { });
        }

        public static void Auth(System.Action callback)
        {
#if GPGS_ENABLED
            if (GooglePlayGames.OurUtils.PlatformUtils.Supported)
            {
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.Activate();

                if (Social.localUser.authenticated) return;

                Social.localUser.Authenticate(success =>
                {
                    if (success)
                        ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                });
            }
#endif
            callback?.Invoke();
        }

        public static void ShowAchievements()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowAchievementsUI();
        }

        public static void ShowLeaderboards()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowLeaderboardUI();
        }
    }
}
