using MediatR;

namespace CMC.Commands
{
    public abstract class CommandBaseController<TCommand, TResult> : RequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {
        // protected readonly string _baseCurrency;
        protected CommandBaseController()
        {
            // _baseCurrency = configuration.GetSection("BaseCurrency").Value;
        }

        public TResult UnitTestHandle(TCommand request)
        {
            return Handle(request);
        }
    }
}
