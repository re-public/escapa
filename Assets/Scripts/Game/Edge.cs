using Escapa.Core.Interfaces;
using Escapa.Utility;
using UnityEngine;

namespace Escapa.Game
{
    [RequireComponent(typeof(EdgeCollider2D))]
    public sealed class Edge : MonoBehaviour
    {
        [SerializeField]
        private Edges edgeType;
        
        private new IMainCamera camera;

        private void Awake() => camera = GameObject.FindWithTag(Tags.MainCamera).GetComponent<IMainCamera>();

        private void Start()
        {
            var position = Vector2.zero;
            
            switch (edgeType)
            {
                case Edges.Left:   position.x = -Screen.width * camera.UnitsPerPixel / 2f;  break;
                case Edges.Top:    position.y =  Screen.height * camera.UnitsPerPixel / 2f; break;
                case Edges.Right:  position.x =  Screen.width * camera.UnitsPerPixel / 2f;  break;
                case Edges.Bottom: position.y = -Screen.height * camera.UnitsPerPixel / 2f; break;
            }

            transform.position = position;
        }
    }
}
