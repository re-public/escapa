using UnityEngine;

namespace Escapa.Utility
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Language")]
    public sealed class Language : ScriptableObject
    {
        public SystemLanguage SystemLanguage;
        public string DifficultyEasy;
        public string DifficultyMedium;
        public string DifficultyHard;
        public string DifficultyInsane;
        public string GameCopyright;
        public string GameLink;
        public string HighScoreTitle;
        public string Logo;
        public string NewHighScore;
        public string SoundCredit;
        public string SoundCopyright;
        public string SoundLink;

        // TODO Figure out how to do it in a normal way.
        public string GetString(LanguageTokens token)
        {
            switch (token)
            {
                case LanguageTokens.DifficultyEasy: return DifficultyEasy;
                case LanguageTokens.DifficultyMedium: return DifficultyMedium;
                case LanguageTokens.DifficultyHard: return DifficultyHard;
                case LanguageTokens.DifficultyInsane: return DifficultyInsane;
                case LanguageTokens.GameCopyright: return GameCopyright;
                case LanguageTokens.GameLink: return GameLink;
                case LanguageTokens.HighScoreTitle: return HighScoreTitle;
                case LanguageTokens.Logo: return Logo;
                case LanguageTokens.NewHighScore: return NewHighScore;
                case LanguageTokens.SoundCredit: return SoundCredit;
                case LanguageTokens.SoundCopyright: return SoundCopyright;
                case LanguageTokens.SoundLink: return SoundLink;
                case LanguageTokens.None:
                default: return string.Empty;
            }
        }
    }
}
