namespace Escapa.Utility
{
    public enum GameScenes
    {
        Preload = 0,
        Menu,
        Game,
        End,
        Social,
        Info
    }

    public static class PlayerPrefKeys
    {
        public static string IsSoundEnabled = "SoundEnabled";
        public static string Level = "Level";
        public static string Record = "Record";
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

        public static string DifficultyText = "DifficultyText";
        public static string GameCreditText = "GameCreditText";
        public static string GameCopyrightText = "GameCopyrightText";
        public static string GameLinkText = "GameLinkText";
        public static string HighScoreText = "HighScoreText";
        public static string LogoText = "LogoText";
        public static string SoundCreditText = "SoundCreditText";
        public static string SoundCopyrightText = "SoundCopyrightText";
        public static string SoundLinkText = "SoundLinkText";
        public static string TimeText = "TimeText";

        public static string BackButton = "BackButton";
        public static string InfoButton = "InfoButton";
        public static string PlayButton = "PlayButton";
        public static string RestartButton = "RestartButton";
        public static string SocialButton = "SocialButton";
        public static string SoundButton = "SoundButton";

        public static string SceneController = "SceneController";
        public static string SystemController = "SystemController";
    }
}
