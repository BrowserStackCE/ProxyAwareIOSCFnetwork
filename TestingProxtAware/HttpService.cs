using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

public class HttpService
{
    private readonly IProxyInfoProvider _proxyInfoProvider;

    private readonly HttpClient _httpClient;

    public WebProxy Proxy { get; private set; }

  
    public HttpService(IProxyInfoProvider proxyInfoProvider,String URL)
    {
        _proxyInfoProvider = proxyInfoProvider;

        _httpClient = CreateHttpClient(URL);
    }


    public HttpClient CreateHttpClient(String URL)
    {
        HttpClient httpClient;
       
            var handler = new HttpClientHandler
            {
                Proxy = _proxyInfoProvider.GetProxySettings()
            };

             httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(URL)
            };

        return httpClient;
    }

    public async Task<string> GetStringAsync(string url, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync(url, cancellationToken);
        return await response.Content.ReadAsStringAsync();
    }

    public static implicit operator HttpService(string v)
    {
        throw new NotImplementedException();
    }
}