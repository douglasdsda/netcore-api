using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.Data;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace SmartSchool
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<SmartContext>(
          // context => context.UseSqlite(Configuration.GetConnectionString("Default"))
          context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"))
      );

      // services.AddSingleton<IRepository, Repository>();
      //  services.AddTransient<IRepository, Repository>();

      services.AddControllers()
              .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling =
                  Newtonsoft.Json.ReferenceLoopHandling.Ignore);

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
      services.AddScoped<IRepository, Repository>();

      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
      }).AddApiVersioning(options =>
      {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
      });

      var apiProvierDescription = services.BuildServiceProvider()
        .GetService<IApiVersionDescriptionProvider>();

      services.AddSwaggerGen(
          options =>
          {

            foreach (var description in apiProvierDescription.ApiVersionDescriptions)
            {
              options.SwaggerDoc(description.GroupName,
               new Microsoft.OpenApi.Models.OpenApiInfo()
               {
                 Title = "SmartShool",
                 Version = description.ApiVersion.ToString(),
                 TermsOfService = new Uri("http://SeusTermosUso.combr"),
                 Description = "A descri��o da WebApi do SmartSchool",
                 License = new Microsoft.OpenApi.Models.OpenApiLicense
                 {
                   Name = "SmartSchool License",
                   Url = new Uri("http://mit.com")
                 },
                 Contact = new Microsoft.OpenApi.Models.OpenApiContact
                 {
                   Name = "Douglas S",
                   Email = "douglas@gmail.com",
                   Url = new Uri("http://progrmadamente.com")
                 }
               }); ;

              var xmlComment = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
              var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComment);

              options.IncludeXmlComments(xmlCommentsFullPath);
            }
          }
      );
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app,
     IWebHostEnvironment env,
     IApiVersionDescriptionProvider apiVersionDescriptionProvider
     )
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseRouting();
      app.UseSwagger()
         .UseSwaggerUI(options =>
         {
           foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
           {
             options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
           }
             options.RoutePrefix = "";

         })
      ;

      // app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
