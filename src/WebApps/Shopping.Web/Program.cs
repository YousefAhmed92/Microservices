
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<TransientGatewayRetryHandler>();

builder.Services.AddRefitClient<ICatalogService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSetting:GatewayAddress"]!);
        c.DefaultRequestVersion = System.Net.HttpVersion.Version11;
        c.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();

        if (builder.Environment.IsDevelopment())
        {
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        }

        return handler;
    })
    .AddHttpMessageHandler<TransientGatewayRetryHandler>();

builder.Services.AddRefitClient<IBasketService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["ApiSetting:GatewayAddress"]!);
        c.DefaultRequestVersion = System.Net.HttpVersion.Version11;
        c.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
    })
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler();

        if (builder.Environment.IsDevelopment())
        {
            handler.ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
        }

        return handler;
    })
    .AddHttpMessageHandler<TransientGatewayRetryHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();

public sealed class TransientGatewayRetryHandler : DelegatingHandler
{
    private static readonly TimeSpan[] RetryDelays =
    [
        TimeSpan.FromMilliseconds(300),
        TimeSpan.FromSeconds(1),
        TimeSpan.FromSeconds(2),
        TimeSpan.FromSeconds(4),
        TimeSpan.FromSeconds(8)
    ];

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        for (var attempt = 0; attempt <= RetryDelays.Length; attempt++)
        {
            var retryRequest = await CloneRequest(request, cancellationToken);

            try
            {
                var response = await base.SendAsync(retryRequest, cancellationToken);

                if (!IsTransientGatewayFailure(response) || attempt == RetryDelays.Length)
                {
                    return response;
                }

                response.Dispose();
            }
            catch (HttpRequestException) when (attempt < RetryDelays.Length)
            {
            }

            await Task.Delay(RetryDelays[attempt], cancellationToken);
        }

        throw new InvalidOperationException("The transient gateway retry loop exited unexpectedly.");
    }

    private static bool IsTransientGatewayFailure(HttpResponseMessage response)
    {
        return response.StatusCode is
            System.Net.HttpStatusCode.BadGateway or
            System.Net.HttpStatusCode.ServiceUnavailable or
            System.Net.HttpStatusCode.GatewayTimeout;
    }

    private static async Task<HttpRequestMessage> CloneRequest(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri)
        {
            Version = request.Version,
            VersionPolicy = request.VersionPolicy
        };

        foreach (var header in request.Headers)
        {
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        if (request.Content is null)
        {
            return clone;
        }

        var content = await request.Content.ReadAsByteArrayAsync(cancellationToken);
        clone.Content = new ByteArrayContent(content);

        foreach (var header in request.Content.Headers)
        {
            clone.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        return clone;
    }
}
