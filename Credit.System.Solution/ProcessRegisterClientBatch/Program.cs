using Microsoft.EntityFrameworkCore;
using Platform.Entity.Interfaces;
using Platform.Repository;
using Platform.Transactional.Operations;
using ProcessRegisterClientBatch;

var builder = Host.CreateApplicationBuilder(args);

// Register hosted worker (singleton)
builder.Services.AddHostedService<Worker>();

// ✅ Scoped - required because it depends on EF Core DbContext
builder.Services.AddScoped<IBatchClientRegisterOperations, BatchClientRegisterOperations>();
builder.Services.AddScoped<IClientOperations, ClientOperations>();

// DbContext (scoped by default) - always correct for EF
builder.Services.AddDbContext<ServiceSystemConnection>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceSystemDB")));

var host = builder.Build();
host.Run();
