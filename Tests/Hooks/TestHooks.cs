using AdvancedReqnRollTest.Config;
using Reqnroll;
using Reqnroll.BoDi;
using OpenQA.Selenium;

[Binding]
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
    }

    [AfterScenario]
    public void AfterScenario()
    {
        _driver?.Quit();
    }
}