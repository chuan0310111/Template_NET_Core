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
using Microsoft.OpenApi.Models;
using Polly;
using System.Reflection;

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
builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(options => {
//    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
//    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
//    options.SwaggerDoc("v1", new OpenApiInfo
//    {
//        Version = "v1",
//        Title = "Template API",
//        Description = ""
//    });

//});

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

app.UseAuthorization();

app.MapControllers();

app.MapGet("/ping", async context => { await context.Response.WriteAsync("pong"); });

app.Run();
