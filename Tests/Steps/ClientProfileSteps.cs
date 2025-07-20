using System;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using AdvancedReqnRollTest.Registry;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class ClientProfileSteps
{
    private readonly IPageObjectManager _pages;
    private readonly ScenarioContext _scenarioContext;

    public ClientProfileSteps(IPageObjectManager pages, ScenarioContext scenarioContext)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
    }

    [Then("the user verifies the newly created client ID is displayed")]
    public void ThenTheUserVerifiesTheNewlyCreatedClientIdIsDisplayed()
    {
        string clientId = _pages.ClientProfilePage.GetClientIdFromFormData();
        _scenarioContext[ScenarioKeys.ClientId] = clientId;
        Console.WriteLine("Saved Client ID: " + clientId);
    }
}