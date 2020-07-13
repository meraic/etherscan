using AutoMapper;
using Btc.Etherscan.Common.Core.Extensions;
using Btc.Etherscan.Common.Core.Middleware;
using Btc.Etherscan.Common.Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Collections.Generic;
using System.Reflection;

namespace Btc.Etherscan.Common.Core
{
    /// <summary>
    /// Application startup base - provides the most commonly used startup options and can be extended/customized if needed
    /// </summary>
    public abstract class StartupBase
    {
        /// <summary>
        /// Configuration File References
        /// </summary>
        public IConfiguration Configuration { get; private set; }

        /// <summary>
        /// Hosting environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Name of the consuming application. Defaults to: `Assembly.GetName().Name`
        /// </summary>
        protected virtual string ApplicationName { get; }

        /// <summary>
        /// The calling assembly. Should be: `GetType().Assembly`
        /// </summary>
        protected abstract Assembly Assembly { get; }

        /// <summary>
        /// Consuming application description. Blank by default
        /// </summary>
        protected virtual string ApplicationDescription { get; } = string.Empty;

        /// <summary>
        /// Defines how null values will be handled by JsonSerializer. Used in <see cref="ConfigureJsonOptions(IMvcBuilder)"/>
        /// </summary>
        protected virtual NullValueHandling NullValueHandling { get; } = NullValueHandling.Ignore;

        /// <summary>
        /// Defines how property names will be handled by JsonSerializer. Used in <see cref="ConfigureJsonOptions(IMvcBuilder)"/>.
        /// 
        /// <para>Defaults to <see cref="CamelCasePropertyNamesContractResolver"/></para>
        /// </summary>
        protected virtual IContractResolver ContractResolver { get; } = new CamelCasePropertyNamesContractResolver();

        /// <summary>
        /// Defines a collection of converters that specifies how various types of field wil be handled by JsonSerializer. 
        /// Used in <see cref="ConfigureJsonOptions(IMvcBuilder)"/>.
        /// 
        /// <para>Defaults to empty list.</para>
        /// <para>Common addition is to add <see cref="StringEnumConverter"/> with <see cref="StringEnumConverter.NamingStrategy"/> being <see cref="CamelCaseNamingStrategy"/></para>
        /// </summary>
        protected virtual ICollection<JsonConverter> JsonConverters { get; } = new List<JsonConverter>();

        /// <summary>
        /// This method gets called by the runtime
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        protected StartupBase(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            ApplicationName = Assembly.GetName().Name;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Show dev info where appropriate
            if (env.IsDevelopment() || env.IsDev())
            {
                app.UseDeveloperExceptionPage();

                // Allow Cross Origin Requests
                app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            }
            else
            {
                // Handles exceptions and generates a custom response body
                app.UseExceptionHandler("/errors/500");
                app.UseHsts();
            }

            // Include Logging Middleware
            app.UseMiddleware<LoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            ConfigureConfiguration(services);
            ConfigureDatabase(services);
            ConfigureHttpOptions(services);
            ConfigureUserIdentity(services);
            ConfigureAutoMapper(services);
            ConfigureApiVersioning(services);
            ConfigureLogging(services);
            ConfigureJsonOptions(ConfigureMvc(services));
            
            // Abstract function call to be implemented on consuming apps to configure their services
            ConfigureCustomServices(services);
        }

        /// <summary>
        /// Adds the relevant appsettings file based on the current environment, builds and returns.
        /// 
        /// Currently supports the following environments:
        /// 
        /// <list type="bullet">
        ///     <item><description>Development</description></item>
        ///     <item><description>DEV</description></item>
        ///     <item><description>UAT</description></item>
        ///     <item><description>PRD</description></item>
        /// </list>
        /// </summary>
        protected virtual void ConfigureConfiguration(IServiceCollection services)
        {
            var configBuilder = new ConfigurationBuilder();

            // Always include default appsettings.json file
            configBuilder.AddJsonFile("appsettings.json", false, true);

            // Include the per-environment appsettings.?.json file (Expanded if to allow for renaming in dev)
            if (Environment.IsDevelopment())
            {
                configBuilder.AddJsonFile("appsettings.Development.json", false, true);
            }
            else if (Environment.IsDev())
            {
                configBuilder.AddJsonFile("appsettings.DEV.json", false, true);
            }
            else if (Environment.IsUat())
            {
                configBuilder.AddJsonFile("appsettings.UAT.json", false, true);
            }
            else if (Environment.IsPrd())
            {
                configBuilder.AddJsonFile("appsettings.PRD.json", false, true);
            }

            // Build and store configuration
            Configuration = configBuilder.AddEnvironmentVariables().Build();

            services.AddSingleton(Configuration);
        }

