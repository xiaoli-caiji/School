using IdentityServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using School.Core.Repository;
using School.Web.MappingMapper;
using SchoolCore;
using SchoolCore.Service;

namespace School.Web
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
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
            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.TryAddScoped<ISchoolContracts, SchoolService>();
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins, builder =>
                 {
                     builder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed(_ => true)
                            .AllowCredentials();
                 });
            });
            //鉴别权限
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "api1");
                });
            });

            var builder = services.AddIdentityServer()
                    .AddInMemoryIdentityResources(Config.GetIdentityResources())
                    .AddProfileService<SchoolProfileService>()
                    //.AddInMemoryApiResources(Config.GetApis())
                    .AddInMemoryClients(Config.GetClients())
                    // .AddTestUsers(Config.GetUsers());
                    .AddResourceOwnerValidator<ResourceOwnerPasswordVaildator>()
                    .AddInMemoryApiScopes(Config.ApiScopes());

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:13001";
                    //options.RequireHttpsMetadata = false;
                    //options.Audience = "school";
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false
                    };
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

            app.UseCors(MyAllowSpecificOrigins);
            app.UseIdentityServer();          
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //身份验证
            app.UseAuthorization();


            app.UseSwagger();
            //启用中间件服务队swagger - ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
            //授权
            app.UseAuthentication();


        }
    }
}
