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
using Avalonia.Media;
using Avalonia.Media.Immutable;
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
using Color = Avalonia.Media.Color;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    private int _maxNumberOfPreferences = 4; // can differ based on subscription model
    private readonly string searchToggleDefault = "Po zvoleni preference, muzete vyhledat kuryra";
    private readonly string searchToggleOff = "Vyhledat kuryra";
    private readonly string searchToggleOn = "Cekam na potvrzeni od kuryra v okoli...";
    
    public SourceList<Restaurant> Source { get; set; } = new SourceList<Restaurant>();
    public readonly ReadOnlyObservableCollection<Restaurant> _restaurants;
    public ReadOnlyObservableCollection<Restaurant> Restaurants => _restaurants;
    
    [Reactive] public string ToggleBtnContent { get; set; }
    [Reactive] public bool ToggleBtnIsEnabled { get; set; }
    [Reactive] public bool PrefListIsEnabled { get; set; } = true;
    public ItemCollection MyItems { get; set; }
    
    public PrefListViewModel() {
        AddRestaurant();
        var disposable = Source
            .Connect()
            //.Trace("RestaurantsBindObservable")
            .ObserveOn(RxApp.MainThreadScheduler)
            .Bind(out _restaurants)
            .Subscribe();
        
        ReactiveCommand<Unit, Unit> sourceChangedCommand = ReactiveCommand.Create(ReactToSourceChanged);
        var canAdd = Source
            .Connect()
            //.Trace("AddRestaurantObservable")
            .WhenAnyPropertyChanged()
            .Select(_ => System.Reactive.Unit.Default)
            .InvokeCommand(sourceChangedCommand);
        
        var canAddOnDelete = Source
            .Connect()
            //.Trace("AddOnDeleteRestaurantObservable")
            .CountChanged()
            .Select(_ => System.Reactive.Unit.Default)
            .InvokeCommand(sourceChangedCommand);
    }
    
    public void CheckToggleBtn() {
        ToggleBtnContent = ToggleBtnContent == searchToggleOff ? searchToggleOn : searchToggleOff;
        PrefListIsEnabled = ToggleBtnContent == searchToggleOff;
    }
    
    public void ReactToSourceChanged() {
        CalculatePositions();
        AutoAddRestaurant();
        if (!AnyRestaurantValid()) {
            ToggleBtnContent = searchToggleDefault;
            ToggleBtnIsEnabled = false;
            PrefListIsEnabled = true;
        } else {
            ToggleBtnContent = searchToggleOff;
            ToggleBtnIsEnabled = true;
            PrefListIsEnabled = true;
        }
    }

    public void CalculatePositions() {
        foreach (var r in Source.Items.Where(r => r.IsValid)) {
            r.Position = $"{Source.Items.IndexOf(r) + 1}.";
        }
    }
    
    public void AutoAddRestaurant() {
        if (AllRestaurantsValid() && Source.Count < _maxNumberOfPreferences) 
            AddRestaurant();
    }

    public bool AnyRestaurantValid() => Source.Items.Any(r => r.IsValid);
    public bool AllRestaurantsValid() => Source.Items.All(r => r.IsValid);
    public void AddRestaurant() => Source.Add(new Restaurant());

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
        if (fromIdx > toIdx) SwapHelper.Swap(ref fromIdx, ref toIdx);
        // Don't allow to swap not edited last restaurant
        if (Source.Items.ElementAt(fromIdx).IsValid == false || Source.Items.ElementAt(toIdx).IsValid == false) return;
        
        var fromRestaurant = Restaurants[fromIdx];
        var toRestaurant = Restaurants[toIdx];
        
        Source.ReplaceAt(fromIdx, toRestaurant);
        Source.ReplaceAt(toIdx, fromRestaurant);
        CalculatePositions();
    }
    
    // used for testing purpose only
    private void AddTestRestaurants() {
        for (int i = 0; i < _maxNumberOfPreferences; i++) {
            var restaurant = new Restaurant();
            Source.Add(restaurant);
        }
    }
    
    /*
     Idk could be useful in the future:
     
     var allowSearchObservable = Source
    .Connect()
    //.Trace("AllowSearchRestaurantObservable")
    .AutoRefreshOnObservable(
        r => r.WhenAnyValue(x => x.Name))
    .ToCollection()
    .Select(c => c.Any(r => r.IsValid));*/
    // AllowSearchCommand = ReactiveCommand.Create<ToggleButton>(ChangeToggleBtn, allowSearchObservable);
}