using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.ViewModels.Customer;
using DynamicData;

namespace AvaloniaApplication1.Views.Customer;

public partial class PrefListView : UserControl {
    private PrefListViewModel _prefListViewModel;
    private int lastSelected = -1;
    private bool swapped = true;

    public PrefListView() {
        InitializeComponent();
    }

    private void _PrefList_OnTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Tapped.");
        if (swapped) {
            lastSelected = _PrefList.SelectedIndex;
            swapped = false;
            return;
        }
        // Swap the two items
        _prefListViewModel = (PrefListViewModel) DataContext;
        _prefListViewModel.SwapRestaurants(_PrefList.SelectedIndex, lastSelected);
        // Base case
        lastSelected = -1;
        swapped = true;
    }

    private void _PrefList_OnDoubleTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Double tapped");
        _prefListViewModel = (PrefListViewModel) DataContext;
        _prefListViewModel.EditRestaurant(_PrefList.SelectedIndex);
    }
}