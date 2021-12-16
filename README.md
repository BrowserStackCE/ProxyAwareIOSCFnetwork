![BrowserStack Logo](https://camo.githubusercontent.com/09765325129b9ca76d770b128dbe30665379b7f2915d9b60bf57fc44d9920305/68747470733a2f2f7777772e62726f77736572737461636b2e636f6d2f696d616765732f7374617469632f6865616465722d6c6f676f2e6a7067)

# BrowserStack Example - Proxy Aware IOS app (CFnetwork) 

* This app demonstrates how proxy-aware vs non-proxy aware applications behave on BrowserStack.


What is a proxy-aware app?
https://docstore.mik.ua/orelly/networking_2ndEd/fire/ch09_02.htm

## Modules 

ViewController : https://github.com/sanketsmali/ProxyAwareIOSCFnetwork/blob/master/TestingProxtAware/ViewController.cs
HTTPRequest Service   : https://github.com/sanketsmali/ProxyAwareIOSCFnetwork/blob/master/TestingProxtAware/HttpService.cs
Proxy Info Povider : https://github.com/sanketsmali/ProxyAwareIOSCFnetwork/blob/master/TestingProxtAware/ProxyInfoProvider.cs


## Workflow
Enter the expected URL that needs to be checked in the app. Best example for getting the IP details would be http://ip-api.com/json
Click on  "Make proxy aware" button the app will detect the system proxy and print the response.
The toggle button will change system proxy using the following code:

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
The proxy is being read from a PAC file and then formatted via regular expression 

## Example 
1. Clone the repo and build the app on Visual Studio.
2. Enter http://ip-api.com/json your local browser. Note the IP mentioned in the "query" field.
3. Start an AppLive session from https://app-live.browserstack.com/ and upload the .ipa
4. Once the App has started, enter the same URI as above.
5. To get the same IP on the app as your local machine, you can download and enable the Local testing app on you machine. Along with that enable "Force-Local" option (Ref: https://www.browserstack.com/docs/app-live/local-testing) which will route all the traffic to BrowserStack from your machine.
6. Once this is done and the "Make proxy aware" checkbox is checked you should the same IP on the app's UI as on your local browser when http://ip-api.com/json was resolved.


## Reference
https://medium.com/@anna.domashych/httpclient-and-proxy-76835c784eab
https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclienthandler.proxy?view=net-5.0
