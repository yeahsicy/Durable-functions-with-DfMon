using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DurableFunctionsMonitor.DotNetBackend;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(FunctionApp_durable_fan_out_and_in.Startup))]
namespace FunctionApp_durable_fan_out_and_in
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            DfmEndpoint.Setup(new DfmSettings { DisableAuthentication = true });
        }
    }
}
