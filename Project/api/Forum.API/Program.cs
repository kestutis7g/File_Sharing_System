using Autofac;
using Autofac.Extensions.DependencyInjection;
using Forum.Core;
using Forum.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog.Web;
using Forum.Infrastructure.Data;
using Forum.Shared.Models;
using Forum.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseNLog(new NLogAspNetCoreOptions
{
    RemoveLoggerFactoryFilter = false,
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("CONNECTIONSTRING");

// builder.Services.AddIdentity<RestUser, IdentityRole>()
//     .AddEntityFrameworkStores<DatabaseContext>()
//     .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.JWT_SECURITY_KEY)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
// builder.Services.AddAuthentication(options =>
// {
//     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
// })
//     .AddJwtBearer(options =>
//     {
//         options.TokenValidationParameters.ValidAudience = builder.Configuration["JWT:ValidAudience"];
//         options.TokenValidationParameters.ValidIssuer = builder.Configuration["JWT:ValidIssuer"];
//         options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]));
//     });

builder.Services.AddAppDbContext(connectionString);


builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScopedQuartz();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddHttpClient();

builder.Services.AddCors();

builder.Services
    .AddControllers()
    .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Forum.API", Version = "v1" });

    //var jwtSecurityScheme = new OpenApiSecurityScheme
    //{
    //    BearerFormat = "JWT",
    //    Name = "JWT Authorization",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = JwtBearerDefaults.AuthenticationScheme,
    //    Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

    //    Reference = new OpenApiReference
    //    {
    //        Id = JwtBearerDefaults.AuthenticationScheme,
    //        Type = ReferenceType.SecurityScheme
    //    }
    //};

    //c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    //c.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    { jwtSecurityScheme, Array.Empty<string>() }
    //});

    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        Name = "JWT Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[] {}
        }
    });
    c.EnableAnnotations();
});



builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new CoreDIModule());
    containerBuilder.RegisterModule(new InfrastructureDIModule());
});

var app = builder.Build();

//app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
    
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseMiddleware<JwtMiddleware>();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();


app.MapControllers();

MigrateDb(app);

app.Run();

static void MigrateDb(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;

        try
        {
            var context = services.GetRequiredService<DatabaseContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred migrating the database.");
        }
    }
}
