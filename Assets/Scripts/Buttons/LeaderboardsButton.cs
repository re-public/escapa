using UnityEngine;

namespace Escapa.Buttons
{
    public sealed class LeaderboardsButton : ImageButtonBase
    {
        public override void Action()
        {
            if (Social.localUser.authenticated)
            {
                Social.ShowLeaderboardUI();
            }
        }
        
        private void Start()
        {
            gameObject.SetActive(Social.localUser.authenticated);
        }
    }
}