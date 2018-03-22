using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Wonders.Api;

namespace Wonders.IntegrationTests
{
    public class WondersApiTestServer
    {
        private static TestServer _server;
        public HttpClient Client { get; private set; }

        public void Init()
        {
            var apiStartupAssembly = typeof(Startup).GetTypeInfo().Assembly;
            var apiContentRoot = GetProjectPath(apiStartupAssembly);

            var builder = new WebHostBuilder()
                .UseContentRoot(apiContentRoot)
                .ConfigureServices(services =>
                {
                    var manager = new ApplicationPartManager();
                    manager.ApplicationParts.Add(new AssemblyPart(apiStartupAssembly));
                    manager.FeatureProviders.Add(new ControllerFeatureProvider());
                    services.AddSingleton(manager);
                })
                .UseStartup(typeof(TestStartup));

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost");
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }

        private static string GetProjectPath(Assembly startupAssembly)
        {
            var projectName = startupAssembly.GetName().Name;
            var applicationBasePath = PlatformServices.Default.Application.ApplicationBasePath;
            var directoryInfo = new DirectoryInfo(applicationBasePath);
            do
            {
                var solutionFileInfo = new FileInfo(Path.Combine(directoryInfo.FullName, "Wonders.sln"));
                if (solutionFileInfo.Exists)
                {
                    return Path.GetFullPath(Path.Combine(directoryInfo.FullName, projectName));
                }

                directoryInfo = directoryInfo.Parent;
            }
            while (directoryInfo?.Parent != null);
            throw new ArgumentException($"Solution root could not be located using application root {applicationBasePath}.");
        }
    }
}
