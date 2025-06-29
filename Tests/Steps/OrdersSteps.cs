using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
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
                foreach (var key in new[] { "firstname", "lastname" })
                {
                    if (details.TryGetValue(key, out var value) && value == "<unique_text>")
                    {
                        var resolvedValue = _pages.CommonPage.ResolveDynamicValue("<unique_text>");
                        _scenarioContext[$"Temp{key}"] = resolvedValue;
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
                _scenarioContext[ScenarioKeys.TempUserId] = tempId;
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
                        _scenarioContext[$"{key}"] = resolvedValue;
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
                _scenarioContext[ScenarioKeys.ClientUserId] = clientId;
                Console.WriteLine("Saved Client ID: " + clientId);
            }
                break;
        }
        
    }
}