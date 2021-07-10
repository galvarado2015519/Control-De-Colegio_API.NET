using ApiControlDeColegio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiControlDeColegio.Helpers
{
    public class ErrorFilterException : ExceptionFilterAttribute
    {
        private ApiResponse response = null;
        public ErrorFilterException()
        {

        }
        public override void OnException(ExceptionContext context)
        {
            if(context.HttpContext.Response.StatusCode == 400){
                response = new ApiResponse() {TipoError = "Error de negocio", HttpStatusCode = "400", Mensaje = context.Exception.Message};
                context.HttpContext.Response.StatusCode = 400;
            }else{
                response = new ApiResponse() {TipoError = "Error de servicio", HttpStatusCode = "503", Mensaje = context.Exception.Message};
                context.HttpContext.Response.StatusCode = 503;
            }
            context.Result = new JsonResult(response);
            base.OnException(context);
        }
    }
}