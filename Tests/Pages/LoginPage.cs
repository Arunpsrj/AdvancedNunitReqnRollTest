using System;
using System.Linq;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

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
        
        string baseUrl = targetUrl.Replace("/login.cfm", "");
        AppUrls.BaseUrl = baseUrl;
        
        EnterText(GetBy(LocatorType.Id, UsernameTextboxIdValue), resolvedUsername);
        EnterText(GetBy(LocatorType.Id, PasswordTextboxIdValue), resolvedPassword);
        Click(GetBy(LocatorType.Id, LoginButtonIdValue));
    }
    
    public void NavigateToPage(string pageIdentifier)
    {
        if (string.IsNullOrEmpty(pageIdentifier))
            throw new ArgumentException("Page identifier cannot be null or empty");

        pageIdentifier = pageIdentifier.Trim().ToLower();

        if (pageIdentifier == "parent")
        {
            SwitchToParentWindow();
        }
        else if (pageIdentifier == "popup")
        {
            StoreParentWindow(); // store before switching
            SwitchToChildWindowByIndex(0); // switch to first child
        }
        else if (pageIdentifier.EndsWith("child"))
        {
            int childIndex = GetChildIndexFromIdentifier(pageIdentifier); // e.g., "1stchild" -> 0
            SwitchToChildWindowByIndex(childIndex);
        }
        else
        {
            throw new ArgumentException($"Unsupported page identifier: {pageIdentifier}");
        }
    }

    private int GetChildIndexFromIdentifier(string identifier)
    {
        // Supports 1stchild, 2ndchild, 3rdchild, etc.
        if (identifier.StartsWith("1st")) return 0;
        if (identifier.StartsWith("2nd")) return 1;
        if (identifier.StartsWith("3rd")) return 2;

        if (identifier.StartsWith("4th")) return 3;
        if (identifier.StartsWith("5th")) return 4;

        // fallback: try numeric prefix
        var numberPart = new string(identifier.TakeWhile(char.IsDigit).ToArray());
        if (int.TryParse(numberPart, out int index) && index > 0)
            return index - 1;

        throw new ArgumentException($"Invalid child window format: {identifier}");
    }
    #endregion
}