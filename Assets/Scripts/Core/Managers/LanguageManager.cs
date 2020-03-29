using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static readonly string[] strings = Load();
        public static string GetString(LanguageTokens token) => strings[(int)token];

        private static string[] Load()
        {
            var lang = "en";

            switch(Application.systemLanguage)
            {
                case SystemLanguage.French:  lang = "fr"; break;
                case SystemLanguage.German:  lang = "de"; break;
                case SystemLanguage.Russian: lang = "ru"; break;
                case SystemLanguage.Spanish: lang = "es"; break;
            }

            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}/{lang}").text;
            return JsonUtility.FromJson<Language>(json).Strings;
        }
    }
}
