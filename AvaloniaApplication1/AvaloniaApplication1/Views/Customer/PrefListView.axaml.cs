using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Helpers;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.ViewModels.Customer;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication1.Views.Customer;

public partial class PrefListView : UserControl {
    private PrefListViewModel _prefListViewModel;
    private readonly string infinity = "Infinity";
    private int lastSelected = -1;

    public PrefListView() {
        InitializeComponent();
    }
    
    private void _PrefList_OnTapped(object? sender, TappedEventArgs e) {
        _prefListViewModel = (PrefListViewModel) DataContext;
        if (lastSelected == -1) {
            lastSelected = _PrefList.SelectedIndex;
            return;
        }
        _prefListViewModel.SwapRestaurants(_PrefList.SelectedIndex, lastSelected);
        // Return to base case
        lastSelected = -1;
    }

    private void _PrefList_OnDoubleTapped(object? sender, TappedEventArgs e) {
        _prefListViewModel = (PrefListViewModel) DataContext;
        if (_PrefList.SelectedIndex != -1) {
            _prefListViewModel.EditRestaurant(_PrefList.SelectedIndex);
        }   
    }
    
    private void TimeLimit_OnInitialized(object? sender, EventArgs e) {
        var timeLimitBtn = sender as ButtonSpinner;
        timeLimitBtn.Content = infinity;
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
            var reg = new Regex(@"\d+");
            int value = int.Parse(reg.Match(content).Value);
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
            
            if (value >= 2 && value <= 4) {
                timeLimitBtn.Content = $"{value} minuty";
            } else {
                timeLimitBtn.Content = $"{value} minut";
            }
        }
    }
}