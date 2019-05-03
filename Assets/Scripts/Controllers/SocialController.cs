using Escapa.Managers;
using Escapa.Utility;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Escapa.Controllers
{
    public sealed class SocialController : MonoBehaviour, ISocialController
    {
        public void CompleteAchievement(Achievements achievement)
        {
            if (!Social.localUser.authenticated && _achievementsFlags[(int)achievement]) return;

            var achievementGuid = string.Empty;

            switch(achievement)
            {
                case Achievements.BlackHawk:
                    achievementGuid = GooglePlayIds.achievement_black_hawk; break;
                case Achievements.Hothead:
                    achievementGuid = GooglePlayIds.achievement_hothead; break;
                case Achievements.MovesLikeJagger:
                    achievementGuid = GooglePlayIds.achievement_moves_like_jagger; break;
                case Achievements.PanicButton:
                    achievementGuid = GooglePlayIds.achievement_panic_button; break;
                case Achievements.TakeItEasy:
                    achievementGuid = GooglePlayIds.achievement_take_it_easy; break;
                case Achievements.Zen:
                    achievementGuid = GooglePlayIds.achievement_zen; break;
            }

            _achievementsFlags[(int)achievement] = true;
            Social.ReportProgress(achievementGuid, 100d, (success) => { });
        }

        public void SendScore()
        {
            if (!Social.localUser.authenticated || DifficultyManager.CurrentLevelIsCustom) return;

            var time = (long)(ScoreManager.LastTime * 1000);
            var leaderboardGuid = string.Empty;

            switch (DifficultyManager.Level)
            {
                case 0:
                    leaderboardGuid = GooglePlayIds.leaderboard_easy; break;
                case 1:
                    leaderboardGuid = GooglePlayIds.leaderboard_medium; break;
                case 2:
                    leaderboardGuid = GooglePlayIds.leaderboard_hard; break;
                case 3:
                    leaderboardGuid = GooglePlayIds.leaderboard_i_n_s_a_n_e; break;
            }

            Social.ReportScore(time, leaderboardGuid, (success) => { });
        }

        public void SaveAchievementsLocal()
        {
            for (var i = 0; i < _achievementsCount; i++)
            {
                var achievementName = string.Empty;

                switch ((Achievements)i)
                {
                    case Achievements.BlackHawk: achievementName = PlayerPrefKeys.BlackHawk; break;
                    case Achievements.Hothead: achievementName = PlayerPrefKeys.Hothead; break;
                    case Achievements.MovesLikeJagger: achievementName = PlayerPrefKeys.MovesLikeJagger; break;
                    case Achievements.PanicButton: achievementName = PlayerPrefKeys.PanicButton; break;
                    case Achievements.TakeItEasy: achievementName = PlayerPrefKeys.TakeItEasy; break;
                    case Achievements.Zen: achievementName = PlayerPrefKeys.Zen; break;
                }

                PlayerPrefs.SetInt(achievementName, _achievementsFlags[i] ? 1 : 0);
            }
        }

        private static readonly int _achievementsCount = 6;
        private static bool[] _achievementsFlags;

        private void Start()
        {
            Auth();
        }

        private void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Escape))
                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
        }
        
        private static void Auth()
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

                _achievementsFlags = new bool[_achievementsCount];
                for (var i = 0; i < _achievementsCount; i++)
                {
                    var achievementName = string.Empty;

                    switch ((Achievements)i)
                    {
                        case Achievements.BlackHawk: achievementName = PlayerPrefKeys.BlackHawk; break;
                        case Achievements.Hothead: achievementName = PlayerPrefKeys.Hothead; break;
                        case Achievements.MovesLikeJagger: achievementName = PlayerPrefKeys.MovesLikeJagger; break;
                        case Achievements.PanicButton: achievementName = PlayerPrefKeys.PanicButton; break;
                        case Achievements.TakeItEasy: achievementName = PlayerPrefKeys.TakeItEasy; break;
                        case Achievements.Zen: achievementName = PlayerPrefKeys.Zen; break;
                    }

                    _achievementsFlags[i] = PlayerPrefs.GetInt(achievementName, 0) != 0;
                }

                SceneManager.LoadSceneAsync((int) GameScenes.Menu, LoadSceneMode.Single);
            });
        }
    }
}
