using Escapa.Core.Managers;

namespace Escapa.Components
{
    public sealed class TimeCounterLabel : Label
    {
        private new void Start() => TextMesh.color = StyleManager.Current.textAlfa;

        private void FixedUpdate() => TextMesh.SetText(ScoreManager.CurrentTime.ToString("0.00"));
    }
}