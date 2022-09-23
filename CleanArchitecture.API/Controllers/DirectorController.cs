using CleanArchitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DirectorController : ControllerBase
    {
        private IMediator _mediator;

        // Mediator lo necesitamos para enviar nuestro command hacia nuestra capa de CQRS
        public DirectorController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost(Name = "CreateDirector")]
        //[Authorize(Roles = "Administrator")]

        //Resultado que va a devolver
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand command)
        {
            // El CQRS se encargará de todo lo demás
            // Aquí el unit of work está trabajando dentro de la capa de aplicación
            // y en el patrón CQRS, el controller no sabe lo que está ocurriendo por detrás
            // el solamente se encarga de crear el command y enviarselo por mediaTr
            // a la capa de application

            return await _mediator.Send(command);


        }


    }
}
