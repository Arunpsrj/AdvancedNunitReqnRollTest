using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AdvancedReqnRollTest.Pages;

public class CommonPage : BasePage
{
    public CommonPage(IWebDriver driver) : base(driver)
    {
    }

    private string TabNameIdValue => "cv_Tab_{0}";
    private string NewTempLinkXpath => "//a[text()='New']";
    private string NewOrderLinkXpath => "//a[text()='New']";
    private Dictionary<string, string> _buttonXpaths;
    private static readonly Random _random = new();
    private static readonly Regex DateAddPattern = new(@"<getDate\+(\d+)>", RegexOptions.IgnoreCase);
    private static readonly Regex DateSubPattern = new(@"<getDate-(\d+)>", RegexOptions.IgnoreCase);
    private static readonly Regex TodayPattern   = new(@"<getDate>", RegexOptions.IgnoreCase);

    private Dictionary<string, string> ButtonXpaths => _buttonXpaths ??= new()
    {
        { "New Temp link", NewTempLinkXpath },
        { "RN Cert", RNCertButtonXpath },
        { "ER Spec", ERSpecButtonXpath },
        { "Temp Save", SaveButtonXpath },
        { "New Client link", NewClientLinkXpath },
        { "Client Save", SaveButtonXpath },
        {"New Order link", NewOrderLinkXpath}
    };
    
    public void NavigateToTab(string tabName)
    {
        Click(GetBy(LocatorType.Id, TabNameIdValue, tabName));
    }

    public void ClickButtonXpath(string buttonName)
    {
        if (ButtonXpaths.TryGetValue(buttonName, out var xpath))
        {
            Click(GetBy(LocatorType.XPath, xpath));
        }
        else
        {
            throw new ArgumentException($"No XPath found for button: {buttonName}");
        }
    }
    
    public string ResolveDynamicValue(string value)
    {
        return value.Equals("<unique_text>", StringComparison.OrdinalIgnoreCase)
            ? GenerateRandomWord(7)
            : value;
    }

    private string GenerateRandomWord(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[_random.Next(chars.Length)]).ToArray());
    }

    public void SelectFromDropDown(string dropDownValue, string dropDownName)
    {
        if (string.IsNullOrWhiteSpace(dropDownName))
            throw new ArgumentException("Dropdown name cannot be null or empty.", nameof(dropDownName));

        By locator = dropDownName switch
        {
            "Status" => GetBy(LocatorType.Name, StatusDropDownName),
            "HomeRegion" => GetBy(LocatorType.Id, HomeRegionDropDownId),
            "Region" => GetBy(LocatorType.Id, RegionDropDownId),
            _ => throw new ArgumentOutOfRangeException(nameof(dropDownName), $"Dropdown '{dropDownName}' is not supported.")
        };

        SelectDropdownByText(locator, dropDownValue);
    }
    
    public void FillAddressFor(string type, AddressModel addressModel)
    {
        Dictionary<string, string> fieldMap = type switch
        {
            "temp-permanent" or "client" => new()
            {
                { "address", AddressLineId },
                { "city", CityId },
                { "state", StateId },
                { "zip", ZipId }
            },
            "temp-temporary" => new()
            {
                { "address", TemporaryAddressLineId },
                { "city", TemporaryCityId },
                { "state", TemporaryStateId },
                { "zip", TemporaryZipId }
            },
            _ => throw new ArgumentException($"Unsupported address type: {type}")
        };

        EnterText(GetBy(LocatorType.Id, fieldMap["address"]), addressModel.Address);
        EnterText(GetBy(LocatorType.Id, fieldMap["city"]), addressModel.City);
        EnterText(GetBy(LocatorType.Id, fieldMap["state"]), addressModel.State);
        EnterText(GetBy(LocatorType.Id, fieldMap["zip"]), addressModel.Zip);
    }
    
    public void EnterTextForField(string fieldLabel, string value)
    {
        var locator = fieldLabel switch
        {
            "temp first name" => GetBy(LocatorType.Name, TempFirstNameId),
            "temp last name" => GetBy(LocatorType.Name, TempLastNameId),
            "client name" => GetBy(LocatorType.Id,ClientNameId),
            // Add more field mappings as needed
            _ => throw new ArgumentException($"No mapping defined for: {fieldLabel}")
        };
        EnterText(locator, value);
    }

    public void NavigatetoUrl(string urlSufix)
    {
        Driver.Navigate().GoToUrl(AppUrls.BaseUrl+urlSufix);
        WaitForPageToLoad();
    }
    
    public string ResolveDatePattern(string datePattern)
    {
        if (string.IsNullOrWhiteSpace(datePattern))
            return string.Empty;

        // <getDate+X>
        if (DateAddPattern.IsMatch(datePattern))
        {
            var match = DateAddPattern.Match(datePattern);
            if (int.TryParse(match.Groups[1].Value, out int daysToAdd))
            {
                return DateTime.Today.AddDays(daysToAdd).ToString("yyyy-MM-dd");
            }
        }

        // <getDate-X>
        if (DateSubPattern.IsMatch(datePattern))
        {
            var match = DateSubPattern.Match(datePattern);
            if (int.TryParse(match.Groups[1].Value, out int daysToSubtract))
            {
                return DateTime.Today.AddDays(-daysToSubtract).ToString("yyyy-MM-dd");
            }
        }

        // <getDate>
        if (TodayPattern.IsMatch(datePattern))
        {
            return DateTime.Today.ToString("yyyy-MM-dd");
        }

        // No match found — return original pattern or empty
        return datePattern;
    }
}