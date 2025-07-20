namespace AdvancedReqnRollTest.Models;

public class AppSettings
{
    public required string Browser { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string LoginUrl { get; set; }
    public required string RunOn { get; set; }
    public required BrowserStackSettings BrowserStack { get; set; }
    public required ConnectionStrings ConnectionStrings { get; set; }
}