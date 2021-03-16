// iOS implementation
using System.Net;
using CoreFoundation;
using System;
using Foundation;
using System.Net.Http;
using System.Text.RegularExpressions;
public class ProxyInfoProvider : IProxyInfoProvider
{
    public static String set;
    public WebProxy GetProxySettings()
    {
        var settings = CFNetwork.GetSystemProxySettings();
        Console.WriteLine("Setting" + settings.Dictionary.ToString());
        set = settings.Dictionary.ToString();
        var proxyPACURL = settings.ProxyAutoConfigURLString;
        String fetchedResponse = GetResponseData(proxyPACURL);
        String proxyHostPortMatch = Regex.Match(fetchedResponse, "(\\d+.\\d+.\\d+.\\d+:\\d+)", RegexOptions.IgnoreCase).ToString();
        String[] proxyHostPortArray = proxyHostPortMatch.Split(':');
        var proxyHost = proxyHostPortArray[0];
        var proxyPort = int.Parse(proxyHostPortArray[1]);
        Console.WriteLine("proxyPort  " + proxyPort);
        Console.WriteLine("proxyHost  " + proxyHost);
        return !string.IsNullOrEmpty(proxyHost) && proxyPort != 0
            ? new WebProxy($"{proxyHost}:{proxyPort}")
            : null;
    }
    public String GetResponseData(string apiUri)
    {
        using (var httpClient = new HttpClient())
        {
            var httpResponseMessage = httpClient.GetAsync(apiUri).Result;
            var responseContent = httpResponseMessage.Content.ReadAsStringAsync().Result;
            Console.WriteLine("HTTP Response message: " + responseContent);
            return responseContent;
        }
    }
}