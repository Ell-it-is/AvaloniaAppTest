using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.ViewModels.Customer;
using DynamicData;

namespace AvaloniaApplication1.Views.Customer;

public partial class PrefListView : UserControl {
    private PrefListViewModel _prefListViewModel;
    private int lastSelected = -1;

    public PrefListView() {
        InitializeComponent();
    }
    
    private void _PrefList_OnTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Tapped.");
        int curSelected = _PrefList.SelectedIndex;
        if (curSelected == lastSelected) {
            _PrefList.UnselectAll();
        } else if (lastSelected != -1) {
            // Swap the two items
            _prefListViewModel = (PrefListViewModel) DataContext;
            _prefListViewModel.SwapRestaurants(curSelected, lastSelected);
        }
        lastSelected = _PrefList.SelectedIndex;
    }

    private void _PrefList_OnDoubleTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Double tapped");
        _prefListViewModel = (PrefListViewModel) DataContext;
        _prefListViewModel.EditRestaurant(_PrefList.SelectedIndex);
    }
}