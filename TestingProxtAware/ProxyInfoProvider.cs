// iOS implementation
using System.Net;
using CoreFoundation;
using System;
using Foundation;

public class ProxyInfoProvider : IProxyInfoProvider
{
    public WebProxy GetProxySettings()
    {
        var systemProxySettings = CFNetwork.GetSystemProxySettings();

        var configuration = NSUrlSessionConfiguration.DefaultSessionConfiguration;
        var settings = CFNetwork.GetSystemProxySettings();
        var proxyHost = settings.HTTPProxy;
        var proxyPort = settings.HTTPPort;

        // var proxyPort = 8888;
        // var proxyHost = "localhost";

        Console.WriteLine("proxyPort  " + proxyPort);
        Console.WriteLine("proxyHost  " + proxyHost);

        return !string.IsNullOrEmpty(proxyHost) && proxyPort != 0
            ? new WebProxy($"{proxyHost}:{proxyPort}")
            : null;
    }
}