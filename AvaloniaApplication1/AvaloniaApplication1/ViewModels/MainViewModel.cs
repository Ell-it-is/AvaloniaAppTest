using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AvaloniaApplication1.Services;
using ReactiveUI;
using Splat;

namespace AvaloniaApplication1.ViewModels;

public class MainViewModel : ViewModelBase {
    private Stack<ViewModelBase> _LastPages = new Stack<ViewModelBase>();
    public ViewModelBase _CurrentPage;
    public ViewModelBase CurrentPage {
        get => _CurrentPage;
        set {
            this.RaiseAndSetIfChanged(ref _CurrentPage, value);
            _LastPages.Push(value);
        }
    }
    
    public MainViewModel() {
        //CurrentPage = PagesService.GetPages().First();
        CurrentPage = PagesService.GetPages()[1];
    }

    private void BackRequested() {
        // Pop the last visited page (the one you are currently on)
        if (_LastPages.Count() > 0) _LastPages.Pop();
        
        if (_LastPages.Count == 0) CurrentPage = PagesService.GetPages().First();
        else CurrentPage = _LastPages.Pop();
    }
    
    public void OnBackRequested(object? sender, RoutedEventArgs e) {
        BackRequested();
        e.Handled = true;
    }
}