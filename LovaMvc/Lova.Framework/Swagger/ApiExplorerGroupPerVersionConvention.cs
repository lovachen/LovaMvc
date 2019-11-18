using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast.Framework
{
    // ApiExplorerGroupPerVersionConvention.cs
    public class ApiExplorerGroupPerVersionConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            var controllerNamespace = controller.ControllerType.Namespace; 
            if (controllerNamespace.Contains(".Controllers"))
            {
                controllerNamespace = controllerNamespace.Replace(".Controllers", "");
                var apiVersion = controllerNamespace.Split('.').Last().ToLower();

                controller.ApiExplorer.GroupName = apiVersion;
            }
        }
    }
}
