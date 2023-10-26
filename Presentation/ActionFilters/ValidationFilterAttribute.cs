using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.ActionFilters
{
    public class ValidationFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            var action = context.RouteData.Values["action"];

            //Dto
            var param = context.ActionArguments
                .SingleOrDefault(p => p.Value.ToString().Contains("Dto")).Value;

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null." +
                    $"Controller : {controller}" +
                    $"Action : {action}");
                return; //400
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
    }

    //Kodun Aciklamasi
    /*
    Bu kod, ozel bir Action Filter Attribute icerir
    - HTTP istegi islenmeden once calisan `OnActionExecuting` yontemi icerir
    - Ilgili controller ve action bilgilerini cikartir
    - Eylem argumanlari arasinda "Dto" kelimesini iceren bir argumani bulmaya calisir
    - Eger "Dto" iceren arguman bulunamazsa, istegi basarisiz ilan ederek HTTP 400 Bad Request yaniti dondurur
    - Model dogrulama islemi sirasinda hatalar mevcutsa, HTTP 422 Unprocessable Entity yaniti dondurur
    */
}
