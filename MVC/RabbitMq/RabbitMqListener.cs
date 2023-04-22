using OnlineAuction.BLL.Interface;
using OnlineAuction.Entities;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Diagnostics;
using System.Text;

public class RabbitMqListener : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private IUserLogic _userLogic;

    public RabbitMqListener(IUserLogic userLogic)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "MyQueue", durable: false, exclusive: false, autoDelete: false, arguments: null);

        _userLogic = userLogic;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            Debug.WriteLine($"Получено сообщение: {content}");

            try
            {
                var userData = content.Split(' ');

                if (userData.Length == 6)
                {
                    _userLogic.Add(new User()
                    {
                        Name = userData[0],
                        SurName = userData[1],
                        Email = userData[2],
                        Password = userData[3],
                        PhoneNumber = userData[4],
                        DateOfBirth = DateTime.Parse(userData[5])
                    });
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Получена ошибка: {ex.Message}");
            }

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("MyQueue", false, consumer);

        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}