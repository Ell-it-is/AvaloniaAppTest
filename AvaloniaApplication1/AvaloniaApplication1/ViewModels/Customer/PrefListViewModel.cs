using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Xaml.Interactions.Core;
using AvaloniaApplication1.Helpers;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using DynamicData;
using DynamicData.Binding;
using ExCSS;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Topten.RichTextKit;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    private int _maxNumberOfPreferences = 4; // can differ based on subscription model
    
    private SourceList<Restaurant> Source { get; set; } = new SourceList<Restaurant>();
    private readonly ReadOnlyObservableCollection<Restaurant> _restaurants;
    public ReadOnlyObservableCollection<Restaurant> Restaurants => _restaurants;
    
    public ReactiveCommand<Unit, Unit> AllowSearchCommand { get; set; }

    public PrefListViewModel() {
        AddRestaurant();
        
        var disposable = Source
            .Connect()
            //.Trace("RestaurantsBindObservable")
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _restaurants)
            .Subscribe();
        
        ReactiveCommand<Unit, Unit> AutoAddCommand = ReactiveCommand.Create(AutoAddRestaurant);
        var canAdd = Source
            .Connect()
            //.Trace("AddRestaurantObservable")
            .WhenAnyPropertyChanged()
            .Select(_ => System.Reactive.Unit.Default)
            .InvokeCommand(AutoAddCommand);
        
        var canAddOnDelete = Source
            .Connect()
            //.Trace("AddOnDeleteRestaurantObservable")
            .CountChanged()
            .Select(_ => System.Reactive.Unit.Default)
            .InvokeCommand(AutoAddCommand);
        
        var allowSearchObservable = Source
            .Connect()
            //.Trace("AllowSearchRestaurantObservable")
            .AutoRefreshOnObservable(
                r => r.WhenAnyValue(x => x.Name))
            .ToCollection()
            .Select(c => c.Any(r => r.IsValid));
        
        AllowSearchCommand = ReactiveCommand.Create(() => { }, allowSearchObservable);
    }

    public void AutoAddRestaurant() {
        if (AllRestaurantsValid() && Source.Count < _maxNumberOfPreferences) 
            AddRestaurant();
    }

    private bool AnyRestaurantValid() => Source.Items.Any(r => r.IsValid);
    private bool AllRestaurantsValid() => Source.Items.All(r => r.IsValid);
    private void AddRestaurant() => Source.Add(new Restaurant());

    public void RemoveRestaurant(ListBoxItem listBoxItem) {
        if (listBoxItem is null) return;
        if (Source.Items.Any()) Source.Remove((Restaurant)listBoxItem.Content);
        if (Source.Items.Count() == 0) AddRestaurant();
    }
    
    /*
     * After tapping on restaurant twice (double tap)
     */
    public void EditRestaurant(int idx) {
        App._MainViewModel.CurrentPage = new RestaurantDetailViewModel(Source.Items.ElementAt(idx));
    }
    
    /*
     * Tap once on a restaurant 'r1'
     * Then tap again on a restaurant 'r2' and swap its positions
     */
    public void SwapRestaurants(int fromIdx, int toIdx) {
        /*if (fromIdx > toIdx) SwapHelper.Swap(ref fromIdx, ref toIdx);
        // Don't allow to swap not edited last restaurant
        // TODO if (toIdx == Restaurants.Count - 1 && Restaurants[toIdx].IsValid is false) return;
        
        var tempRestaurant = Restaurants[fromIdx];
        Restaurants[fromIdx] = Restaurants[toIdx];
        Restaurants[toIdx] = tempRestaurant;*/
    }
    
    // used for testing purpose only
    private void AddTestRestaurants() {
        for (int i = 0; i < _maxNumberOfPreferences; i++) {
            var restaurant = new Restaurant();
            Source.Add(restaurant);
        }
    }
}