using System.Globalization;
using CsvHelper;

namespace ChatApp.Application.Integration.Stooq;

public static class StooqApi
{
    public static async Task<List<QuoteApiSchema>> GetQuote(string symbol)
    {
        using var client = new HttpClient();
        var url = $"https://stooq.com/q/l/?s={symbol}&f=sd2t2ohlcv&h&e=csv";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var response = await client.SendAsync(request);
        var resposeBody = await response.Content.ReadAsStreamAsync();
        using var streamReader = new StreamReader(resposeBody);
        using var csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture);
        {
            var records = csvReader.GetRecords<QuoteApiSchema>().ToList();
            return records;
        }
    }
}