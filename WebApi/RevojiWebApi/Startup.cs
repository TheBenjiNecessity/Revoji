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
            X509Certificate2 certificate = null;
            using (var certStore = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                var thumbprint = "1a fe 7f 14 eb 82 47 75 23 52 4d 4d 23 6c 4c 1c 4f ac 64 ff";
                var password = "owljofyguqua";

                certStore.Open(OpenFlags.ReadOnly);
                var certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    thumbprint,
                    false
                    );

                if (certCollection.Count > 0)
                {
                    certificate = certCollection[0];
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
