using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using AdvancedReqnRollTest.Pages;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Config;

public class PageObjectManager : IPageObjectManager
{
    private readonly IWebDriver _driver;
    private readonly AppSettings _settings;

    private LoginPage _loginPage;
    private TempProfilePage _tempProfilePage;
    private CommonPage _commonPage;
    private ClientProfilePage _clientProfilePage;
    private OrderPage _orderPage;

    public PageObjectManager(IWebDriver driver, AppSettings settings)
    {
        _driver = driver;
        _settings = settings;
    }

    public LoginPage LoginPage =>
        _loginPage ??= new LoginPage(_driver, _settings);

    public TempProfilePage TempProfilePage =>
        _tempProfilePage ??= new TempProfilePage(_driver);

    public CommonPage CommonPage =>
        _commonPage ??= new CommonPage(_driver);
    
    public ClientProfilePage ClientProfilePage =>
        _clientProfilePage ??= new ClientProfilePage(_driver);
    
    public OrderPage OrderPage =>
        _orderPage ??= new OrderPage(_driver);
}