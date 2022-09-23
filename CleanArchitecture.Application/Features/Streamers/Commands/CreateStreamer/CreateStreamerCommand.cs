using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{

    // Devuelve el ID
    public class CreateStreamerCommand : IRequest<int>
    {


        // Aquí realmente da igual usar String o string. Es lo mismo
        // String viene de System
        // string es un alias, una representación, es decir es lo mismo
        // Las buenas prácticas dicen que es mejor usar el alias
        public string Nombre { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

    }
}
