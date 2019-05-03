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
        public const string EnemiesCount = "EnemiesCount";
        public const string IsSoundEnabled = "SoundEnabled";
        public const string Level = "Level";
        public const string MaxSpeed = "MaxSpeed";
        public const string MinSpeed = "MinSpeed";
        public const string Record = "Record";

        #region Achievements
        public const string BlackHawk = "BlackHawk";
        public const string Hothead = "Hothead";
        public const string MovesLikeJagger = "MovesLikeJagger";
        public const string PanicButton = "PanicButton";
        public const string TakeItEasy = "TakeItEasy";
        public const string Zen = "Zen";
        #endregion
    }

    public static class ResourceKeys
    {
        public const string Difficulty = "Difficulty";
        public const string Languages = "Languages";
        public const string Style = "Style";
    }

    public static class Tags
    {
        public const string Edges = "Edges";
        public const string Player = "Player";

        public const string CountSliderText = "CountSliderText";
        public const string EnemiesCountText = "EnemiesCountText";
        public const string GameCreditText = "GameCreditText";
        public const string GameCopyrightText = "GameCopyrightText";
        public const string GameLinkText = "GameLinkText";
        public const string GameSetupText = "GameSetupText";
        public const string HighScoreText = "HighScoreText";
        public const string HighScoreTitleText = "HighScoreTitleText";
        public const string LogoText = "LogoText";
        public const string MainCamera = "MainCamera";
        public const string MinimalSpeedText = "MinimalSpeedText";
        public const string MinSpeedSliderText = "MinSpeedSliderText";
        public const string MaximumSpeedText = "MaximumSpeedText";
        public const string MaxSpeedSliderText = "MaxSpeedSliderText";
        public const string SoundCreditText = "SoundCreditText";
        public const string SoundCopyrightText = "SoundCopyrightText";
        public const string SoundLinkText = "SoundLinkText";
        public const string StartText = "StartText";
        public const string TimeText = "TimeText";

        public const string AchievementsButton = "AchievementsButton";
        public const string LeaderboardsButton = "LeaderboardsButton";

        public const string CountSlider = "CountSlider";
        public const string MinSpeedSlider = "MinSpeedSlider";
        public const string MaxSpeedSlider = "MaxSpeedSlider";
        
        public const string SystemController = "SystemController";
        public const string EventSystem = "EventSystem";
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
        public LanguagePair[] English;
        public LanguagePair[] Russian;
    }
    
    [Serializable]
    public class LanguagePair
    {
        public string Token;
        public string Text;
    }

    public enum LanguageTokens
    {
        DifficultyEasy,
        DifficultyMedium,
        DifficultyHard,
        DifficultyInsane,
        DifficultyCustom,
        EnemiesCount,
        GameCopyright,
        GameLink,
        GameSetupTitle,
        HighScoreTitle,
        Logo,
        MinimalSpeed,
        MaximumSpeed,
        NewHighScore,
        Play,
        SoundCredit,
        SoundCopyright,
        SoundLink,
        Time
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
