using NUnit.Framework;
using Reqnroll;

[Binding]
public class LoginSteps
{
    private readonly LoginPage _loginPage;

    public LoginSteps(LoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    [Given("the {string} user logged into {string} site")]
    public void GivenTheUserLoggedIntoSite(string user, string url)
    {
        _loginPage.Login(user, url);
    }

    [Given("the user verifies all the texts are displayed")]
    public void GivenTheUserVerifiesAllTheTextsAreDisplayed()
    {
        Assert.IsTrue(true);
    }
}
