using DAL.Data;
using BLL.Services;
using BLL.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using DAL.Entities;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BLL.Settings;
using Microsoft.AspNetCore.Http;
using GameStore.Helper;
using GameStore.Interfaces;
using DAL.Interfaces;
using DAL.Repository;
using Amazon.S3;
using Amazon.Runtime;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace GameStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

       
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddControllers();

            services.AddResponseCompression(options => {
                options.EnableForHttps = true;                             
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddMemoryCache();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.Configure<JwtSettings>(Configuration.GetSection("Jwt"));
            var jwtSettings = Configuration.GetSection("Jwt").Get<JwtSettings>();

            services.Configure<EmailSettings>(Configuration.GetSection("Email"));
            services.Configure<AwsSettings>(Configuration.GetSection("AWS"));

            services.AddDefaultAWSOptions(Configuration.GetAWSOptions("AWS"));
            services.AddSingleton<IS3Service, S3Service>();
            services.AddAWSService<IAmazonS3>();
           
            var dataAssemblyName = typeof(ApplicationDbContext).Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options => 
            {               
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),x => x.MigrationsAssembly(dataAssemblyName));
            });
                                                     

            services.AddIdentity<User, Role>(options =>
            {                               
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan =
                TimeSpan.FromMinutes(1d);
                options.Lockout.MaxFailedAccessAttempts = 5;
            })               
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmailService, EmailService>();            
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ILogService, LogService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddTransient<IUserHelper, UserHelper>();
            

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Game Store", Version = "v1" });    
            });
            services.AddAutoMapper(typeof(Startup));
            

            services
                .AddAuthorization(options =>
                {
                    
                })
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");              
                app.UseHsts();
            }   
            
            app.UseHttpsRedirection();
            app.UseResponseCompression();
            app.UseRouting();           

            app.UseAuthentication();
            app.UseAuthorization();                       

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            CreateRoles(services).Wait();
        }
        
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
          
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            
            string[] roleNames = { "Admin", "User" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                   
                    roleResult = await _roleManager.CreateAsync(new Role(roleName));
                }
            }                             
        }
      
    }
}
