using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using DynamicData.Binding;
using ReactiveUI;
using Topten.RichTextKit;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    private int _maxNumberOfPreferences = 4; // can differ based on subscription model
    public ObservableCollection<Restaurant> Restaurants { get; private set; }
    public bool AllRestaurantsEdited { get; set; }

    public PrefListViewModel() {
        Restaurants = new ObservableCollection<Restaurant>();
        
        AddRestaurant();
    }

    public void ToggleOrderCommand(ToggleButton toggleButton) {
        if (AllRestaurantsEdited is false) return;

        Console.WriteLine("Performed!");
        string searchToggleOn = "Searching for couriers!";
        string searchToggleOff = "Click here, to begin your order";
        bool isChecked = toggleButton.IsChecked ?? false;
        toggleButton.Content = isChecked ? searchToggleOn : searchToggleOff;
    }

    // used for testing purpose only
    private void AddTestRestaurants() {
        for (int i = 0; i < _maxNumberOfPreferences; i++) {
            var restaurant = new Restaurant();
            Restaurants.Add(restaurant);
        }
    }

    public void AddRestaurant() {
        var restaurant = new Restaurant();
        Restaurants.Add(restaurant);
    }

    public void RemoveRestaurant(ListBoxItem listBoxItem) {
        if (Restaurants.Any()) Restaurants.Remove((Restaurant)listBoxItem.Content);
        if (Restaurants.Count == 0) AddRestaurant();
    }
    
    /*
     * After tapping on restaurant twice (double tap)
     * -> open screen where you can edit the details
     */
    public void EditRestaurant(int idx) {
        if (idx < 0 || idx >= Restaurants.Count) return;
        App._MainViewModel.CurrentPage = new RestaurantDetailViewModel(Restaurants[idx]);
    }

    public bool CheckIfAllRestaurantsAreEdited() {
        bool all_edited = true;
        for (int i = 0; i < Restaurants.Count; i++) {
            if (!Restaurants[i].Edited) {
                all_edited = false;
                break;
            }
        }

        AllRestaurantsEdited = all_edited;
        return all_edited;
    }

    public void AllEditedAddNew() {
        bool all_edited = CheckIfAllRestaurantsAreEdited();
        if (all_edited && Restaurants.Count < _maxNumberOfPreferences) AddRestaurant();
    }
    
    /*
     * Tap once on a restaurant 'r1'
     * Then tap again on a restaurant 'r2' and swap its positions
     */
    public void SwapRestaurants(int fromIdx, int toIdx) {
        if (fromIdx > toIdx) SwapHelper.Swap(ref fromIdx, ref toIdx);
        // Don't allow to swap not edited last restaurant
        if (toIdx == Restaurants.Count - 1 && Restaurants[toIdx].Edited is false) return;
        
        var tempRestaurant = Restaurants[fromIdx];
        Restaurants[fromIdx] = Restaurants[toIdx];
        Restaurants[toIdx] = tempRestaurant;
    }
}