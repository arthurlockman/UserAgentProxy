using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/{urlBase64}/{headersBase64}", async (string urlBase64, string headersBase64, HttpClient httpClient) =>
{
    var url = Encoding.UTF8.GetString(Convert.FromBase64String(urlBase64));
    var headersJson = Encoding.UTF8.GetString(Convert.FromBase64String(headersBase64));
    var headers = JsonSerializer.Deserialize<Dictionary<string, string>>(headersJson);

    httpClient.DefaultRequestHeaders.Clear();
    foreach (var header in headers ?? new Dictionary<string, string>())
    {
        httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
    }

    return await httpClient.GetStringAsync(url);
});

app.Run();
