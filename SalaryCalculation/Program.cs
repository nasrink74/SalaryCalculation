using Application.Incoms.Command;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common;
using Common.Utilities;
using Data;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Service.Users.Command.Services;
using System.Reflection;
using WebFramework.Configuration;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddMediatR(typeof(AddIncomCommand).GetTypeInfo().Assembly);

builder.Services.AddServiceExtention();

#region Autofac
// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<Autofac.ContainerBuilder>(autofacConfigure =>
{
    var commonAssembly = typeof(CalenderHelper).Assembly;
    var entityAssembly = typeof(BaseEntity).Assembly;
    var dataAssembly = typeof(ApplicationDbContext).Assembly;
    var serviceAssembly = typeof(CommandUsersService).Assembly;
    autofacConfigure.RegisterAssemblyTypes(commonAssembly, entityAssembly, dataAssembly, serviceAssembly)
    .AssignableTo<IScopedDependency>()
    .AsImplementedInterfaces()
    .InstancePerLifetimeScope();

    autofacConfigure.RegisterAssemblyTypes(commonAssembly, entityAssembly, dataAssembly, serviceAssembly)
   .AssignableTo<ITransientDependency>()
   .AsImplementedInterfaces()
   .InstancePerDependency();

    autofacConfigure.RegisterAssemblyTypes(commonAssembly, entityAssembly, dataAssembly, serviceAssembly)
   .AssignableTo<ISingletonDependency>()
   .AsImplementedInterfaces()
   .SingleInstance();
});
#endregion

var app = builder.Build();
#endregion

#region Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion

