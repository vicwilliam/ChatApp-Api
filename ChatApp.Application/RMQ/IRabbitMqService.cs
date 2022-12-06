using RabbitMQ.Client;

namespace ChatApp.Application.RMQ;

public interface IRabbitMqService
{
    IConnection CreateChannel();
}