using System;
using System.IO;
using AutoMapper;
using CustomersTestApi.Model;
using CustomersTestApi.Persistance;
using CustomersTestApi.Persistance.Repositories;
using CustomersTestApi.Support;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CustomersTestApi
{
  /// <summary>
  /// 
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <value>
    /// The configuration.
    /// </value>
    private IConfiguration Configuration { get; }

    /// <summary>
    /// The env
    /// </summary>
    private readonly IWebHostEnvironment _env;

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    /// <param name="env">The env.</param>
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      _env = env;
    }

    /// <summary>
    /// Configures the services.
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    /// <param name="services">The services.</param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<CustomersContext>(options =>
      {
        options.UseSqlServer(Configuration.GetConnectionString("Db"));
      });

      if (_env.IsDevelopment())
      {
        services.AddDistributedMemoryCache();
      }
      else 
      {
        services.AddDistributedRedisCache(options =>
        {
          options.Configuration = Configuration.GetConnectionString("Redis");
        });
      }

      var mappingConfig = new MapperConfiguration(mc =>
      {
        mc.AddProfile(new MappingProfile());
      });

      var mapper = mappingConfig.CreateMapper();
      services.AddSingleton(mapper);

      services.AddScoped<ICustomersRepository, CustomersRepository>();

      services.Configure<GeneralConf>(Configuration.GetSection("GeneralConf"));

      services.AddMediatR(typeof(Startup));

      services.AddCors(options =>
      {
        options.AddDefaultPolicy(builder =>
         builder.SetIsOriginAllowed(_ => true)
          .AllowAnyMethod()
          .AllowAnyHeader()
          .AllowCredentials());
      });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
          Title = $"API {Configuration.GetSection("GeneralConf")["AppTitle"]}",
          Version = Configuration.GetSection("GeneralConf")["AppVersion"],
          Description = Configuration.GetSection("GeneralConf")["AppDescription"]
        });

        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        const string docsFileName = "CustomersTestApi.XML";
        var commentsFile = Path.Combine(baseDirectory ?? throw new InvalidOperationException(), docsFileName);

        c.IncludeXmlComments(commentsFile);

        if (!Convert.ToBoolean(Configuration.GetSection("GeneralConf")["AppAuthorization"])) return;
        
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
          Description = "Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey
        });


        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            new string[] {}
          }
        });
      });

      services.AddControllers();
    }

    /// <summary>
    /// Configures the specified application.
    /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app">The application.</param>
    /// <param name="env">The env.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseAuthentication();

      app.UseSerilogRequestLogging();

      app.UseRouting();

      app.UseAuthorization();

      app.UseSwagger();

      app.UseSwaggerUI(s =>
      {
        s.RoutePrefix = "help";
        s.SwaggerEndpoint("../swagger/v1/swagger.json", $"{ Configuration.GetSection("GeneralConf")["AppTitle"]}");
        s.InjectStylesheet("../css/swagger.min.css");
      });

      app.UseCors();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

    }
  }
}
