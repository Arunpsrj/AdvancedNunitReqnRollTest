using System;
using RestSharp;

namespace AdvancedReqnRollTest.RestAPI;

public class RestLibrary
{
    public RestClient RestClient { get; }

    public RestLibrary(string baseUrl)
    {
        var options = new RestClientOptions
        {
            BaseUrl = new Uri(baseUrl)
        };

        RestClient = new RestClient(options);
    }
}