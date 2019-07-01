using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json;

using survey.data;
using survey.services;

namespace survey
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DI
            services.AddScoped<FakeManager>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyGenerator, SurveyGenerator>();

            //Add DbContext
            if (string.IsNullOrWhiteSpace(Configuration.GetConnectionString("Sql")))
                services.AddDbContext<SurveyContext>(o => o.UseInMemoryDatabase(nameof(SurveyContext)));
            else
                services.AddDbContext<SurveyContext>(o => o.UseSqlServer(Configuration.GetConnectionString("Sql")));

            services.AddLogging();
            services.AddMemoryCache();
            services.AddHealthChecks();

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(o =>
                {
                    o.SerializerSettings.Formatting = Formatting.Indented;
                    o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                services.GetRequiredService<FakeManager>()
                    .UseFakeContext()
                    .Wait();
            }

            app.UseHealthChecks("/ping");
            app.UseMvc();
        }
    }
}
