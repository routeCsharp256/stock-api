using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.StockApi.Infrastructure.Middlewares;

namespace OzonEdu.StockApi.Infrastructure.StartupFilters
{
    internal class VersionStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                next(app);
            };
        }
    }
}