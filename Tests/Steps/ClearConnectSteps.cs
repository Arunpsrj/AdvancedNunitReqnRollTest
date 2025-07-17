using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Registry;
using AdvancedReqnRollTest.RestAPI;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Reqnroll;
using RestSharp;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class ClearConnectSteps
{
    private readonly IPageObjectManager _pages;
    private readonly ScenarioContext _scenarioContext;
    private readonly RestFactory _restFactory;


    public ClearConnectSteps(IPageObjectManager pages, ScenarioContext scenarioContext,  RestFactory restFactory)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
        _restFactory = restFactory;
    }

    [Given(@"a ""(.*)"" request sent for ""(.*)"" to the ""(.*)"" site with the following arguments")]
    public async Task GivenARequestSentForToTheSiteWithTheFollowingArguments(string requestType, string action, string site, Reqnroll.Table table)
    {
        var baseUrl = _pages.LoginPage.GetAPIUrl(site);
        var parameters = table.Rows.ToDictionary(r => r["Field"].ToLower(), r => r["Value"]);
        
        foreach (var key in parameters.Keys.ToList()) // Use ToList() to safely modify dictionary
        {
            if (!parameters.TryGetValue(key, out var value)) continue;

            switch (key)
            {
                case "orderid":
                    if (value.Contains("scenario", StringComparison.OrdinalIgnoreCase))
                    {
                        parameters[key] = _scenarioContext.Get<string>(ScenarioKeys.OrderId);
                    }
                    break;
                case "startdate":
                    if (value.Contains("getDate", StringComparison.OrdinalIgnoreCase))
                    {
                        parameters[key] = _pages.CommonPage.ResolveDatePattern(parameters[key]);
                    }
                    break;
            }
        }
        
        var builder = _restFactory
            .Create(baseUrl)
            .WithRequest("")
            .WithQueryParameter("resultType", "json")
            .WithHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("Arunsrj:Arunsrj@24")));

        var method = RequestMethodRegistry.GetHttpMethodFor(action);
        RestResponse response;

        switch (method)
        {
            case "GET":
                builder = builder.WithQueryParameter("action", action); // ✅ action in query for GET

                foreach (var param in parameters) 
                    builder = builder.WithQueryParameter(param.Key, param.Value);
                response = await builder.WithGet();
                break;

            case "POST":
                parameters["action"] = action; // ✅ action in form-data for POST
                builder = builder.WithFormDataBody(parameters);
                response = await builder.WithPost();
                break;

            default:
                throw new ArgumentException($"Unsupported HTTP method for action '{action}'");
        }

        _scenarioContext["LastResponse"] = response;

        // ✅ Assert response success
        if (!response.IsSuccessful)
        {
            throw new InvalidOperationException(
                $"API request failed!\n" +
                $"Status Code: {response.StatusCode}\n" +
                $"Error Message: {response.ErrorMessage}\n" +
                $"Response Body: {response.Content}"
            );
        }
    }
    
    [Then("the web response should contain {string} with value {string}")]
    public void ThenTheWebResponseShouldContainWithValue(string key, string expectedValue)
    {
        var response = (RestResponse)_scenarioContext[ScenarioKeys.LastResponse];

        JToken parsed = JToken.Parse(response.Content);

        if (parsed is JArray jsonArray)
        {
            Assert.IsTrue(jsonArray.Count > 0, "Expected a non-empty array in the response.");
            var actualValue = jsonArray[0]?[key]?.ToString();

            Assert.IsNotNull(actualValue, $"Expected field '{key}' was not found in the first array item.");
            Assert.AreEqual(expectedValue, actualValue, $"Expected '{key}' to be '{expectedValue}', but got '{actualValue}'.");
        }
        else if (parsed is JObject jsonObject)
        {
            var actualValue = jsonObject[key]?.ToString();

            Assert.IsNotNull(actualValue, $"Expected field '{key}' was not found in the response.");
            Assert.AreEqual(expectedValue, actualValue, $"Expected '{key}' to be '{expectedValue}', but got '{actualValue}'.");
        }
        else
        {
            Assert.Fail("Unsupported JSON format: expected object or array.");
        }
    }
}