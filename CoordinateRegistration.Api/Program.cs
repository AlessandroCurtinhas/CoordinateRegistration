using CoordinateRegistration.Application.JwtAuthentication;
using CoordinateRegistration.Application.Dto.Authentication;
using CoordinateRegistration.Application.Dto.Comment;
using CoordinateRegistration.Application.Dto.Marker;
using CoordinateRegistration.Application.Dto.Review;
using CoordinateRegistration.Application.Dto.TypeOccurrence;
using CoordinateRegistration.Application.Interface;
using CoordinateRegistration.Application.Interface.Authenticate;
using CoordinateRegistration.Application.Services;
using CoordinateRegistration.Application.Services.Authenticate;
using CoordinateRegistration.Application.Validators.Authentication;
using CoordinateRegistration.Application.Validators.Comment;
using CoordinateRegistration.Application.Validators.Marker;
using CoordinateRegistration.Application.Validators.Review;
using CoordinateRegistration.Application.Validators.TypeOccurrence;
using CoordinateRegistration.Persistence.Context;
using CoordinateRegistration.Persistence.Interface;
using CoordinateRegistration.Persistence.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using CoordinateRegistration.Application.Dto.Person;
using CoordinateRegistration.Application.Validators.Person;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200")
                          .AllowAnyHeader()
                          .AllowAnyMethod();

                      });

});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<CoordinateRegistrationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BikeRoute")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAllRespository, AllRespository>();
builder.Services.AddScoped<IMarkerService, MarkerService>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IPersonAuthenticationService, PersonAuthenticationService>();

builder.Services.AddScoped<ITypeOccurrenceService, TypeOccurrenceService>();
builder.Services.AddScoped<IMarkerRepository, MarkerRepository>();
builder.Services.AddScoped<ITypeOccurrenceRespository, TypeOccurrenceRepository>();
builder.Services.AddScoped<IMarkerTypeOccurrenceRepository, MarkerTypeOccurrenceRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddTransient<IValidator<MarkerAddDto>, MarkerAddValidator>();
builder.Services.AddTransient<IValidator<MarkerPutDto>, MarkerPutValidator>();
builder.Services.AddTransient<IValidator<TypeOccurrenceAddDto>, TypeOccurrenceAddValidator>();
builder.Services.AddTransient<IValidator<TypeOccurrencePutDto>, TypeOccurrencePutValidator>();
builder.Services.AddTransient<IValidator<PersonAddDto>, PersonAddValidator>();
builder.Services.AddTransient<IValidator<PersonPutDto>, PersonPutValidator>();
builder.Services.AddTransient<IValidator<PersonLoginDto>, PersonLoginValidator>();
builder.Services.AddTransient<IValidator<PersonDeleteDto>, PersonDeleteValidator>();
builder.Services.AddTransient<IValidator<PersonRecoveryPasswordDto>, PersonRecoveryPasswordValidator>();
builder.Services.AddTransient<IValidator<PersonRecoveryRequestDto>, PersonRecoveryRequestValidator>();
builder.Services.AddTransient<IValidator<ReviewAddDto>, ReviewAddValidator>();
builder.Services.AddTransient<IValidator<ReviewPutDto>, ReviewPutValidator>();
builder.Services.AddTransient<IValidator<CommentAddDto>, CommentAddValidator>();
builder.Services.AddTransient<IValidator<CommentPutDto>, CommentPutValidator>();

builder.Services.AddScoped<IPersonAuthenticationService, PersonAuthenticationService>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Coordinate Registration", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }
        });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});


var settings = builder.Configuration.GetSection("JwtSettings");
builder.Services.Configure<JwtSettings>(settings);

var appSettings = settings.Get<JwtSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

builder.Services.AddAuthentication(
    auth => {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(auth => {
        auth.RequireHttpsMetadata = false;
        auth.SaveToken = true;
        auth.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddTransient(map => new JwtService(appSettings));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider; 
    try 
    { var context = services.GetRequiredService<CoordinateRegistrationDbContext>(); context.Database.Migrate(); 
    }
    catch (Exception ex)
    { // Log qualquer erro ocorrido durante a migração
      var logger = services.GetRequiredService<ILogger<Program>>(); logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações."); 
    } 
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();


app.Run();
