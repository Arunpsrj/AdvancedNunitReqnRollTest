using System;
using System.Text.RegularExpressions;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class TempProfilePage : BasePage
{
    public TempProfilePage(IWebDriver driver) : base(driver)
    {
        
    }
    
    private string TempRecordInfo => "//td[text()='Record Info']/following-sibling::td[@class='cv-form-data']";
    
    public string GetTempIdFromFormData()
    {
        string fullText = WaitAndFind(GetBy(LocatorType.XPath, TempRecordInfo)).Text;

        var match = Regex.Match(fullText, @"Temp ID:\s*(\d+)");
        if (!match.Success)
            throw new Exception("Temp ID not found in the form data section.");
        var tempId = match.Groups[1].Value;
        return tempId;
    }
}