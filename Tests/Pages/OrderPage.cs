using System.Collections.Generic;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using AdvancedReqnRollTest.Registry;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class OrderPage : BasePage   
{
    public OrderPage(IWebDriver driver) : base(driver)
    {
        
    }
    
    private string ClientSearchName => "button2";
    private string TempSearchBoxId => "tempSelector";
    private string TempSearchXpath => "(//input[@value='Search'])[2]";
    private string BookingRegionId => "bookingRegion";
    private string StartDateId => "jobdatestart_display";
    private string ShiftIdPartialXpath => "//option[text()='{0}']";
    private string SaveDone_Id => "createdone";
    private string TempConfirm_Name => "TempConfirmYN";
    private string ClientConfirm_Name => "ClientConfirmYN";
    private string FillOrder_Id => "confirmed1";
    private string OrderId_Xpath => "//td[contains(text(),'Your order has been created.')]/a";
    private string Text_a_tag_PartialXpath => "//a[text()='{0}']";
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
    
    public void CreateOrderWithDetails(OrderModel orderDetails)
    {
        EnterText(GetBy(LocatorType.Name,ClientNameId),orderDetails.Clientname);
        Click(GetBy(LocatorType.Name,ClientSearchName));
        WaitUntilVisible(GetBy(LocatorType.XPath,Text_a_tag_PartialXpath,orderDetails.Clientname));
        EnterText(GetBy(LocatorType.Id,TempSearchBoxId),orderDetails.Tempname);
        Click(GetBy(LocatorType.XPath,TempSearchXpath));
        SelectDropdownByText(GetBy(LocatorType.Id,BookingRegionId),  orderDetails.BookingRegion);
        EnterText(GetBy(LocatorType.Id, StartDateId), orderDetails.StartDate);
        Click(GetBy(LocatorType.XPath,ShiftIdPartialXpath,orderDetails.ShiftId));
        EnterText(GetBy(LocatorType.Id,CertTextBoxId),orderDetails.Certification);
        Click(GetBy(LocatorType.XPath, CertPartialXpath,orderDetails.Certification));
        EnterText(GetBy(LocatorType.Id,SpecTextBoxId),orderDetails.Speciality);
        Click(GetBy(LocatorType.XPath, SpecPartialXpath,orderDetails.Speciality));
        Click(GetBy(LocatorType.Id,SaveDone_Id));
        Click(GetBy(LocatorType.Name, TempConfirm_Name));
        Click(GetBy(LocatorType.Name, ClientConfirm_Name));
        Click(GetBy(LocatorType.Id, FillOrder_Id));
    }
    
    public string NewlyCreatedOrderId()
    {
        var orderId = WaitAndFind(GetBy(LocatorType.XPath, OrderId_Xpath)).Text.Trim();
        return orderId;
    }
}