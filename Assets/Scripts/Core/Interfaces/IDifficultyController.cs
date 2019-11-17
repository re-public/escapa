using Escapa.Core.Events;
using Escapa.Utility;

namespace Escapa.Core.Interfaces
{
    public interface IDifficultyController
    {
        event GameEvent Changed;

        Level Current { get; }

        void Increase();
        void Decrease();
    }
}
