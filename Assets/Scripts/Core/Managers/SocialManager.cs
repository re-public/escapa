using System;
using Escapa.Utility;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class SocialManager
    {
        /// <summary>
        /// Send request to complete achievement with specified guid.
        /// </summary>
        /// <param name="achievementGuid">Guid of achievement to complete.</param>
        public static void CompleteAchievement(string achievementGuid)
        {
            if (Social.localUser.authenticated && !string.IsNullOrWhiteSpace(achievementGuid))
            {
                Social.ReportProgress(achievementGuid, 100d, success => { });
            }
        }

        /// <summary>
        /// Send score to current leaderboard.
        /// </summary>
        public static void SendScore()
        {
            if (Social.localUser.authenticated)
            {

                var time = (long) (ScoreManager.LastTime * 1000);
                var leaderboardGuid = string.Empty;

                switch (DifficultyManager.Current.difficulty)
                {
                    case Difficulties.Easy:
                        leaderboardGuid = GooglePlayIds.leaderboard_easy;
                        break;
                    case Difficulties.Medium:
                        leaderboardGuid = GooglePlayIds.leaderboard_medium;
                        break;
                    case Difficulties.Hard:
                        leaderboardGuid = GooglePlayIds.leaderboard_hard;
                        break;
                    case Difficulties.Insane:
                        leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e;
                        break;
                }

                Social.ReportScore(time, leaderboardGuid, success => { });
            }
        }

        /// <summary>
        /// Authorize in google play services.
        /// </summary>
        public static void Auth(Action callback)
        {
            var config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.Activate();

            Social.localUser.Authenticate(success => 
            {
                if (success)
                {
                    ((PlayGamesPlatform)Social.Active).SetGravityForPopups(Gravity.BOTTOM);
                }
                
                callback?.Invoke();
            });
        }
    }
}