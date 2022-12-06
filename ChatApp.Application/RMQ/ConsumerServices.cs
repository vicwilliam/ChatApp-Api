using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatApp.Application.RMQ;

public class ConsumerService : IConsumerService, IDisposable
{
    private readonly IModel _model;
    private readonly IConnection _connection;
    const string _queueName = "Stocks";

    public ConsumerService(IRabbitMqService rabbitMqService)
    {
        _connection = rabbitMqService.CreateChannel();
        _model = _connection.CreateModel();
        _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare("StocksExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(_queueName, "StocksExchange", string.Empty);
    }


    public async Task ReadMessgaes()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);
        consumer.Received += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            var text = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
            await Task.CompletedTask;
            _model.BasicAck(ea.DeliveryTag, false);
        };
        _model.BasicConsume(_queueName, false, consumer);
        await Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}