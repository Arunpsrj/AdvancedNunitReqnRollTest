using AdvancedReqnRollTest.Config;
using Allure.Net.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;

namespace AdvancedReqnRollTest.Hooks;

[Binding]
[AllureNUnit]
[AllureSuite("Login Suite")]
[AllureSubSuite("Valid Login Tests")]
public class TestHooks
{
    private readonly IObjectContainer _objectContainer;
    private IWebDriver _driver;

    public TestHooks(IObjectContainer objectContainer)
    {
        _objectContainer = objectContainer;
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        _driver = ReqnrollServiceRegistration.RegisterServices(_objectContainer);
        AllureLifecycle.Instance.CleanupResultDirectory();
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driver?.Quit();
    }
}