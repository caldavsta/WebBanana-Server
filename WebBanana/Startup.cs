using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WebBanana
{
    public class Startup
    {
        private bool LOGGING_ENABLED = false;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            VoiceMeeterConnector.Instance.Login();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            if (LOGGING_ENABLED)
            {
                loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                loggerFactory.AddDebug(LogLevel.Debug);
                var logger = loggerFactory.CreateLogger("Startup");
                logger.LogWarning("Logger configured!");
            }

            app.UseMvc();
        }

        private void OnShutdown()
        {
            if (VoiceMeeterConnector.Instance != null)
            {
                VoiceMeeterConnector.Instance.Logout();
            }
        }
    }
}
