using System.Linq;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Core.Managers
{
    public static class LanguageManager
    {
        private static readonly Language _language = Load();
        public static string GetString(LanguageTokens token)
        {
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Russian:
                    return _language?.russian.FirstOrDefault(x => x.token == token.ToString())?.text ?? string.Empty;
                default:
                    return _language?.english.FirstOrDefault(x => x.token == token.ToString())?.text ?? string.Empty;
            }
        }

        private static Language Load()
        {
            var json = Resources.Load<TextAsset>($"{ResourceKeys.Languages}").text;
            return JsonUtility.FromJson<Language>(json);
        }
    }
}
