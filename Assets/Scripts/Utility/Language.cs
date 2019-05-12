using System;

namespace Escapa.Utility
{
    [Serializable]
    public class Language
    {
        public LanguagePair[] english;
        public LanguagePair[] russian;
    }
    
    [Serializable]
    public class LanguagePair
    {
        public string token;
        public string text;
    }
}