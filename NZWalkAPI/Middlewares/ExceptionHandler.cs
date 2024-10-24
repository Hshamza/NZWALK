using System.Net;

namespace NZWalkAPI.Middlewares
{
    public class ExceptionHandler
    {
        private readonly ILogger<ExceptionHandler> logger;
        private readonly RequestDelegate next;
       public  ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next) {

            this.logger = logger;
            this.next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await next(httpContext);
            }
            catch (Exception e)
            {
                var errorId = Guid.NewGuid().ToString();
                logger.LogError(e, $"{errorId} : {e.Message}");
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Somwthing went wrong"
                };

                await httpContext.Response.WriteAsJsonAsync(error);

            }
        }
    }
}
