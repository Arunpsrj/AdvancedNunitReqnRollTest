using System;
using System.Text.RegularExpressions;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class ClientProfilePage : BasePage
{
    public ClientProfilePage(IWebDriver driver) : base(driver)
    {
        
    }
    
    private string ClientRecordInfo => "//legend[text()='Record Info']/parent::fieldset";
    
    public string GetClientIdFromFormData()
    {
        string fullText = WaitAndFind(GetBy(LocatorType.XPath, ClientRecordInfo)).Text;

        var match = Regex.Match(fullText, @"Client ID:\s*(\d+)");
        if (!match.Success)
            throw new Exception("Client ID not found in the form data section.");
        var clientId = match.Groups[1].Value;
        return clientId;
    }
}