using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CMC.Models;
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
        protected Result<T>  RunQuery<T>(Func<T> func, string error)
        {
            try
            {
                var result = func();
                return Result.OK(result);
            }
            catch (Exception e)
            {
                // Console.WriteLine(e);
                return Result.Fail<T>(error);
            }
        }
    }
}
