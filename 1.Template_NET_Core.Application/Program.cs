using _0.Template_NET_Core.Common.Options;
using _1.Template_NET_Core.Application.Controllers.Validators;
using _1.Template_NET_Core.Application.Infrastructure.Filter;
using _1.Template_NET_Core.Application.Infrastructure.MapperProfiler;
using _1.Template_NET_Core.Application.Parameters;
using _2.Template_NET_Core.Services.Implements;
using _2.Template_NET_Core.Services.Infrastructure.MapperProfile;
using _2.Template_NET_Core.Services.Interface;
using _3.Template_NET_Core.Repositories.Cached;
using _3.Template_NET_Core.Repositories.Implement;
using _3.Template_NET_Core.Repositories.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration.AddJsonFile($"appsettings.json", true, true).AddJsonFile($"appsettings.{env.EnvironmentName}.json");

// Add services to the container.
 
builder.Services.AddMemoryCache();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ActionResultFilter>();
    options.Filters.Add<ExceptionFilter>();
}).AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Template API",
        Description = ""
    });

    options.AddSecurityDefinition("Bearer",
    new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization"
    });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });


});

// Validator
builder.Services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
builder.Services.AddTransient<IValidator<HsinChuAreaParameter>, HsinChuAreaParameterValidator>();

// Options
builder.Services.AddOptions<HsinchuGovOptions>().Bind(
    builder.Configuration.GetSection(HsinchuGovOptions.SectionKey));

// HttpClientFactory
var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(30);
builder.Services.AddHttpClient("HsinchuGovOptions")
    .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] { TimeSpan.FromMilliseconds(500) }))
    .AddPolicyHandler(timeoutPolicy);

// Services
builder.Services.AddScoped<ISampleService, SampleService>();

// Repositories
// Decorate > 未已註冊的服務添加裝飾器
builder.Services.AddScoped<IHsinChuRepository, HsinChuRepository>().Decorate<IHsinChuRepository, CachedHsinChuRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(ControllerMapperProfiler), typeof(ServiceMapperProfile));


// 添加身份驗證服務，並設置 JWT Bearer 認證
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "https://mia1-issuer.com", // 須替換為真實的發行者
        ValidAudience = "miaAudience", // 須替換為真實的受眾
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("vN8g4LjNWyVbmGRRBgIXgvGlV/nRIvYIshZw7H3vqZI=")) // 使用對稱密鑰
    };
});

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.yaml", "Template_NET_Core V1");  });
}

app.UseRouting();
app.UseAuthentication();

//app.UseCoreProfiler(true);
//app.UseHttpMetric();
//app.UseEndpoints(endpoint => { endpoint.MapMetrics()});



app.UseHttpsRedirection();

// 使用身份驗證中介軟體
app.UseAuthentication(); // 在 app.UseAuthorization() 之前調用

app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", async context => { await context.Response.WriteAsync("pong"); });

app.Run();
