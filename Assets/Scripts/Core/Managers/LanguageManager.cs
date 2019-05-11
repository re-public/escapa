using System.Linq;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static Language _language;
        public static string GetString(LanguageTokens token)
        {
            if (_language == null)
            {
                LoadLanguage();
            }

            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return _language?.Russian.FirstOrDefault(x => x.Token == token.ToString())?.Text ?? string.Empty;
                default:
                    return _language?.English.FirstOrDefault(x => x.Token == token.ToString())?.Text ?? string.Empty;
            }
        }

        private static void LoadLanguage()
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}").text;
            _language = JsonUtility.FromJson<Language>(json);
        }
    }
}
