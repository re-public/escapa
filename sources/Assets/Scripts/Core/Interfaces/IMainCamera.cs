using UnityEngine;

namespace Escapa.Core.Interfaces
{
    public interface IMainCamera
    {
        float UnitsPerPixel { get; }
        Vector2 ScreenToWorldPoint(Vector2 point);
    }
}
