namespace AdvancedReqnRollTest.RestAPI;

public class RestFactory
{
    public RestBuilder Create(string baseUrl)
    {
        var restLibrary = new RestLibrary(baseUrl);
        return new RestBuilder(restLibrary);
    }
}