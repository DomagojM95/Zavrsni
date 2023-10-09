using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PlaninarskiDnevnik.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sgo => { // sgo je instanca klase SwaggerGenOptions
    // èitati https://devintxcontent.blob.core.windows.net/showcontent/Speaker%20Presentations%20Fall%202017/Web%20API%20Best%20Practices.pdf
    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Planinarski Dnevnik API",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "domagojmarkovic1995@gmail.com",
            Name = "Domagoj Marković"
        },
        Description = "Ovo je dokumentacija za Planinarski Dnevnik  API",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Edukacijska licenca"
        }
    };
    sgo.SwaggerDoc("v1", o);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

});


builder.Services.AddDbContext<PlaninarskiDnevnikContext>(o =>
o.UseSqlServer(builder.Configuration.GetConnectionString(name: "PLDNEVNIK")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(opcije =>
    {
        opcije.SerializeAsV2 = true;
    });
    app.UseSwaggerUI(opcije =>
    {
      opcije.ConfigObject.AdditionalItems.Add("reguestSnippersEnabled", true);
    });
//}

app.UseHttpsRedirection();


app.MapControllers();
app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();
app.UseDeveloperExceptionPage();
app.MapFallbackToFile("index.html");

app.Run();