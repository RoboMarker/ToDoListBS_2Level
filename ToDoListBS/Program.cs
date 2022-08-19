using Generally;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Dapper�k  �椸���ե�
builder.Services.AddInfrastructure();

//Dapper�k1  �`�J IDbConnection
//Scoped�G�`�J������b�P�@Request���A�ѦҪ����O�ۦP����(�A�bController�BView���`�J��IDbConnection���V�ۦP�Ѧ�)
IConfiguration Configuration = builder.Configuration;
builder.Services.AddScoped<IDbConnection, SqlConnection>(serviceProvider => {
    SqlConnection conn = new SqlConnection();
    //�����s�u�r��
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

//�[�Japi����
//builder.Services.AddApiVersioning(opt => opt.ReportApiVersions = true);

#region CORS
//CORS �������g�k �k1
builder.Services.AddCors(option => {
    option.AddDefaultPolicy(builder => {
        builder.WithOrigins("http://localhost:5005",
            "http://127.0.0.1:5500", "https://127.0.0.1:5500", "http://127.0.0.1:5501", "https://127.0.0.1:5501")
        .SetIsOriginAllowedToAllowWildcardSubdomains()//���]�w�l���즨����l���ӷ�
         .AllowAnyHeader()//�����\�Ҧ�Request Header
         .AllowAnyMethod();//�����\�Ҧ�Http Method
    });                                                                                                           
});                                                                                                                

//CORS �ӧO�����g�k �k2
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: "NodjsEx",
//                      builder =>
//                      {
//                          builder.WithOrigins("http://localhost:5005", "http://localhost:5005/")
//                          .SetIsOriginAllowedToAllowWildcardSubdomains()//���]�w�l���즨����l���ӷ�
//                            .AllowAnyHeader()//�����\�Ҧ�Request Header
//                            .AllowAnyMethod();//�����\�Ҧ�Http Method
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

//���UMiddleware
//app.UseMiddleware<ReqHeaderChecker>();

app.UseHttpsRedirection();
app.UseAuthorization();
#region CORS
app.UseCors();//CORS �������g�k  �k1
//app.UseCors("NodjsEx"); // CORS �[�W��|�M�Ψ���� �k2
//����S�w�����| �k3
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
