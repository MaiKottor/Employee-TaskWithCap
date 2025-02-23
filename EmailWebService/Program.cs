using EmailWindowsService;
using EmailWindowsService.EventHandlers;
using EmailWindowsService.Services.Interfaces;
using EmailWindowsService.Services;


//var host = builder.Build();
var host = Host.CreateDefaultBuilder(args)
                .UseWindowsService() // Enable Windows Service support
                .ConfigureServices((hostContext, services) =>
                {
                    // Register CAP with RabbitMQ and SQL Server for persistence
                    services.AddCap(options =>
                    {
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
                        options.UseRabbitMQ(cfg =>
                        {
                            cfg.HostName = hostContext.Configuration["CAP:RabbitMQ:HostName"];
                            cfg.UserName = hostContext.Configuration["CAP:RabbitMQ:UserName"];
                            cfg.Password = hostContext.Configuration["CAP:RabbitMQ:Password"];
                        });
                        options.DefaultGroupName = "email-service-group";
                        
                    });

                    // Register Email Sender Service
                    services.AddSingleton<IEmailSender, EmailSender>();

                    // Register CAP Subscriber for email events
                    services.AddScoped<EmailEventHandler>(); 

                })
                .Build();

await host.RunAsync();
        
    
//host.Run();
