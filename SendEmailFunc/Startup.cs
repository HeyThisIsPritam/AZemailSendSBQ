using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(SendEmailFunc.Startup))]
namespace SendEmailFunc
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOptions<SmtpConfiguration>().Configure<IConfiguration>((smtpConfigurationSettings, configuration) =>
            {
                configuration.GetSection(nameof(SmtpConfiguration)).Bind(smtpConfigurationSettings);
            });
            builder.Services.AddSingleton<IEmailSender, EmailProcessor>();
        }
    }
}
