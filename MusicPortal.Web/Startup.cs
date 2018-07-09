using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using MusicPortal.Facade.Facades;
using MusicPortal.Facade.Interfaces;
using MusicPortal.ViewModels.MappingConfiguration;

namespace MusicPortal.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            var apiKey = Configuration.GetValue<string>("LastFmApiKey");
            var apiSecret = Configuration.GetValue<string>("LastFmApiSecret");

            var mapperConfiguration = MappingConfiguration.GetConfiguration();
            MappingConfiguration.Configure();

            services.AddDbContext<ApplicationContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("MusicPortal.DAL"))
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddScoped<IMusicPortalClient, MusicPortalClient>();

            services.AddSingleton(ctx => mapperConfiguration.CreateMapper());
            services.AddSingleton(conf => Configuration);

            services.AddMvc();
            
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp";
                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });
        }
    }
}