        /// <summary>
        /// Configures Database, add database contexts
        /// </summary>
        /// <remarks>BuildConnectionString(bool) function is available to get the recommended connection string</remarks>
        /// <param name="services"></param>
        protected virtual void ConfigureDatabase(IServiceCollection services)
        {
            // Not applicable here but we can add DbCotext passes through as TContext as generic parameter in StartupBase
            /* 
            services.AddDbContext<TContext>(
                options => options.UseSqlServer(BuildConnectionString(false),   -- if using Sql Server Database
                builder => builder.UseNetTopologySuite()));
            */
        }

        /// <summary>
        /// Configures http client, context accessor and forwarded header options
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureHttpOptions(IServiceCollection services)
        {
            // Add IHttpClientInjector
            services.AddHttpClient();

            // Registers the default implementation of IHttpContextAccessor to allow HttpContext to be used outside of controllers
            services.AddHttpContextAccessor();

            // Configure the service to read XForwardedFor and Proto headers
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders =
                    ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
        }

        /// <summary>
        /// Configures the user identity in Name - Email - AuthenticationType format
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureUserIdentity(IServiceCollection services)
        {
            services.AddTransient((provider) =>
            {
                var contextAccessor = provider.GetService<IHttpContextAccessor>();
                var user = contextAccessor.HttpContext?.User;
                var email = user?.GetEmail();
                var name = $"{user?.GetSurname()}, {user?.GetFirstName()}";
                return new UserIdentity(name, email, user?.Identity?.AuthenticationType);
            });
        }

        /// <summary>
        /// Configures AutoMapper
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureAutoMapper(IServiceCollection services)
        {
            // Goes through the assembly and looks for all types that extend AutoMapper.Profile to figure out mappings
            services.AddAutoMapper(GetType().Assembly);
        }

        /// <summary>
        /// Configures API versioning using "1.0" as default API version
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureApiVersioning(IServiceCollection services)
        {
            // More info: https://www.hanselman.com/blog/ASPNETCoreRESTfulWebAPIVersioningMadeEasy.aspx
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }

        /// <summary>
        /// Configures logging based on the configuration settings
        /// </summary>
        /// <param name="services"></param>
        protected virtual void ConfigureLogging(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration()
                            .ReadFrom.Configuration(Configuration)
                            .CreateLogger();
        }

        /// <summary>
        /// Abstract function call to be implemented on consuming app to configure services.
        /// 
        /// <para>This is where you should do all of your services DI registration</para>
        /// </summary>
        /// <param name="services"></param>
        protected abstract void ConfigureCustomServices(IServiceCollection services);

        /// <summary>
        /// Configures MVC and it's compatibility version
        /// </summary>
        /// <param name="services"></param>
        /// <returns>MVC builder</returns>
        protected virtual IMvcBuilder ConfigureMvc(IServiceCollection services)
        {
            return services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        /// <summary>
        /// Configures logging
        /// </summary>
        /// <param name="mvcBuilder">MVC builder</param>
        protected virtual void ConfigureJsonOptions(IMvcBuilder mvcBuilder)
        {
            mvcBuilder
                .AddNewtonsoftJson(options =>
                {
                    // Ignore null values by default
                    options.SerializerSettings.NullValueHandling = NullValueHandling;

                    options.SerializerSettings.ContractResolver = ContractResolver;

                    if (JsonConverters != null)
                    {
                        foreach (var jsonConverter in JsonConverters)
                        {
                            options.SerializerSettings.Converters.Add(jsonConverter);
                        }
                    }
                });
        }
    }
}
