using System.Globalization;
using CsvHelper;

namespace ChatApp.Application.Integration.Stooq;

public static class StooqApi
{
    public static async Task GetQuote(string symbol)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"https://stooq.com/q/l/?s={symbol}&f=sd2t2ohlcv&h&e=csv");
        response.EnsureSuccessStatusCode();
        var resposeBody = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(resposeBody);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        {
            var records = csvReader.GetRecords<QuoteApiSchema>();
        }
    }
}