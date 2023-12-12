using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using AvaloniaApplication1.Services;
using ReactiveUI;
using Splat;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase {
    public MainViewModel() {
        _CurrentPage = PagesService.GetPages().First();
    }
    public ViewModelBase _CurrentPage;
    public ViewModelBase CurrentPage {
        get => _CurrentPage;
        set => this.RaiseAndSetIfChanged(ref _CurrentPage, value);
    }

    public void BackRequested() {
        CurrentPage = PagesService.GetPages()[0]; // might fix later to go left based on current idx
    }
}