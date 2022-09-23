using CleanArchitecture.Application.Features.Videos.Queries.GetVideosList;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArchitecture.API.Controllers
{
    [ApiController]
    // Añadimos ruta
    // en realidad se transforma en api/v1/video
    [Route("api/v1/[controller]")]
    public class VideoController : ControllerBase
    {

        //este es el punto donde empezamos a desarrollar la capa de presentación
        // Indicamos que es una clase API controller
        private readonly IMediator _mediator;



        // mediaTr
        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //parámetros para el método get
        [HttpGet("{username}", Name = "GetVideo")]
        [Authorize]
        // devolvemos lista con VideosVm y un int para el status
        [ProducesResponseType(typeof(IEnumerable<VideosVm>), (int)HttpStatusCode.OK)]

        // Método que nos permita consultar lista de videos
        public async Task<ActionResult<IEnumerable<VideosVm>>> GetVideosByUsername(string username)
        {
            // Hacemos la query y la enviamos.
            var query = new GetVideosListQuery(username);
            var videos = await _mediator.Send(query);
            return Ok(videos);
        }
    }

}
