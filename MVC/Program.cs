using OnlineAuction.BLL;
using OnlineAuction.BLL.Interface;
using OnlineAuction.DAL;
using OnlineAuction.DAL.Interface;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare(queue: "MyQueue",
                         durable: false,
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine(" [x] Received {0}", message);
    };
    channel.BasicConsume(queue: "MyQueue",
                         autoAck: true,
                         consumer: consumer);
}

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<ILotLogic, LotLogic>();
builder.Services.AddSingleton<IUserLogic, UserLogic>();
builder.Services.AddSingleton<ILotDao, LotDao>(); 
builder.Services.AddSingleton<IUserDao, UserDao>();

builder.Services.AddHostedService<RabbitMqListener>();
var app = builder.Build();


    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
