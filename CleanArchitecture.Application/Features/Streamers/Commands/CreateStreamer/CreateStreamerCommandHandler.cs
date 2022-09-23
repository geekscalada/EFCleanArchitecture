using AutoMapper;
using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands
{
    // En este doc podemos ver la diferencia entre usar el streamerRepository y usar el unit of work
    public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {

        // Aquí lo que estamos haciendo es inyectar los objetos dentro de la clase
        // Son servicios que vamos a usar en la clase


        //private readonly IStreamerRepository _streamerRepository; [ya no lo usaremos, usaremos UOW] ***************
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailservice;

        // Este es un servicio de log, que va a trabajar sobre esta clase, StreamerCommandHandler

        private readonly ILogger<CreateStreamerCommandHandler> _logger;

        public CreateStreamerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailservice, ILogger<CreateStreamerCommandHandler> logger)
        {
            //_streamerRepository = streamerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailservice = emailservice;
            _logger = logger;
        }

        
        
        //este handle es obligatorio, una vez hayamos creado el constructor, habiendo antes
        //inyectado lo necesario, podremos trabajar con esos objetos

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {

            // lo que nos da el cliente es un StreamerCommand, pero lo que nosotros
            // insertaremos en la BDD es un Streamer, por ello tenemos que mapearlo con el IMapper
            // recuerda que request es de tipo Streamercomand, que solo mapea dos propiedades string
            var streamerEntity = _mapper.Map<Streamer>(request);
            //var newStreamer = await _streamerRepository.AddAsync(streamerEntity);

            // Unit of work llama a la instancia streamerRepository y podemos usar sus métodos. 
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);

            // Confirmamos la transacción
            var result = await _unitOfWork.Complete();

            if (result <= 0)
            {
                throw new Exception($"No se pudo insertar el record de streamer");
            }

            _logger.LogInformation($"Streamer {streamerEntity.Id} fue creado existosamente");

            await SendEmail(streamerEntity);

            return streamerEntity.Id;
        }


        // Método para enviar el email
        // Como no va a ser usado fuera de aquí, lo declaramos como try catch
        private async Task SendEmail(Streamer streamer)
        {
            var email = new Email
            {
                To = "vaxi.drez.social@gmail.com",
                Body = "La compania de streamer se creo correctamente",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailservice.SendEmail(email);
            }
            catch (Exception ex) {
                _logger.LogError($"Errores enviando el email de {streamer.Id}");
            }

        }

    }
}
