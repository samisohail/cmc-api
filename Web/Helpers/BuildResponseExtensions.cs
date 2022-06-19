using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMC.Models;
using Microsoft.AspNetCore.Mvc;

namespace Web.Helpers
{
    public static class BuildResponseExtensions
    {
        public static IActionResult BuildResponse(this Result result)
        {
            var error = IsError(result);

            return error ?? new OkResult();
        }

        public static IActionResult BuildResponse<T>(this Result<T> result)
        {
            var error = IsError(result);

            return error ?? new OkObjectResult(result.Value);
        }

        private static IActionResult IsError(Result result)
        {
            // some error occurred
            if (!result.Success && result.NotFoundToModify != true) 
                return new BadRequestObjectResult(result.Error);

            // an expected item not found
            if (!result.Success && result.NotFoundToModify == true) 
                return new NotFoundObjectResult(result.Error);

            // no error
            return null;
        }

    }
}
