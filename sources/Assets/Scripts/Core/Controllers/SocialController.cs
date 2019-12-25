using Escapa.Utility;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Core.Controllers
{
    public class SocialController : MonoBehaviour, ISocialController
    {
        public void CompleteAchievement(string achievementGuid)
        {
            if (!Social.localUser.authenticated) return;

            if (!string.IsNullOrWhiteSpace(achievementGuid))
                Social.ReportProgress(achievementGuid, 100d, success => { });
        }

        public void SendScore(Difficulties difficulty, long time)
        {
            if (!Social.localUser.authenticated) return;

            var leaderboardGuid = string.Empty;

            switch (difficulty)
            {
                case Difficulties.Easy: leaderboardGuid = GooglePlayIds.leaderboard_easy; break;
                case Difficulties.Medium: leaderboardGuid = GooglePlayIds.leaderboard_medium; break;
                case Difficulties.Hard: leaderboardGuid = GooglePlayIds.leaderboard_hard; break;
                case Difficulties.Insane: leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            Social.ReportScore(time, leaderboardGuid, success => { });
        }

        public void ShowAchievements()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowAchievementsUI();
        }

        public void ShowLeaderboards()
        {
            if (!Social.localUser.authenticated) return;

            Social.ShowLeaderboardUI();
        }

        private void Start() => Auth(OnAuthenticated);

        private void Auth(System.Action callback)
        {
            // Check if GPGS is installed
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

            callback?.Invoke();
        }

        private void OnAuthenticated() => SceneManager.LoadScene((int)GameScenes.Menu, LoadSceneMode.Single);
    }
}
