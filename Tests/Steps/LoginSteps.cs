using AdvancedReqnRollTest.Interfaces;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class LoginSteps
{
    private readonly IPageObjectManager _pages;

    public LoginSteps(IPageObjectManager pages)
    {
        _pages = pages;
    }

    [Given("the {string} user logged into {string} site")]
    public void GivenTheUserLoggedIntoSite(string user, string url)
    {
        _pages.LoginPage.Login(user, url);
    }

    [Given("the user verifies all the texts are displayed")]
    public void GivenTheUserVerifiesAllTheTextsAreDisplayed()
    {
        
    }

    [Given("the user navigate to {string} window")]
    public void GivenTheUserNavigateToWindow(string pageIdentifier)
    {
        _pages.LoginPage.NavigateToPage(pageIdentifier);
    }
}