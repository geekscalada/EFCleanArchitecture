using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class DeleteStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteStreamerCommandHandler> _logger;

        public DeleteStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, Contracts.Infrastructure.IEmailService @object, ILogger<DeleteStreamerCommandHandler> logger)
        {
            //_streamerRepository = streamerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            //var streamerToUpdate =  await  _streamerRepository.GetByIdAsync(request.Id);
            var streamerToUpdate = await _unitOfWork.StreamerRepository.GetByIdAsync(request.Id);

            // Comprobar si lo que vamos a actualizar existe o no
            if (streamerToUpdate == null)
            {
                // Hacemos log y posteriormente exception
                _logger.LogError($"No se encontro el streamer id {request.Id}");

                //nameof parece que es para convertir a string
                // entiendo que no pasas un streamer sino la "clase" para decir que no se ha 
                // encontado cierta entidad con un ID particular. 
                throw new NotFoundException(nameof(Streamer), request.Id);
            }



            //reemplazamos

            // origen request, destino streamertoupdate, y ponemos los tipos de datos también
            _mapper.Map(request, streamerToUpdate, typeof(UpdateStreamerCommand), typeof(Streamer));



            //await _streamerRepository.UpdateAsync(streamerToUpdate);

            //ahora enviamos a la base de datos. 
            // lo hacemos con el repositorio

            _unitOfWork.StreamerRepository.UpdateEntity(streamerToUpdate);

            await _unitOfWork.Complete();

            _logger.LogInformation($"La operacion fue exitosa actualizando el streamer {request.Id}");

            return Unit.Value;
        }
    }
}
