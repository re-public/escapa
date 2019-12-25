namespace Escapa.Core.Interfaces
{
    public interface ISoundController
    {
        bool IsMuted { get; }

        void ToggleSound();
    }
}