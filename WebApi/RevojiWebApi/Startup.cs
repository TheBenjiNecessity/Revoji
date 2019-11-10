using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using Amazon.S3;
using RevojiWebApi.Services;
using Microsoft.Extensions.Logging;

namespace RevojiWebApi
{
    public class Startup
    {
        const string CorsPolicyIdentifier = "CorsPolicy";

        private readonly IConfiguration _configuration;
        private readonly ILogger logger;

        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;

            AppSettings.Configuration = configuration; //this feels wrong
            AppSettings.CurrentEnvironment = environment;

            AWSFileUploader.s3Client = configuration.GetAWSOptions().CreateServiceClient<IAmazonS3>();

            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;

            this.logger = logger;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string jwtBearerAuthority = "http://revoji.us-west-2.elasticbeanstalk.com";

            logger.LogInformation("ConfigureServices");

            //if (AppSettings.CurrentEnvironment.IsProduction())
            //{
                logger.LogInformation("IsProduction");

                X509Certificate2 certificate = null;
                using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
                {
                    var thumbprint = "1a fe 7f 14 eb 82 47 75 23 52 4d 4d 23 6c 4c 1c 4f ac 64 ff";
                    var password = "owljofyguqua"; // TODO: should be gotten from external config file

                    certStore.Open(OpenFlags.ReadOnly);
                    var certCollection = certStore.Certificates.Find(
                        X509FindType.FindByThumbprint,
                        thumbprint,
                        false
                        );

                    if (certCollection.Count > 0)
                    {
                        logger.LogInformation("Has Certificate");
                        certificate = certCollection[0];
                    }
                }

                services.AddIdentityServer()
                        .AddSigningCredential(certificate)
                        .AddInMemoryApiResources(Config.GetApiResources())
                        .AddInMemoryClients(Config.GetClients())
                        .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();
            //}
            //else
            //{
            //    logger.LogInformation("IsDevelopment");

            //    services.AddIdentityServer()
            //        .AddDeveloperSigningCredential()
            //        .AddInMemoryApiResources(Config.GetApiResources())
            //        .AddInMemoryClients(Config.GetClients())
            //        .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

            //    jwtBearerAuthority = "http://localhost:5001"; // TODO: Get from config file
            //}


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => 
            {
                options.Authority = jwtBearerAuthority;
                options.Audience = "api";
                options.RequireHttpsMetadata = false;
            });

            services.AddAuthorization();

            services.AddCors(options => // TODO: configure CORS properly
            {
                options.AddPolicy(CorsPolicyIdentifier,
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.AddMvc();

            services.AddDefaultAWSOptions(_configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication()
               .UseIdentityServer();

            app.UseCors(CorsPolicyIdentifier);

            app.UseMvc();

            loggerFactory.AddAWSProvider(_configuration.GetAWSLoggingConfigSection());

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
