using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using AdvancedReqnRollTest.Registry;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class OrdersSteps
{
    private readonly ScenarioContext _scenarioContext;
    private IPageObjectManager _pages;
    
    public OrdersSteps(IPageObjectManager pages, ScenarioContext scenarioContext)
    {
        _pages = pages;
        _scenarioContext = scenarioContext;
    }


    [Given("the user creates new {string} with following details")]
    public void GivenTheUserCreatesNewWithFollowingDetails(string user, Reqnroll.Table table)
    {
        switch (user.ToLower())
        {
            case "temp":
            {
                var details = table.Rows.ToDictionary(row => row["Field"].ToLower(), row => row["Value"]);

                // Resolve dynamic placeholders
                var tempFields = new Dictionary<string, string>
                {
                    { "firstname", ScenarioKeys.TempFirstName },
                    { "lastname", ScenarioKeys.TempLastName }
                };

                foreach (var kvp in tempFields)
                {
                    var key = kvp.Key;
                    var scenarioKey = kvp.Value;

                    if (details.TryGetValue(key, out var value) && value == "<unique_text>")
                    {
                        var resolvedValue = _pages.CommonPage.ResolveDynamicValue("<unique_text>");
                        _scenarioContext[scenarioKey] = resolvedValue;
                        details[key] = resolvedValue;
                        Console.WriteLine($"Temp{key} is {resolvedValue}");
                    }
                }

                var tempDetails = new TempModel
                {
                    Firstname = details.GetValueOrDefault("firstname"),
                    Lastname = details.GetValueOrDefault("lastname"),
                    Status = details.GetValueOrDefault("status"),
                    HomeRegion = details.GetValueOrDefault("homeregion"),
                    Certification = details.GetValueOrDefault("certification"),
                    Speciality = details.GetValueOrDefault("speciality"),
                    Address = details.GetValueOrDefault("address"),
                    City = details.GetValueOrDefault("city"),
                    State = details.GetValueOrDefault("state"),
                    Zip = details.GetValueOrDefault("zip")
                };

                _pages.OrderPage.CreateTempWithDetails(tempDetails);
                var tempId = _pages.TempProfilePage.GetTempIdFromFormData();
                _scenarioContext[ScenarioKeys.TempId] = tempId;
                Console.WriteLine($"Saved Temp ID: {tempId}");
                break;
            }
            case "client":
            {
                var details = table.Rows.ToDictionary(row => row["Field"].ToLower(), row => row["Value"]);
                
                foreach (var key in new[] { "clientname"})
                {
                    if (details.TryGetValue(key, out var value) && value == "<unique_text>")
                    {
                        var resolvedValue = _pages.CommonPage.ResolveDynamicValue("<unique_text>");
                        _scenarioContext[ScenarioKeys.ClientName] = resolvedValue;
                        details[key] = resolvedValue;
                        Console.WriteLine($"{key} is {resolvedValue}");
                    }
                }

                var clientDetails = new ClientModel()
                {
                    Clientname = details.GetValueOrDefault("clientname"),
                    Status = details.GetValueOrDefault("status"),
                    Region = details.GetValueOrDefault("region"),
                    Address = details.GetValueOrDefault("address"),
                    City = details.GetValueOrDefault("city"),
                    State = details.GetValueOrDefault("state"),
                    Zip = details.GetValueOrDefault("zip")
                };

                _pages.OrderPage.CreateClientWithDetails(clientDetails);
                string clientId = _pages.ClientProfilePage.GetClientIdFromFormData();
                _scenarioContext[ScenarioKeys.ClientId] = clientId;
                Console.WriteLine("Saved Client ID: " + clientId);
                break;
            }
            case "order":
            {
                var details = table.Rows.ToDictionary(row => row["Field"].ToLower(), row => row["Value"]);

                foreach (var key in details.Keys.ToList()) // Use ToList() to safely modify dictionary
                {
                    if (!details.TryGetValue(key, out var value)) continue;

                    switch (key)
                    {
                        case "clientname":
                            if (value.Contains("scenario", StringComparison.OrdinalIgnoreCase))
                            {
                                details[key] = _scenarioContext.Get<string>(ScenarioKeys.ClientName);
                            }
                            break;
                        case "tempname":
                            if (value.Contains("scenario", StringComparison.OrdinalIgnoreCase))
                            {
                                var firstName = _scenarioContext.Get<string>(ScenarioKeys.TempFirstName);
                                var lastName = _scenarioContext.Get<string>(ScenarioKeys.TempLastName);
                                details[key] = $"{firstName} {lastName}";
                            }
                            break;
                        case "startdate":
                            if (value.Contains("getDate", StringComparison.OrdinalIgnoreCase))
                            {
                                details[key] = _pages.CommonPage.ResolveDatePattern(details[key]);
                            }
                            break;
                    }
                }
                
                var orderDetails = new OrderModel()
                {
                    Clientname = details.GetValueOrDefault("clientname"),
                    Tempname = details.GetValueOrDefault("tempname"),
                    BookingRegion = details.GetValueOrDefault("bookingregion"),
                    StartDate = details.GetValueOrDefault("startdate"),
                    Certification = details.GetValueOrDefault("certification"),
                    Speciality = details.GetValueOrDefault("speciality"),
                    ShiftId = details.GetValueOrDefault("shiftid")
                };
                
                _pages.OrderPage.CreateOrderWithDetails(orderDetails);
                _pages.LoginPage.NavigateToPage("parent");
                _scenarioContext[ScenarioKeys.OrderId] = _pages.OrderPage.NewlyCreatedOrderId();
                Console.WriteLine("OrderId:"  +  _scenarioContext[ScenarioKeys.OrderId]);
                break;
            }
        }
    }
}