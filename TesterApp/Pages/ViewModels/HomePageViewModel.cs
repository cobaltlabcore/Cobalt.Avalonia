using System;
using System.Threading.Tasks;
using Cobalt.Avalonia.Desktop;
using Cobalt.Avalonia.Desktop.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using TesterApp.ViewModels;
using TesterApp.Views;

namespace TesterApp.Pages.ViewModels;

public class HomePageViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;
    private readonly IOverlayService _overlayService;
    private readonly IContentDialogService _contentDialogService;

    public HomePageViewModel(
        INavigationService navigationService,
        IOverlayService overlayService,
        IContentDialogService contentDialogService)
    {
        _navigationService = navigationService;
        _overlayService = overlayService;
        _contentDialogService = contentDialogService;
        NavigateToSettingsCommand = new RelayCommand(NavigateToSettings);
        NavigateToTestCommand = new RelayCommand(NavigateToTest);
        ShowOverlayCommand = new AsyncRelayCommand(ShowOverlay);
        ShowDialogSimpleCommand = new AsyncRelayCommand(ShowDialogSimple);
        ShowDialogComplexCommand = new AsyncRelayCommand(ShowDialogComplex);
    }

    public IServiceProvider ServiceProvider => Program.Bootstrapper.ServiceProvider;
    public IFilePickerService FilePickerService => ServiceProvider.GetRequiredService<IFilePickerService>();
    public IFolderPickerService FolderPickerService => ServiceProvider.GetRequiredService<IFolderPickerService>();

    public double DoubleValue
    {
        get => _doubleValue;
        set => SetProperty(ref _doubleValue, value);
    }
    private double _doubleValue;
    
    public RelayCommand NavigateToSettingsCommand { get; }
    public RelayCommand NavigateToTestCommand { get; }
    public AsyncRelayCommand ShowOverlayCommand { get; }
    public AsyncRelayCommand ShowDialogSimpleCommand { get; }
    public AsyncRelayCommand ShowDialogComplexCommand { get; }

    private void NavigateToSettings()
    {
        _navigationService.NavigateTo(typeof(SettingsPage));
    }
    
    private void NavigateToTest()
    {
        _navigationService.NavigateTo(typeof(TestPage));
    }

    private async Task ShowOverlay()
    {
        var progressView = new OverlayProgressView { Width = 500 };
        var vm = new OverlayProgressViewModel();
        progressView.DataContext = vm;

        try
        {
            await _overlayService.ShowAsync(progressView);
            await DoSomethingWithProgress(vm);
        }
        finally
        {
            await _overlayService.HideAsync();
        }
    }
    
    private async Task DoSomethingWithProgress(IProgress progress)
    {
        progress.UpdateProgress(message: "Initializing", isIndeterminate: true);
        await Task.Delay(3000);
        
        progress.UpdateProgress(isIndeterminate: false);
        var total = 200;
        var cpt = 0;

        while (cpt++ < total)
        {
            progress.UpdateProgress(
                value: cpt / (double)total * 100,
                message: Guid.NewGuid().ToString());
            await Task.Delay(10);
        }
    }

    private async Task ShowDialogSimple()
    {
        var result = await _contentDialogService.ShowAsync(
            title: "Test",
            content: "Test dialog",
            buttons: ContentDialogButtons.YesNoCancel);
    }
    
    private async Task ShowDialogComplex()
    {
        //TODO: find a way to pass the dialog to the viewmodel
        //      or a kind of func that will be called by the viewmodel
        //      to handle the IsButtonEnabled for primary and secondary buttons
        
        // var content = new ContentDialogComplexView();
        //
        // var dialog = new ContentDialog
        // {
        //     PrimaryButtonText = "Ok",
        //     Title = "Test",
        //     Content = content
        // };
        //
        // var vm = new ContentDialogComplexViewModel(dialog);
        // content.DataContext = vm;
        //
        // var result = await _contentDialogService.ShowAsync(
        //     title: "Test",
        //     content: content,
        //     buttons: ContentDialogButtons.Ok);
        //
        // if (result == ContentDialogResult.Ok)
        // {
        //     await _contentDialogService.ShowAsync(
        //         title: "Result",
        //         content: $"Password: {vm.Pass1}",
        //         buttons: ContentDialogButtons.Ok);
        // }
        
    }
}