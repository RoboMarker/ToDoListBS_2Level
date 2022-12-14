using Generally;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dapper猭  虫じ代刚ノ
builder.Services.AddInfrastructure();

//Dapper猭1  猔 IDbConnection
//Scoped猔ンRequestい把σ常琌ン(ControllerViewい猔IDbConnection把σ)
IConfiguration Configuration = builder.Configuration;
builder.Services.AddScoped<IDbConnection, SqlConnection>(serviceProvider => {
    SqlConnection conn = new SqlConnection();
    //硈絬﹃
    conn.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
    return conn;
});


//builder.Services.AddMvc(config => {
//    config.Filters.Add(new AuthorFilter());
//    config.Filters.Add(new ResourceFilter());
//    config.Filters.Add(new ActionFilter());
//    config.Filters.Add(new ExceptionFilter());
//    config.Filters.Add<ResultFilter>();
//});

//apiセ
//builder.Services.AddApiVersioning(opt => opt.ReportApiVersions = true);

#region CORS
//CORS 呼糶猭 猭1
builder.Services.AddCors(option => {
    option.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5005",
            "http://127.0.0.1:5500", "https://127.0.0.1:5500", "http://127.0.0.1:5501", "https://127.0.0.1:5501")
        .SetIsOriginAllowedToAllowWildcardSubdomains()//砞﹚呼办Θ﹍ㄓ方
         .AllowAnyHeader()//す砛┮ΤRequest Header
         .AllowAnyMethod();//す砛┮ΤHttp Method
    });                                                                                                           
});                                                                                                                

//CORS 呼糶猭 猭2
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "NodjsEx",
//                      builder =>
//                      {
//                          builder.WithOrigins("http://localhost:5005", "http://localhost:5005/")
//                          .SetIsOriginAllowedToAllowWildcardSubdomains()//砞﹚呼办Θ﹍ㄓ方
//                            .AllowAnyHeader()//す砛┮ΤRequest Header
//                            .AllowAnyMethod();//す砛┮ΤHttp Method
//                      });
//
//    options.AddPolicy(name: "live",
//                      builder =>
//                      {
//                          builder.WithOrigins("http://127.0.0.1:5501");
//                      });
//});
#endregion

var app = builder.Build();

//app.ConfigureApi(); //All OF my API endpoint mapping

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//爹Middleware
//app.UseMiddleware<ReqHeaderChecker>();

app.UseHttpsRedirection();
app.UseAuthorization();
#region CORS
app.UseCors();//CORS 呼糶猭  猭1
//app.UseCors("NodjsEx"); // CORS 穦甅ノ办 猭2
//纯癸疭﹚隔畖 猭3
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapGet("/map",
//        context => context.Response.WriteAsync("map"))
//        .RequireCors("NodjsEx");
//
//    endpoints.MapControllers()
//             .RequireCors("live");
//});
#endregion
app.MapControllers();

app.Run();
