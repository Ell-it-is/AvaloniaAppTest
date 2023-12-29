using System;
using System.Collections.ObjectModel;
using AvaloniaApplication1.Models;

namespace AvaloniaApplication1.ViewModels.Customer;

public class RestaurantDetailViewModel : ViewModelBase {
    public Restaurant Restaurant { get; set; }

    public RestaurantDetailViewModel(Restaurant restaurant) {
        Restaurant = restaurant;
    }
}