using Escapa.Utility;

public interface ISocialController
{
    void CompleteAchievement(string achievementGuid);
    void SendScore(Difficulties difficulty, long time);
    void ShowAchievements();
    void ShowLeaderboards();
}
