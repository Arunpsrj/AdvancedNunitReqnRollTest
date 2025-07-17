using System;
using System.Collections.Generic;

namespace AdvancedReqnRollTest.Registry;

public class RequestMethodRegistry
{
    private static readonly HashSet<string> GetMethods = new()
    {
        "getTemps",
        "getOrders",
        // Add other GET-type actions here
    };

    private static readonly HashSet<string> PostMethods = new()
    {
        "insertOrder",
        "createTemp",
        "submitForm"
        // Add other POST-type actions here
    };

    public static string GetHttpMethodFor(string action)
    {
        if (GetMethods.Contains(action)) return "GET";
        if (PostMethods.Contains(action)) return "POST";

        throw new NotSupportedException($"Action '{action}' is not mapped to any HTTP method.");
    }
}