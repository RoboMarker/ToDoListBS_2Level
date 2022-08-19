using Generally;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dapper法  單元測試用
builder.Services.AddInfrastructure();

//Dapper法1  注入 IDbConnection
//Scoped：注入的物件在同一Request中，參考的都是相同物件(你在Controller、View中注入的IDbConnection指向相同參考)
IConfiguration Configuration = builder.Configuration;
builder.Services.AddScoped<IDbConnection, SqlConnection>(serviceProvider => {
    SqlConnection conn = new SqlConnection();
    //指派連線字串
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

//加入api版本
//builder.Services.AddApiVersioning(opt => opt.ReportApiVersions = true);

#region CORS
//CORS 全網站寫法 法1
builder.Services.AddCors(option => {
    option.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5005",
            "http://127.0.0.1:5500", "https://127.0.0.1:5500", "http://127.0.0.1:5501", "https://127.0.0.1:5501")
        .SetIsOriginAllowedToAllowWildcardSubdomains()//為設定子網域成為原始的來源
         .AllowAnyHeader()//為允許所有Request Header
         .AllowAnyMethod();//為允許所有Http Method
    });                                                                                                           
});                                                                                                                

//CORS 個別網站寫法 法2
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "NodjsEx",
//                      builder =>
//                      {
//                          builder.WithOrigins("http://localhost:5005", "http://localhost:5005/")
//                          .SetIsOriginAllowedToAllowWildcardSubdomains()//為設定子網域成為原始的來源
//                            .AllowAnyHeader()//為允許所有Request Header
//                            .AllowAnyMethod();//為允許所有Http Method
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

//註冊Middleware
//app.UseMiddleware<ReqHeaderChecker>();

app.UseHttpsRedirection();
app.UseAuthorization();
#region CORS
app.UseCors();//CORS 全網站寫法  法1
//app.UseCors("NodjsEx"); // CORS 加上後會套用到全域 法2
//曾對特定的路徑 法3
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
