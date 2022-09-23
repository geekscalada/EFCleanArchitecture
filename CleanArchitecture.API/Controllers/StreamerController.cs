using CleanArchitecture.Application.Features.Streamers.Commands;
using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StreamerController : ControllerBase
    {

        private IMediator _mediator;

        public StreamerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // solo los admin pueden ejecutar esta lógica de creación
        // es decir necesitas hacer login y recibir el token
        [HttpPost(Name = "CreateStreamer")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType((int)HttpStatusCode.OK)]


        // Aquí los parámetros vienen desde el body por ello el frombody
        // El tipo de dato es CreateStreamerCommand
        // recuerda que este es el que dispara al Handler
        // aquí si lo piensas, el cliente ya está instanciando a un command enviandote la data
        // dentro del body del request, el command ya existe.
        // Lo podemos enviar hacia el handler

        public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand command)
        {
          return  await  _mediator.Send(command);
        }

        [HttpPut(Name = "UpdateStreamer")]

        // Es posible que no haya nada o que no sea encontrado
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }


        [HttpDelete("{id}", Name = "DeleteStreamer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteStreamer(int id)
        {
            var command = new DeleteStreamerCommand
            {
                Id = id
            };

            await _mediator.Send(command);

            return NoContent();    
        }

    }
}
