using AdvancedReqnRollTest.Interfaces;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Config;

public class PageObjectManager : IPageObjectManager
{
    private readonly IWebDriver _driver;
    private readonly AppSettings _settings;

    public PageObjectManager(IWebDriver driver, AppSettings settings)
    {
        _driver = driver;
        _settings = settings;
    }

    private LoginPage _loginPage;
    public LoginPage LoginPage => _loginPage ??= new LoginPage(_driver, _settings);

}