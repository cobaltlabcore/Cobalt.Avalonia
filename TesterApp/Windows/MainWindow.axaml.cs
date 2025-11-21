using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Cobalt.Avalonia.Desktop;
using FluentAvalonia.UI.Windowing;
using MdiAvalonia;
using TesterApp.Pages;
using TesterApp.Windows.ViewModels;

namespace TesterApp.Windows;

public partial class MainWindow : AppWindow
{
    private readonly INavigationService _navigationService;

    public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();
        DataContext = viewModel;
        Loaded += OnLoaded;
        var drawingImage = new IconsFactory().CreateDrawingImage(MdiAvalonia.Icon.cog, Brushes.White);
        // Icon = new WindowIcon(CreateBitmapFromDrawingImage(drawingImage, 32, 32));
        Icon = CreateBitmapFromDrawingImage(drawingImage, 32, 32);
    }
    
    private Bitmap CreateBitmapFromDrawingImage(DrawingImage drawingImage, int width, int height)
    {
        // Create an Image control to display the DrawingImage
        var image = new Image
        {
            Source = drawingImage,
            Width = width,
            Height = height,
            Stretch = Stretch.Uniform
        };

        // Create a container (Canvas or Border) to hold the image
        var container = new Canvas
        {
            Width = width,
            Height = height,
            Background = Brushes.Transparent
        };
        
        container.Children.Add(image);

        // Measure and arrange the container
        container.Measure(new Size(width, height));
        container.Arrange(new Rect(0, 0, width, height));

        // Create render target bitmap and render the container
        var renderBitmap = new RenderTargetBitmap(
            new PixelSize(width, height),
            new Vector(96, 96));
        
        renderBitmap.Render(container);
        
        return renderBitmap;
    }




    private void OnLoaded(object? sender, RoutedEventArgs e)
    {
        _navigationService.NavigateTo(typeof(HomePage));
    }
}