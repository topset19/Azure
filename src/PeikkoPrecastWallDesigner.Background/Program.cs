using PeikkoPrecastWallDesigner.Application.Computations.DTOs;
using PeikkoPrecastWallDesigner.Background.Common.BackgroundTaskQueue;

var builder = Host.CreateApplicationBuilder(args);


var host = builder.Build();
host.Run();
