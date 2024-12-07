using BuberDinner.Api;
using BuberDinner.Application;
using BuberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()   
        .AddInfrastructure(builder.Configuration);
}


var app = builder.Build();
{
    //app.UseMiddleware<ErrorHandlingMiddleware>();
    //app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();  
    app.UseAuthentication();
    //UseAuthentication se goi hanlder duoc cau hinh trong infa
    //de xac thuc token
    app.UseAuthorization();
    //nhung gia tri da duoc validate se duoc UseAuthentication
    //va quyet dinh no co nen di tiep den nhung ennpoinet hay khong
    app.MapControllers();
    app.Run();
}