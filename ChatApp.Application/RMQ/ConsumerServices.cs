using ChatApp.Application.Dtos;
using ChatApp.Application.Integration.Stooq;
using ChatApp.Application.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ChatApp.Application.RMQ;

public class ConsumerService : IConsumerService, IDisposable
{
    private readonly IMessageService _messageService;
    private readonly IUserService _userService;
    private readonly IModel _model;
    private readonly IConnection _connection;
    private Guid? botGuid = null;
    const string _queueName = "Stocks";

    public ConsumerService(IRabbitMqService rabbitMqService, IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        this._messageService = scope.ServiceProvider.GetRequiredService<IMessageService>();
        this._userService = scope.ServiceProvider.GetRequiredService<IUserService>();
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
            var requestData = JsonConvert.DeserializeObject<SendCommandDto>(text);
            botGuid = botGuid ?? await _userService.GetUserIdFromUsername("stocksbot");
            try
            {
                var stockList = await StooqApi.GetQuote(requestData.Parameter);
                if (stockList.First().High != "N/D")
                {
                    var stock = stockList.FirstOrDefault();
                    var messageDto = new SendMessageDto()
                    {
                        Content = $"Quotes for {stock.Symbol}\n " +
                                  $"Open:{stock.Open} \n" +
                                  $"Close: {stock.Close}\n" +
                                  $"High: {stock.High}\n" +
                                  $"Low: {stock.Low}",
                        AuthorId = (Guid)botGuid,
                        RoomId = requestData.RoomId
                    };
                    await this._messageService.SendMessage(messageDto);
                    return;
                }

                throw new Exception();
            }
            catch (Exception e)
            {
                await this._messageService.SendMessage(new SendMessageDto()
                {
                    Content = "Failed to get stock price",
                    AuthorId = (Guid)botGuid,
                    RoomId = requestData.RoomId
                });
                throw;
            }

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