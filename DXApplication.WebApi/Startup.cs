using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.AspNetCore.WebApi;
using DevExpress.ExpressApp.Core;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Security.Authentication;
using DevExpress.ExpressApp.Security.Authentication.ClientServer;
using DevExpress.ExpressApp.WebApi.Services;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DXApplication.Blazor.BusinessObjects;
using DXApplication.Module.BusinessObjects.Main;
using DXApplication.WebApi.Core;
using DXApplication.WebApi.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OData;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace DXApplication.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddSingleton<IXpoDataStoreProvider>((serviceProvider) =>
            {
                string connectionString = null;
                if (Configuration.GetConnectionString("ConnectionString") != null)
                {
                    connectionString = Configuration.GetConnectionString("ConnectionString");
                }
#if EASYTEST
                if(Configuration.GetConnectionString("EasyTestConnectionString") != null) {
                    connectionString = Configuration.GetConnectionString("EasyTestConnectionString");
                }
#endif
                return XPObjectSpaceProvider.GetDataStoreProvider(connectionString, null, true);
            })
            .AddScoped<IAuthenticationTokenProvider, JwtTokenProviderService>()
            .AddScoped<IObjectSpaceProviderFactory, ObjectSpaceProviderFactory>()
            .AddSingleton<IWebApiApplicationSetup, WebApiApplicationSetup>();
        PasswordCryptographer.SupportLegacySha512 = true;
        services.AddXafAspNetCoreSecurity(Configuration, options =>
        {
            options.RoleType = typeof(PermissionPolicyRole);
            // ApplicationUser descends from PermissionPolicyUser and supports the OAuth authentication. For more information, refer to the following topic: https://docs.devexpress.com/eXpressAppFramework/402197
            // If your application uses PermissionPolicyUser or a custom user type, set the UserType property as follows:
            options.UserType = typeof(ApplicationUser);
            // ApplicationUserLoginInfo is only necessary for applications that use the ApplicationUser user type.
            // If you use PermissionPolicyUser or a custom user type, comment out the following line:
            options.UserLoginInfoType = typeof(ApplicationUserLoginInfo);
            options.Events.OnSecurityStrategyCreated = securityStrategy => ((SecurityStrategy)securityStrategy).RegisterXPOAdapterProviders();
            options.SupportNavigationPermissionsForTypes = false;
        })
        .AddAuthenticationStandard(options =>
        {
            options.IsSupportChangePassword = true;
        })
        .AddAuthenticationProvider<CustomAuthenticationProvider>();
        const string customBearerSchemeName = "CustomBearer";
        var authentication = services.AddAuthentication(customBearerSchemeName);
        authentication
            .AddJwtBearer(customBearerSchemeName, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = Configuration["Authentication:Jwt:Issuer"],
                    //ValidAudience = Configuration["Authentication:Jwt:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Jwt:IssuerSigningKey"]))
                };
            });
        //Configure OAuth2 Identity Providers based on your requirements. For more information, see
        //https://docs.devexpress.com/eXpressAppFramework/402197/task-based-help/security/how-to-use-active-directory-and-oauth2-authentication-providers-in-blazor-applications
        //https://developers.google.com/identity/protocols/oauth2
        //https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-oauth2-auth-code-flow
        //https://developers.facebook.com/docs/facebook-login/manually-build-a-login-flow
        authentication.AddMicrosoftIdentityWebApi(Configuration, configSectionName: "Authentication:AzureAd");

        services.AddAuthorization(options =>
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                JwtBearerDefaults.AuthenticationScheme,
                customBearerSchemeName)
                    .RequireAuthenticatedUser()
                    .RequireXafAuthentication()
                    .Build();
        });

        services
            .AddXafWebApi(Configuration, options =>
            {
                // Make your business objects available in the Web API and generate the GET, POST, PUT, and DELETE HTTP methods for it.
                // options.BusinessObject<YourBusinessObject>();
                options.BusinessObject<ApplicationUser>();
                options.BusinessObject<ApplicationUserLoginInfo>();
                options.BusinessObject<ChuNhiem_CanBoQuanLy>();
                options.BusinessObject<DeTaiDuAn_KHCN>();
                options.BusinessObject<DeXuatNhiemVu_KHCN>();
                options.BusinessObject<FileDuLieu>();
                options.BusinessObject<HoiDongThamDinh>();
                options.BusinessObject<KinhPhiThucHien>();
                options.BusinessObject<Noidung_DeTai>();
                options.BusinessObject<NhaKhoaHoc>();
                options.BusinessObject<ThanhVienHoiDongTD>();
                options.BusinessObject<ThaoLuan>();
            })
            .AddXpoServices();
        services
            .AddControllers()
            .AddOData((options, serviceProvider) =>
            {
                options
                    .AddRouteComponents("api/odata", new EdmModelBuilder(serviceProvider).GetEdmModel())
                    .EnableQueryFeatures(100);
            });

        services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "DXApplication API",
                Version = "v1",
                Description = @"Use AddXafWebApi(options) in the DXApplication.WebApi\Startup.cs file to make Business Objects available in the Web API."
            });
            c.AddSecurityDefinition("JWT", new OpenApiSecurityScheme()
            {
                Type = SecuritySchemeType.Http,
                Name = "Bearer",
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme() {
                            Reference = new OpenApiReference() {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "JWT"
                            }
                        },
                        new string[0]
                    },
            });
            var azureAdAuthorityUrl = $"{Configuration["Authentication:AzureAd:Instance"]}{Configuration["Authentication:AzureAd:TenantId"]}";
            c.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows()
                {
                    AuthorizationCode = new OpenApiOAuthFlow()
                    {
                        AuthorizationUrl = new Uri($"{azureAdAuthorityUrl}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"{azureAdAuthorityUrl}/oauth2/v2.0/token"),
                        Scopes = new Dictionary<string, string> {
                            // Configure scopes corresponding to https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-configure-app-expose-web-apis
                            { @"[Enter the scope name in the DXApplication.WebApi\Startup.cs file]", @"[Enter the scope description in the DXApplication.WebApi\Startup.cs file]" }
                        }
                    }
                }
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                {
                    new OpenApiSecurityScheme {
                        Name = "OAuth2",
                        Scheme = "OAuth2",
                        Reference = new OpenApiReference {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "OAuth2"
                        },
                        In = ParameterLocation.Header
                    },
                    new string[0]
                }
            });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DXApplication WebApi v1");
                c.OAuthClientId(Configuration["Authentication:AzureAd:ClientId"]);
                c.OAuthUsePkce();
            });
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. To change this for production scenarios, see: https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseRequestLocalization();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
