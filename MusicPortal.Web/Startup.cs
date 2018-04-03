using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusicPortal.BLL.Interfaces;
using MusicPortal.BLL.Services;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<ITrackService, TrackService>();
            services.AddSingleton(ctx => configMapper.CreateMapper());

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
            }
            else {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
