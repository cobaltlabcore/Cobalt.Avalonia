namespace Cobalt.Avalonia.Desktop;

public interface INavigationPage
{
    void OnNavigatingTo();
    void OnNavigatedFrom();
}