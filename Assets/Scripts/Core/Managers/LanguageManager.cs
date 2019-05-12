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
                    return _language?.russian.FirstOrDefault(x => x.token == token.ToString())?.text ?? string.Empty;
                default:
                    return _language?.english.FirstOrDefault(x => x.token == token.ToString())?.text ?? string.Empty;
            }
        }

        private static void LoadLanguage()
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}").text;
            _language = JsonUtility.FromJson<Language>(json);
        }
    }
}
