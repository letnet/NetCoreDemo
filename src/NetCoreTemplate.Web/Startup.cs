using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetCoreTemplate.Infrastructure;
using NetCoreTemplate.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace NetCoreTemplate.Web
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
            //自动创建映射
            services.AddAutoMapper();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //自动注入
            services.AddAutoInject();
            services.AddMiniProfiler();

            //验证签名密匙
            string jwtsecret = Configuration["JWTSecret"].ToString();
            //使用Jwt Bearer验证授权
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(p =>
                    {
                        p.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuer = "Login",
                            ValidAudience = "User",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtsecret)),
                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.Zero
                        };
                    });

            services.AddControllersWithViews()
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });
            //使用代理服务器
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            }); 

            // 注册Swagger服务
            services.AddSwaggerGen(c =>
            {
                // 添加文档信息
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NetCoreTemplate Api", Version = "v1" });

                //载入XML文件信息
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //使用代理服务器
            app.UseForwardedHeaders();
            //设置请求基路径
            app.UsePathBase(Configuration.GetValue<string>("PathBase"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStateAutoMapper();
            app.UseMiniProfiler();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "NetCoreTemplate V1");
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
