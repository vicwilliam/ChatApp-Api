namespace ChatApp.Application.Integration.Stooq;

public class QuoteApiSchema
{
    public string Symbol { get; set; }
    public double Opening { get; set; }
    public DateOnly Date { get; set; }
    public TimeOnly Time { get; set; }
    public double Close { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Volume { get; set; }
}