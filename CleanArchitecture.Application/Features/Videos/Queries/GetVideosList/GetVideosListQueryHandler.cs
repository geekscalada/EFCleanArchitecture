using AutoMapper;
using CleanArchitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArchitecture.Application.Features.Videos.Queries.GetVideosList
{

    //ahora vamos a hacer la implementación de la comunicación mediante la interfaz mediaTr
    public class GetVideosListQueryHandler : IRequestHandler<GetVideosListQuery, List<VideosVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IVideoRepository _videoRepository;


        //Necesitamos mapear. Porque _videoRepository va a ser de tipo lista de video, pero cuando lo devuelvas
        // debes de devolverlo como VideosVm. COnvertir un tipo de valor a otro, necesitamos imapper

        private readonly IMapper _mapper;

        public GetVideosListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideosVm>> Handle(GetVideosListQuery request, CancellationToken cancellationToken)
        {


            var videoList = await  _unitOfWork.VideoRepository.GetVideoByUsername(request._Username);


            //Aquí el destino sea VideosVm el resultado de videoList.
            //mejor explicado en los commands de streamer
            // cuidado porque el mapper hay que inicializarle una configuración. 
            return _mapper.Map<List<VideosVm>>(videoList);
        }
    }
}
