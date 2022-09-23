using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Directors.Commands.CreateDirector
{
    public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
    {
        private readonly ILogger<CreateDirectorCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateDirectorCommandHandler(ILogger<CreateDirectorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
        {
            // Necesitamos que se mapee contra la clase modelo que se llama director. 
            // la data que queremos transformar a este tipo de Director es "request"
            // recuerda que estos mapeos han de declararse en Application -> Mappings -> MappingFrofile
            var directorEntity = _mapper.Map<Director>(request);

            //  Vamos a usar la interfaz generica porque en Director no tenemos interface desarrollada.
            // Necesitaremos llamar a la interface genérica
            // Por eso llamamos primero a unit of work y a su método repository que es el que
            // se encarga de crear la instancia del servicio repository que es genérico
            // por lo que debemos de crearlo sobre director. 
            // el record es directorEntity

            _unitOfWork.Repository<Director>().AddEntity(directorEntity);
            // hasta aquí estamos solo en memoria, si queremos ir a la BDD tenemos que llamar al métoddo
            
            // confirm, al async complete async. 
            // Este complete es el que ya realiza la transacción

            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                _logger.LogError("No se inserto el record del director");
                throw new Exception("No se pudo insertar el record del director");
            }

            return directorEntity.Id;
        }
    }
}
