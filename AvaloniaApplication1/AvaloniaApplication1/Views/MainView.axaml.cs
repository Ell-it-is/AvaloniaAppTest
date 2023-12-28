using System;
using Avalonia.Controls;
using Avalonia.Input;
using AvaloniaApplication1.ViewModels;
using Console = System.Console;

namespace AvaloniaApplication1.Views;

public partial class MainView : UserControl {
    public MainView() {
        InitializeComponent();
    }

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e) {
        // pressing shift + custom mouse button (binded to PageDown)
        if (e.Key == Key.PageDown && (e.KeyModifiers & KeyModifiers.Shift) != 0) {
            App._MainViewModel.OnBackRequested(sender, e);
        }
    }
}