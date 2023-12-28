using System;
using System.Collections.ObjectModel;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    private int _maxNumberOfPreferences = 4; // can differ based on subscription model
    public ObservableCollection<Restaurant> RestaurantsList { get; private set; }

    private ViewModelBase[] RestaurantsPages;
    public ViewModelBase[] GetRestaurantsPages => RestaurantsPages;

    public PrefListViewModel() {
        RestaurantsList = new ObservableCollection<Restaurant>();
        RestaurantsPages = new ViewModelBase[_maxNumberOfPreferences];
        
        for (int i = 0; i < _maxNumberOfPreferences; i++) {
            RestaurantsList.Add(new Restaurant());
            RestaurantsPages[i] = new RestaurantDetailViewModel();
        }
    }
    
    /*
     * After tapping on restaurant twice (double tap)
     * -> open screen where you can edit the details
     */
    public void EditRestaurant(int idx) {
        Console.WriteLine(idx);
    }
    
    /*
     * Tap once on a restaurant 'r1'
     * Then tap again on a restaurant 'r2' and swap its positions
     */
    public void SwapRestaurants(int fromIdx, int toIdx) {
        var temp = RestaurantsList[fromIdx];
        RestaurantsList[fromIdx] = RestaurantsList[toIdx];
        RestaurantsList[toIdx] = temp;
    }
}