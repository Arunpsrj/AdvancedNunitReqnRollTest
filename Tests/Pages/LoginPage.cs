using System;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;

public class LoginPage : BasePage
{
    #region Fields

    private readonly AppSettings _settings;

    #endregion 
    
    #region Constructor
    
    public LoginPage(IWebDriver driver, AppSettings settings) : base(driver)
    {
        _settings = settings;
    }
    
    #endregion 
    

    #region Properties

    private string UsernameTextboxIdValue => "loginusername";
    private string PasswordTextboxIdValue => "loginpassword";
    private string LoginButtonIdValue => "login";
    
    #endregion

    #region Methods
    
    public void Login(string username, string url)
    {
        string resolvedUsername, resolvedPassword;

        if (username.Equals("default", StringComparison.OrdinalIgnoreCase))
        {
            resolvedUsername = _settings.Username;
            resolvedPassword = _settings.Password;  
        }
        else
        {
            resolvedUsername = username;
            // need to fetch or pass the password externally for non-default users
            throw new ArgumentException("Password required for non-default users.");
        }

        string targetUrl = url.Equals("default", StringComparison.OrdinalIgnoreCase) ? _settings.LoginUrl : url;
        Driver.Navigate().GoToUrl(targetUrl);

        EnterText(GetBy(LocatorType.Id, UsernameTextboxIdValue), resolvedUsername);
        EnterText(GetBy(LocatorType.Id, PasswordTextboxIdValue), resolvedPassword);
        Click(GetBy(LocatorType.Id, LoginButtonIdValue));
    }
    
    #endregion
}
