using System;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class TempProfileSteps
{
    private readonly IPageObjectManager _pages;
    private readonly ScenarioContext _scenarioContext;

    public TempProfileSteps(IPageObjectManager pages, ScenarioContext scenarioContext)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
    }

    [Given("the user navigates to {string} tab")]
    public void GivenTheUserNavigatesToTab(string tabName)
    {
        _pages.CommonPage.NavigateToTab(tabName);
    }

    [Then("the user verifies the newly created temp ID is displayed")]
    public void ThenTheUserVerifiesTheNewlyCreatedTempIdIsDisplayed()
    {
        string tempId = _pages.TempProfilePage.GetTempIdFromFormData();
        _scenarioContext[ScenarioKeys.TempUserId] = tempId;
        Console.WriteLine("Saved Temp ID: " + tempId);
    }
}