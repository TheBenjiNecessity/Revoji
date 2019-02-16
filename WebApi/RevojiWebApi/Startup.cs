using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography.X509Certificates;

namespace RevojiWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            AppSettings.Configuration = configuration; //this feels wrong
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //var keyIssuer = "revoji.us-west-2.elasticbeanstalk.com";

            X509Certificate2 certificate = null;
            using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                var thumbprint = "33 d3 55 64 03 09 7d b1 34 8d 59 39 94 e6 9f c0 0d 13 eb e3";
                var password = "owljofyguqua";

                certStore.Open(OpenFlags.ReadOnly);
                var certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    thumbprint,
                    false
                    );

                if (certCollection.Count > 0)
                {
                    certificate = new X509Certificate2(certCollection[0].GetRawCertData(), password);
                }
            }

            services.AddIdentityServer()
                    //.AddDeveloperSigningCredential()
                    .AddSigningCredential(certificate)
                    .AddInMemoryApiResources(Config.GetApiResources())
					.AddInMemoryClients(Config.GetClients())
					.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			        .AddJwtBearer(options => 
			{
				options.Authority = "http://revoji.us-west-2.elasticbeanstalk.com";//TODO change to variable
				options.Audience = "api";
				options.RequireHttpsMetadata = false;
			});

			services.AddAuthorization();

			services.AddCors(options =>
			{
			    options.AddPolicy("CorsPolicy",
			        builder => builder.AllowAnyOrigin()
			        .AllowAnyMethod()
			        .AllowAnyHeader()
			        .AllowCredentials());
			});

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            services.AddMvc();
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			//if (env.IsDevelopment())
			//{
			//	app.UseDeveloperExceptionPage();
			//}

			app.UseAuthentication()
			   .UseIdentityServer();

			app.UseCors("CorsPolicy");
            
			//app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials());//TODO: only allow certain origins?
			app.UseMvc();

            app.UseDatabaseErrorPage();

            app.UseDeveloperExceptionPage();
        }
    }
}
