using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace GestionPedidosService.Api.ApiConventions
{
    public class ApiVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNameSpace = controller.ControllerType.Namespace;
            var apiVersion = controllerNameSpace.Split(".").Last().ToLower();
            controller.ApiExplorer.GroupName = apiVersion;
        }
    }
}