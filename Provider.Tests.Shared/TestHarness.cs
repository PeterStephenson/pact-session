using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Provider.Data;

namespace Provider.Tests.Shared
{
    public class TestHarness : WebApplicationFactory<Startup>
    {
        public GivenSteps Given => Services.GetRequiredService<GivenSteps>();
        
        public ThenSteps Then => Services.GetRequiredService<ThenSteps>();
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(sc =>
            {
                sc.AddSingleton<GivenSteps>();
                sc.AddSingleton<ThenSteps>();

                sc.AddSingleton<TestDvdRepository>();
                sc.AddSingleton<IDvdRepository>(sp => sp.GetRequiredService<TestDvdRepository>());
            });
        }
    }
}