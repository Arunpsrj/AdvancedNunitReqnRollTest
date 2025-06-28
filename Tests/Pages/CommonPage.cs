using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Config;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class CommonPage : BasePage
{
    public CommonPage(IWebDriver driver) : base(driver)
    {
        
    }

    private string TabNameIdValue => "cv_Tab_{0}";
    private static string NewTempLinkXpath => "//a[text()='New']";
    private static string RNCertButtonXpath => "//li[@title='RN']";
    private static string ERSpecButtonXpath => "//li[@title='ER']";
    private static string SaveButtonXpath => "//input[@id='saveBtn']";
    private static string StatusDropDownName => "status";
    private static string HomeRegionDropDownId => "HomeRegion";
    private static string RegionDropDownId => "region";
    private static string AddressLineId => "address";
    private static string CityId => "city";
    private static string StateId => "state";
    private static string ZipId => "zip";
    private static string TemporaryAddressLineId => "address_2";
    private static string TemporaryCityId => "city_2";
    private static string TemporaryStateId => "state_2";
    private static string TemporaryZipId => "zip_2";
    private static string NewClientLinkXpath => "//a[text()='New']";
    private string TempFirstNameId => "firstname";
    private string TempLastNameId => "lastname";
    private string ClientNameId => "clientname";
    
    private readonly Dictionary<string, string> _buttonXpaths = new()
    {
        { "New Temp link", NewTempLinkXpath },
        { "RN Cert", RNCertButtonXpath },
        { "ER Spec", ERSpecButtonXpath },
        {"Temp Save", SaveButtonXpath},
        {"New Client link", NewClientLinkXpath },
        {"Client Save", SaveButtonXpath}
    };
    
    public void NavigateToTab(string tabName)
    {
        Click(GetBy(LocatorType.Id, TabNameIdValue, tabName));
    }

    public void ClickButtonXpath(string buttonName)
    {
        if (_buttonXpaths.TryGetValue(buttonName, out var xpath))
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
            ? $"{GenerateRandomWord(7)}"
            : value;
    }

    private string GenerateRandomWord(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var random = new Random();
        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[random.Next(chars.Length)]).ToArray());
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
    
    public void FillAddressFor(string type, Address address)
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

        EnterText(GetBy(LocatorType.Id, fieldMap["address"]), address.address);
        EnterText(GetBy(LocatorType.Id, fieldMap["city"]), address.city);
        EnterText(GetBy(LocatorType.Id, fieldMap["state"]), address.state);
        EnterText(GetBy(LocatorType.Id, fieldMap["zip"]), address.zip);
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
}