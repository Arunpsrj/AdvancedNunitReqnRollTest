using AdvancedReqnRollTest.Interfaces;
using Reqnroll;

namespace AdvancedReqnRollTest.Steps;

[Binding]
public class CommonSteps
{
    private readonly IPageObjectManager _pages;

    public CommonSteps(IPageObjectManager pages)
    {
        _pages = pages;
    }
    
    [StepDefinition("the user clicks {string}")]
    public void GivenTheUserClicks(string button)
    {
        _pages.CommonPage.ClickButtonXpath(button);
    }
}