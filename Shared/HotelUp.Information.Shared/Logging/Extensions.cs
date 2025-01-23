using HotelUp.Information.Shared.Logging.Seq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace HotelUp.Information.Shared.Logging;

internal static class Extensions
{
    internal static WebApplicationBuilder AddCustomLogging(this WebApplicationBuilder builder)
    {
        builder.Services.AddSeqLogging();

        builder.Host.UseSerilog((context, loggerConfiguration) =>
        {
            loggerConfiguration.WriteTo.Console();
            var seqOptions = context.Configuration.GetSection("Seq").Get<SeqOptions>()
                             ?? throw new NullReferenceException("Seq configuration is missing.");
            loggerConfiguration.WriteTo.Seq(seqOptions.ServerUrl);
        });
        return builder;
    }
}