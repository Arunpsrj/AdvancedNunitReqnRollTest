using System;
using System.Collections.Generic;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using AdvancedReqnRollTest.RestAPI;
using Reqnroll;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdvancedReqnRollTest.Registry;
using FluentAssertions;
using NUnit.Framework;
using RestSharp;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class TempProfileSteps
{
    private readonly IPageObjectManager _pages;
    private readonly ScenarioContext _scenarioContext;
    private readonly RestFactory _restFactory;


    public TempProfileSteps(IPageObjectManager pages, ScenarioContext scenarioContext,  RestFactory restFactory)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
        _restFactory = restFactory;
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
        _scenarioContext[ScenarioKeys.TempId] = tempId;
        bool isInserted = _pages.DbHelperPage.InsertTempRecords(_scenarioContext.Get<string>(ScenarioKeys.TempId),_scenarioContext.Get<string>(ScenarioKeys.TempFirstName),_scenarioContext.Get<string>(ScenarioKeys.TempLastName));
        Assert.IsTrue(isInserted, "Temp record was not inserted into the database.");
        Console.WriteLine("Saved Temp ID: " + tempId);
    }
}