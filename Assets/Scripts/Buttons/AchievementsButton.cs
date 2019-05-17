using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class AchievementsButton : ImageButtonBase
    {
        public override void Action() => Social.ShowAchievementsUI();

        private new void Start()
        {
            base.Start();
            gameObject.SetActive(Social.localUser.authenticated);
        }
    }
}