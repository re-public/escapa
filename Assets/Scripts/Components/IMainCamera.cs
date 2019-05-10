using UnityEngine;

namespace Escapa.Components
{
    public interface IMainCamera
    {
        float UnitsPerPixel { get; }
        Vector2 ScreenToWorldPoint(Vector2 point);
    }
}