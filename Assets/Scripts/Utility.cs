using System;
using UnityEngine;

namespace Escapa.Utility
{
    public enum Achievements
    {
        BlackHawk,
        MovesLikeJagger,
        PanicButton,
        Zen
    }

    public enum GameScenes
    {
        Preload = 0,
        Menu,
        Game,
        End,
        Social,
        Info
    }

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
        GameCopyright,
        GameLink,
        HighScoreTitle,
        Logo,
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
