using System.Threading.Tasks;
using Avalonia.Controls;

namespace Cobalt.Avalonia.Desktop;

public interface IOverlayService
{
    void SetOverlayHost(ContentControl host);
    Task ShowAsync(Control content);
    Task HideAsync();
}