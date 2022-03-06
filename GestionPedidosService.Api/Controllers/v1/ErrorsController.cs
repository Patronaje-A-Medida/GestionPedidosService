﻿using GestionPedidosService.Api.Extensions;
using GestionPedidosService.Api.Utils;
using GestionPedidosService.Business.Handlers;
using GestionPedidosService.Persistence.Handlers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Net;
using static GestionPedidosService.Domain.Utils.ErrorsUtil;

namespace GestionPedidosService.Api.Controllers.v1
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ControllerBase
    {
        [Route("error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                return NotFound();
            }

            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;
            ErrorDevDetail errorResponse;

            if (exception is RepositoryException repoEx)
            {
                errorResponse = new ErrorDevDetail
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    errorCode = repoEx.ErrorCode,
                    message = repoEx.RepositoryMessage,
                    systemMessage = repoEx.Message
                };
            }
            else if (exception is ServiceException servcEx)
            {
                errorResponse = new ErrorDevDetail
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    errorCode = servcEx.ErrorCode,
                    message = servcEx.ServiceMessage,
                    systemMessage = servcEx.Message
                };
            }
            else
            {
                errorResponse = new ErrorDevDetail
                {
                    statusCode = (int)HttpStatusCode.BadRequest,
                    errorCode = ErrorsCode.GENERIC_ERROR,
                    message = ErrorMessages.GENERIC_ERROR_400,
                    systemMessage = exception.Message
                };
            }

            return this.CustomProblem(errorResponse.statusCode, errorResponse);

        }

        [Route("error")]
        public IActionResult HandleError()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()!.Error;
            ErrorDetail errorResponse;

            if (exception is RepositoryException repoEx)
            {
                errorResponse = new ErrorDetail
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    errorCode = repoEx.ErrorCode,
                    message = repoEx.RepositoryMessage,
                };
            }
            else if (exception is ServiceException servcEx)
            {
                errorResponse = new ErrorDetail
                {
                    statusCode = (int)HttpStatusCode.InternalServerError,
                    errorCode = servcEx.ErrorCode,
                    message = servcEx.ServiceMessage,
                };
            }
            else
            {
                errorResponse = new ErrorDetail
                {
                    statusCode = (int)HttpStatusCode.BadRequest,
                    errorCode = ErrorsCode.GENERIC_ERROR,
                    message = ErrorMessages.GENERIC_ERROR_400,
                };
            }

            return this.CustomProblem(errorResponse.statusCode, errorResponse);
        }
    }
}
