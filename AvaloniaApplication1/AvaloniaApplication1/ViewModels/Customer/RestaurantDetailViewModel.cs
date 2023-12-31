using System;
using System.Collections.ObjectModel;
using System.Linq;
using AvaloniaApplication1.Models;
using DynamicData;

namespace AvaloniaApplication1.ViewModels.Customer;

public class RestaurantDetailViewModel : ViewModelBase {
    public Restaurant Restaurant { get; set; }

    public RestaurantDetailViewModel(Restaurant restaurant) {
        Restaurant = restaurant;
    }
}