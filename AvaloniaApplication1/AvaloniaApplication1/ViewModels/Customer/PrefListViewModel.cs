using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using ReactiveUI;
using Topten.RichTextKit;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    private int _maxNumberOfPreferences = 4; // can differ based on subscription model
    public ObservableCollection<Restaurant> Restaurants { get; private set; }

    public PrefListViewModel() {
        Restaurants = new ObservableCollection<Restaurant>();
        AddTestRestaurants();
    }

    private void AddTestRestaurants() {
        for (int i = 0; i < _maxNumberOfPreferences; i++) {
            var restaurant = new Restaurant() { Name = "Restaurant " + (i + 1).ToString() };
            Restaurants.Add(restaurant);
        }
    }

    public void AddRestaurant() {
        var restaurant = new Restaurant() { Name = "Add new preference" };
        Restaurants.Add(restaurant);
    }

    public void RemoveRestaurant(ListBoxItem listBoxItem) {
        if (Restaurants.Any()) Restaurants.Remove((Restaurant)listBoxItem.Content);
    }
    
    /*
     * After tapping on restaurant twice (double tap)
     * -> open screen where you can edit the details
     */
    public void EditRestaurant(int idx) {
        App._MainViewModel.CurrentPage = new RestaurantDetailViewModel(Restaurants[idx]);
        Restaurants[idx].Edited = true; // Considering restaurant as edited if user double taps (behaviour for now)
        AllEditedAddNew();
    }

    private void AllEditedAddNew() {
        bool all_edited = true;
        for (int i = 0; i < Restaurants.Count; i++) {
            if (!Restaurants[i].Edited) {
                all_edited = false;
                break;
            }
        }
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