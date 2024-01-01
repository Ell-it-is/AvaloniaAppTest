using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Xaml.Interactions.Core;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.ViewModels;
using AvaloniaApplication1.ViewModels.Customer;

namespace AvaloniaApplication1.Views.Customer;

public partial class RestaurantDetailView : UserControl {
    private RestaurantDetailViewModel _restaurantDetailViewModel;
    
    public RestaurantDetailView() {
        InitializeComponent();
    }

    private void SavePreference_OnClick(object? sender, RoutedEventArgs e) {
        _restaurantDetailViewModel = (RestaurantDetailViewModel) DataContext;
        
        _restaurantDetailViewModel.Restaurant.Name = RestaurantNamesComboBox.SelectedItem?.ToString() ?? DefaultRestaurant.Name;
        _restaurantDetailViewModel.Restaurant.SelectedFood = FoodNamesComboBox.SelectedItem?.ToString() ?? DefaultRestaurant.SelectedFood;
        _restaurantDetailViewModel.Restaurant.SelectedDrink = DrinkNamesComboBox.SelectedItem?.ToString() ?? DefaultRestaurant.SelectedDrink;
        App._MainViewModel.OnBackRequested(sender, e);
    }

    private void StyledElement_OnInitialized(object? sender, EventArgs e) {
        _restaurantDetailViewModel = (RestaurantDetailViewModel) DataContext;
        
        RestaurantNamesComboBox.PlaceholderText = DefaultRestaurant.Name;
        FoodNamesComboBox.PlaceholderText = DefaultRestaurant.SelectedFood;
        DrinkNamesComboBox.PlaceholderText = DefaultRestaurant.SelectedDrink;
        
        RestaurantNamesComboBox.ItemsSource = RestaurantsService.RestaurantNames;
        FoodNamesComboBox.ItemsSource = RestaurantsService.FoodNames;
        DrinkNamesComboBox.ItemsSource = RestaurantsService.DrinkNames;

        RestaurantNamesComboBox.SelectedItem = _restaurantDetailViewModel.Restaurant.Name;
        FoodNamesComboBox.SelectedItem = _restaurantDetailViewModel.Restaurant.SelectedFood;
        DrinkNamesComboBox.SelectedItem = _restaurantDetailViewModel.Restaurant.SelectedDrink;
    }
}