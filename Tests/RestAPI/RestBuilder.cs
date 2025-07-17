using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace AdvancedReqnRollTest.RestAPI;

public class RestBuilder
{
    private readonly RestLibrary _restLibrary;

    public RestBuilder(RestLibrary restLibrary)
    {
        _restLibrary = restLibrary;
    }

    private RestRequest RestRequest { get; set; } = null!;

    public RestBuilder WithRequest(string request)
    {
        RestRequest = new RestRequest(request);
        return this;
    }
    
    public RestBuilder WithHeader(string name, string value)
    {
        RestRequest.AddHeader(name, value);
        return this;
    }
    
    public RestBuilder WithQueryParameter(string name, string value)
    {
        RestRequest.AddQueryParameter(name, value);
        return this;
    }
    
    public RestBuilder WithUrlSegment(string name, string value)
    {
        RestRequest.AddUrlSegment(name, value);
        return this;
    }
    
    public RestBuilder WithFormDataBody(Dictionary<string, string> body)
    {
        RestRequest.AlwaysMultipartFormData = true;

        foreach (var kvp in body)
        {
            RestRequest.AddParameter(kvp.Key, kvp.Value);
        }

        return this;
    }
    
    public async Task<RestResponse> WithGet()
    {
        return await _restLibrary.RestClient.ExecuteAsync(RestRequest);
    }
    
    public async Task<RestResponse> WithPost()
    {
        return await _restLibrary.RestClient.PostAsync(RestRequest);
    }
    
    public async Task<T?> WithPut<T>()
    {
        return await _restLibrary.RestClient.PutAsync<T>(RestRequest);
    }
    
    public async Task<T?> WithDelete<T>()
    {
        return await _restLibrary.RestClient.DeleteAsync<T>(RestRequest);
    }
    
    public async Task<T?> WithPatch<T>()
    {
        return await _restLibrary.RestClient.PatchAsync<T>(RestRequest);
    }
}