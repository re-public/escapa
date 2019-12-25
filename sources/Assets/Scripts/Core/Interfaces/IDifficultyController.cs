using Escapa.Core.Events;

namespace Escapa.Core.Interfaces
{
    public interface IDifficultyController
    {
        event DifficultyEvent Changed;

        void Increase();
        void Decrease();
    }
}
