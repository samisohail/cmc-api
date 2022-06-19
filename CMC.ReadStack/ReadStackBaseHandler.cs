using AutoMapper;
using MediatR;

namespace CMC.ReadStack
{
    public abstract class ReadStackBaseHandler<TQuery, TResult> : RequestHandler<TQuery, TResult> where  TQuery : IRequest<TResult>
    {
        protected readonly IMapper _mapper;

        protected ReadStackBaseHandler(IMapper mapper)
        {
            _mapper = mapper;
        }
        protected abstract override TResult Handle(TQuery request);
    }
}
