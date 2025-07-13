using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class CommonSteps
{
    private readonly IPageObjectManager _pages;
    private readonly ScenarioContext _scenarioContext;

    public CommonSteps(IPageObjectManager pages, ScenarioContext scenarioContext)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
    }
    
    [StepDefinition("the user clicks {string}")]
    public void GivenTheUserClicks(string button)
    {
        _pages.CommonPage.ClickButtonXpath(button);
    }

    [Given("the user selects {string} from the {string} dropdown")]
    public void GivenTheUserSelectsFromTheDropdown(string active, string status)
    {
        _pages.CommonPage.SelectFromDropDown(active, status);
    }
    
    [Given("the user enters following address for {string}")]
    public void GivenTheUserEntersFollowingAddressFor(string type, Reqnroll.Table table)
    {
        var dict = table.Rows.ToDictionary(row => row["AddressDetails"].ToLower(), row => row["AddressValues"]);

        var address = new AddressModel()
        {
            Address = dict.GetValueOrDefault("address"),
            City = dict.GetValueOrDefault("city"),
            State = dict.GetValueOrDefault("state"),
            Zip = dict.GetValueOrDefault("zip")
        };
        
        _pages.CommonPage.FillAddressFor(type, address);
    }
    
    [Given("the user enters {string} for {string}")]
    public void GivenTheUserEntersFor(string value, string field)
    {
        var resolvedValue = _pages.CommonPage.ResolveDynamicValue(value);
        _pages.CommonPage.EnterTextForField(field, resolvedValue);
        if (field == "temp first name")
        {
            _scenarioContext[ScenarioKeys.TempFirstName] = resolvedValue;
        }
        else if (field == "temp last name")
        {
            _scenarioContext[ScenarioKeys.TempLastName] = resolvedValue;
        }
        else if (field == "client name")
        {
            _scenarioContext[ScenarioKeys.ClientName] = resolvedValue;
        }
    }
    
    [Given(@"the user opens '(.*)' url page")]
    public void GivenTheUserOpensUrlPage(string urlSufix)
    {
        _pages.CommonPage.NavigatetoUrl(urlSufix);
    }
    
    [Given(@"the user navigate to '(.*)' window")]
    public void GivenTheUserNavigateToWindow(string pageIdentifier)
    {
        _pages.LoginPage.NavigateToPage(pageIdentifier);
    }
}