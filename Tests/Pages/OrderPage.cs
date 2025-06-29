using System.Collections.Generic;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class OrderPage : BasePage   
{
    public OrderPage(IWebDriver driver) : base(driver)
    {
        
    }

    public void CreateTempWithDetails(TempModel tempDetails)
    {
        Driver.Navigate().GoToUrl(AppUrls.BaseUrl+AppUrls.TempNewUrl);
        EnterText(GetBy(LocatorType.Name,TempFirstNameId),tempDetails.Firstname);
        EnterText(GetBy(LocatorType.Name,TempLastNameId),tempDetails.Lastname);
        SelectDropdownByText(GetBy(LocatorType.Id,StatusDropDownName), tempDetails.Status);
        SelectDropdownByText(GetBy(LocatorType.Id, HomeRegionDropDownId), tempDetails.HomeRegion);
        EnterText(GetBy(LocatorType.Id,CertTextBoxId),tempDetails.Certification);
        Click(GetBy(LocatorType.XPath, CertPartialXpath,tempDetails.Certification));
        EnterText(GetBy(LocatorType.Id,SpecTextBoxId),tempDetails.Speciality);
        Click(GetBy(LocatorType.XPath, SpecPartialXpath,tempDetails.Speciality));
        EnterText(GetBy(LocatorType.Id, AddressLineId), tempDetails.Address);
        EnterText(GetBy(LocatorType.Id, CityId), tempDetails.City);
        EnterText(GetBy(LocatorType.Id, StateId), tempDetails.State);
        EnterText(GetBy(LocatorType.Id, ZipId), tempDetails.Zip);
        Click(GetBy(LocatorType.XPath, SaveButtonXpath));
    }
    
    public void CreateClientWithDetails(ClientModel clientDetails)
    {
        Driver.Navigate().GoToUrl(AppUrls.BaseUrl+AppUrls.ClientNewUrl);
        EnterText(GetBy(LocatorType.Name,ClientNameId),clientDetails.Clientname);
        SelectDropdownByText(GetBy(LocatorType.Name,StatusDropDownName), clientDetails.Status);
        SelectDropdownByText(GetBy(LocatorType.Id, RegionDropDownId), clientDetails.Region);
        EnterText(GetBy(LocatorType.Id, AddressLineId), clientDetails.Address);
        EnterText(GetBy(LocatorType.Id, CityId), clientDetails.City);
        EnterText(GetBy(LocatorType.Id, StateId), clientDetails.State);
        EnterText(GetBy(LocatorType.Id, ZipId), clientDetails.Zip);
        Click(GetBy(LocatorType.XPath, SaveButtonXpath));
    }
}