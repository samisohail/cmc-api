using MediatR;
using Microsoft.Extensions.Configuration;

namespace CMC.Commands
{
    public abstract class CommandBaseController<TCommand, TResult> : RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        protected CommandBaseController() { }

        public TResult UnitTestHandle(TCommand request)
        {
            return Handle(request);
        }
    }
}
