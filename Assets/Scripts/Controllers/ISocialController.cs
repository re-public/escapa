using Escapa.Utility;

namespace Escapa.Controllers
{
    public interface ISocialController
    {
        void CompleteAchievement(Achievements achievement);
        void SaveAchievementsLocal();
        void SendScore();
    }
}