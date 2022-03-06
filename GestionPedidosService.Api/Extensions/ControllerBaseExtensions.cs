using Microsoft.AspNetCore.Mvc;

namespace GestionPedidosService.Api.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static ObjectResult CustomProblem(this ControllerBase controller, int statusCode, object errorModel)
        {
            return new ObjectResult(errorModel)
            {
                StatusCode = statusCode
            };
        }
    }
}
