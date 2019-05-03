using Escapa.Events;

namespace Escapa.Controllers
{
    public interface IStyleController
    {
        event StyleEvent StyleChanged;
    }
}