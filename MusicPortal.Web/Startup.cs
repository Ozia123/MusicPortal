using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Repositories;
using MusicPortal.Web.Util;

namespace MusicPortal.Web {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            MapperConfiguration configMapper = new MapperConfiguration(
                cfg => { cfg.AddProfile(new AutoMapperProfile()); }
            );
            AutoMapperConfiguration.Configure();

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ApplicationContext"), b => b.MigrationsAssembly("MusicPortal.DAL")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddSingleton(ctx => configMapper.CreateMapper());
            services.AddSingleton(conf => Configuration);

            services.AddMvc();

            // In production, the Angular files will be served from this directory
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

                //if (env.IsDevelopment()) {
                //    spa.UseAngularCliServer(npmScript: "start");
                //}
            });
        }
    }
}