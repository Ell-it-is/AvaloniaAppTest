using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.Views;
using Splat;

namespace AvaloniaApplication1;

public partial class App : Application {
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }
    
    public static MainViewModel _MainViewModel { get; set; } = new MainViewModel();

    public override void OnFrameworkInitializationCompleted() {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            desktop.MainWindow = new MainWindow {
                DataContext = _MainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
            singleViewPlatform.MainView = new MainView {
                DataContext = _MainViewModel
            };
            singleViewPlatform.MainView.Loaded += (_, _) =>
                TopLevel.GetTopLevel(singleViewPlatform.MainView)!.BackRequested += _MainViewModel.OnBackRequested;
        }

        base.OnFrameworkInitializationCompleted();
    }
}