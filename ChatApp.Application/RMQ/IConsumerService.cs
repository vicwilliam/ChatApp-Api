namespace ChatApp.Application.RMQ;

public interface IConsumerService
{
    Task ReadMessgaes();
}