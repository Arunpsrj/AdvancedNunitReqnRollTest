using AdvancedReqnRollTest.Interfaces;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class TempProfileSteps
{
    private readonly IPageObjectManager _pages;

    public TempProfileSteps(IPageObjectManager pages)
    {
        _pages = pages;
    }

    [Given("the user navigates to {string} tab")]
    public void GivenTheUserNavigatesToTab(string temp)
    {
        
    }

    [StepDefinition("the user clicks {string}")]
    public void GivenTheUserClicks(string function)
    {
        
    }


    [Given("the user enters {string} for {string}")]
    public void GivenTheUserEntersFor(string p0, string p1)
    {
        
    }


    [Given("the user selects {string} from the {string} dropdown")]
    public void GivenTheUserSelectsFromTheDropdown(string active, string status)
    {
        
    }


    [Given("the user enters {string} as the address for {string}")]
    public void GivenTheUserEntersAsTheAddressFor(string p0, string temp)
    {
        
    }

    [Then("the user verifies the newly created {string} ID is displayed")]
    public void ThenTheUserVerifiesTheNewlyCreatedIdIsDisplayed(string temp)
    {
        
    }
}