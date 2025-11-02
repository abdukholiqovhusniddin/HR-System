using Presentation.Middlewares;

namespace Presentation.Extensions;
public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseAppMiddlewares(this IApplicationBuilder app)
    {
        app.UseFileServer();

        app.UseCustomExceptionHandler();
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}
