using Escapa.Events;

namespace Escapa.Units
{
    public interface IPlayer : IColoredUnit
    {
        event PlayerEvent Die;
        event PlayerEvent MousePressed;

        /// <summary>
        /// Time without moving.
        /// </summary>
        float IdleTime { get; }

        /// <summary>
        /// Time in non-stop moving.
        /// </summary>
        float MovingTime { get; }
    }
}
