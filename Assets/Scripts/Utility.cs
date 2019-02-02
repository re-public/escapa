using System;
using UnityEngine;

namespace Escapa.Utility
{
    public enum Achievements
    {
        BlackHawk,
        Hothead,
        MovesLikeJagger,
        PanicButton,
        TakeItEasy,
        Zen
    }

    public enum GameScenes
    {
        Preload = 0,
        Menu,
        Setup,
        Game,
        End,
        Social,
        Info
    }

    public static class PlayerPrefKeys
    {
        public static string EnemiesCount = "EnemiesCount";
        public static string IsSoundEnabled = "SoundEnabled";
        public static string Level = "Level";
        public static string MaxSpeed = "MaxSpeed";
        public static string MinSpeed = "MinSpeed";
        public static string Record = "Record";

        #region Achievements
        public static string BlackHawk = "BlackHawk";
        public static string Hothead = "Hothead";
        public static string MovesLikeJagger = "MovesLikeJagger";
        public static string PanicButton = "PanicButton";
        public static string TakeItEasy = "TakeItEasy";
        public static string Zen = "Zen";
        #endregion
    }

    public static class ResourceKeys
    {
        public static string Difficulty = "Difficulty";
        public static string Languages = "Languages";
        public static string Style = "Style";
    }

    public static class Tags
    {
        public static string Edges = "Edges";
        public static string Enemies = "Enemies";
        public static string Player = "Player";

        public static string CountSliderText = "CountSliderText";
        public static string DifficultyText = "DifficultyText";
        public static string EnemiesCountText = "EnemiesCountText";
        public static string GameCreditText = "GameCreditText";
        public static string GameCopyrightText = "GameCopyrightText";
        public static string GameLinkText = "GameLinkText";
        public static string GameSetupText = "GameSetupText";
        public static string HighScoreText = "HighScoreText";
        public static string HighScoreTitleText = "HighScoreTitleText";
        public static string LoadingText = "LoadingText";
        public static string LogoText = "LogoText";
        public static string MinimalSpeedText = "MinimalSpeedText";
        public static string MinSpeedSliderText = "MinSpeedSliderText";
        public static string MaximumSpeedText = "MaximumSpeedText";
        public static string MaxSpeedSliderText = "MaxSpeedSliderText";
        public static string SoundCreditText = "SoundCreditText";
        public static string SoundCopyrightText = "SoundCopyrightText";
        public static string SoundLinkText = "SoundLinkText";
        public static string StartText = "StartText";
        public static string TimeText = "TimeText";

        public static string AchievementsButton = "AchievementsButton";
        public static string BackButton = "BackButton";
        public static string InfoButton = "InfoButton";
        public static string LeaderboardsButton = "LeaderboardsButton";
        public static string PlayButton = "PlayButton";
        public static string RestartButton = "RestartButton";
        public static string SocialButton = "SocialButton";
        public static string SoundButton = "SoundButton";

        public static string CountSlider = "CountSlider";
        public static string MinSpeedSlider = "MinSpeedSlider";
        public static string MaxSpeedSlider = "MaxSpeedSlider";

        public static string SceneController = "SceneController";
        public static string SystemController = "SystemController";
    }

    #region Difficulty classes
    [Serializable]
    public class DifficultyRules
    {
        public LevelRules[] Levels;
    }

    [Serializable]
    public class LevelRules
    {
        public int Count;
        public float MinSpeed;
        public float MaxSpeed;
    }
    #endregion

    #region Language classes
    [Serializable]
    public class Language
    {
        public string[] Difficulties;
        public string EnemiesCount;
        public string GameCredit;
        public string GameCopyright;
        public string GameLink;
        public string GameSetupTitle;
        public string HighScoreTitle;
        public string Logo;
        public string MinimalSpeed;
        public string MaximumSpeed;
        public string NewHighScore;
        public string Play;
        public string SoundCredit;
        public string SoundCopyright;
        public string SoundLink;
        public string Start;
        public string Time;
    }
    #endregion

    #region Style classes
    [Serializable]
    public class Style
    {
        public Theme[] Themes;
    }

    [Serializable]
    public class Theme
    {
        public Color Background;
        public Color Enemy;
        public Color Player;
        public Color Text;
        public Color TextAlfa;
    }
    #endregion
}
