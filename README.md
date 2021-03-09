# ProxyAwareIOSCFnetwork (Work in Progress)

## bs-xamarin-proxyaware-app (iOS)


What is a proxy-aware app?
https://docstore.mik.ua/orelly/networking_2ndEd/fire/ch09_02.htm


## Workflow
Enter the expected URL that needs to be checked in the app. Best example for getting the IP details would be http://ip-api.com/json
Click on  "Make proxy aware" button the app will detect the system proxy and print the response.


## Example 
Clone the repo and build the app on Visual Studio.
Enter http://ip-api.com/json your local browser. Note the IP mentioned in the "query" field.
Start an AppLive session from https://app-live.browserstack.com/ and upload the .ipa
Once the App has started, enter the same URI as above.
To get the same IP on the app as your local machine, you can download and enable the Local testing app on you machine. Along with that enable "Force-Local" option (Ref: https://www.browserstack.com/docs/app-live/local-testing) which will route all the traffic to BrowserStack from your machine.
Once this is done and the "Make proxy aware" checkbox is checked you should the same IP on the app's UI as on your local browser when http://ip-api.com/json was resolved.


## Reference
https://medium.com/@anna.domashych/httpclient-and-proxy-76835c784eab
https://docs.microsoft.com/en-us/dotnet/api/system.net.http.httpclienthandler.proxy?view=net-5.0
