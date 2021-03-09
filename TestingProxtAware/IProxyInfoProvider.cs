using System.Net;

public interface IProxyInfoProvider
{
    WebProxy GetProxySettings();
}