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
    private readonly string searchToggleOn = "Searching for couriers!";
    private readonly string searchToggleOff = "Click here, to begin your order";
    private readonly string infinity = "Infinity";
    private int lastSelected = -1;

    public PrefListView() {
        InitializeComponent();
    }

    private void _PrefList_OnTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Tapped.");
        if (lastSelected == -1) {
            lastSelected = _PrefList.SelectedIndex;
            return;
        }
        // Swap the two items
        _prefListViewModel = (PrefListViewModel) DataContext;
        _prefListViewModel.SwapRestaurants(_PrefList.SelectedIndex, lastSelected);
        // Return to base case
        lastSelected = -1;
    }

    private void _PrefList_OnDoubleTapped(object? sender, TappedEventArgs e) {
        Console.WriteLine("Double tapped");
        _prefListViewModel = (PrefListViewModel) DataContext;
        _prefListViewModel.EditRestaurant(_PrefList.SelectedIndex);
    }

    private void SearchCouriersToggleBtn_OnInitialized(object? sender, EventArgs e) {
        SearchCouriersToggleBtn.IsChecked = false;
        SearchCouriersToggleBtn.Content = searchToggleOff;
    }

    private void SearchCouriersToggleBtn_OnClick(object? sender, RoutedEventArgs e) {
        bool isChecked = SearchCouriersToggleBtn.IsChecked ?? false;
        SearchCouriersToggleBtn.Content = isChecked ? searchToggleOn : searchToggleOff;
    }

    private void TimeLimit_OnSpin(object? sender, SpinEventArgs e) {
        var timeLimitBtn = sender as ButtonSpinner;
        string content = timeLimitBtn.Content as string;
        if (content == infinity) {
            if (e.Direction == SpinDirection.Increase) {
                timeLimitBtn.Content = "1 minuta";
                return;
            }
        } else {
            int value = int.Parse(content.Substring(0, 1));
            if (e.Direction == SpinDirection.Increase) {
                value++;
            } else {
                if (value == 1) {
                    timeLimitBtn.Content = infinity;
                    return;
                } else if (value == 2) {
                    timeLimitBtn.Content = "1 minuta";
                    return;
                } else {
                    value--;
                }         
            }
            timeLimitBtn.Content = $"{value} minuty";
        }
    }

    private void TimeLimit_OnInitialized(object? sender, EventArgs e) {
        var timeLimitBtn = sender as ButtonSpinner;
        timeLimitBtn.Content = infinity;
    }
}