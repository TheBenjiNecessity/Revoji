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
            var keyIssuer = "revoji.com";

            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

            var certificate = certificates[0]; //TODO: check for null

            services.AddIdentityServer()
                    .AddSigningCredential(certificate)
                    .AddInMemoryApiResources(Config.GetApiResources())
					.AddInMemoryClients(Config.GetClients())
					.AddResourceOwnerValidator<ResourceOwnerPasswordValidator>();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			        .AddJwtBearer(options => 
			{
				options.Authority = "http://revvr1.us-west-2.elasticbeanstalk.com";//TODO change to variable
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
