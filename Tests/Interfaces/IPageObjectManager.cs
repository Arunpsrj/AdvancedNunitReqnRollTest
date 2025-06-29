using AdvancedReqnRollTest.Pages;

namespace AdvancedReqnRollTest.Interfaces;

public interface IPageObjectManager
{
    LoginPage LoginPage { get; }
    TempProfilePage  TempProfilePage { get; }
    CommonPage CommonPage { get; }
    ClientProfilePage ClientProfilePage { get; }
    OrderPage OrderPage { get; }
}