using EntityConfigurationBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using School.Core.Common.Entities;
using School.Core.Repository;
using School.Web.MappingMapper;
using SchoolCore;
using SchoolCore.Entities;
using SchoolCore.Service;

namespace School.Web
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
            services.AddDbContext<BaseDbContext>(options => options.UseMySql(Configuration.GetConnectionString("BaseDbContext"), MySqlServerVersion.LatestSupportedServerVersion));

            services.AddRazorPages();
            services.AddMvc();
            //AutoMapper这个以来的包：AutoMapper.Extensions.Microsoft.DependencyInjection
            services.AddAutoMapper(typeof(MapperProfile));
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.TryAddScoped<ISchoolContracts, SchoolService>();
            //services.TryAddScoped<IRepository<User>, Repository<User>>();
            //services.TryAddScoped<IRepository<UserRole>, Repository<UserRole>>();
            //services.TryAddScoped<IRepository<Course>, Repository<Course>>();
            //services.TryAddScoped<IRepository<Academic>, Repository<Academic>>();
            //services.TryAddScoped<IRepository<UserCourse>, Repository<UserCourse>>();
            //services.TryAddScoped<IRepository<ReportCards>, Repository<ReportCards>>();


            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
            app.UseSwagger();
            //启用中间件服务队swagger - ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
