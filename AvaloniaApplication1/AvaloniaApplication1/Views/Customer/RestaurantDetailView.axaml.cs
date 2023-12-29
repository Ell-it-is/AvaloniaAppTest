using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Services;
using AvaloniaApplication1.ViewModels.Customer;

namespace AvaloniaApplication1.Views.Customer;

public partial class RestaurantDetailView : UserControl {
    private RestaurantDetailViewModel _restaurantDetailViewModel;
    
    public RestaurantDetailView() {
        InitializeComponent();
    }

    private void SavePreference_OnClick(object? sender, RoutedEventArgs e) {
        _restaurantDetailViewModel = (RestaurantDetailViewModel) DataContext;
        
        var restaurant = _restaurantDetailViewModel.Restaurant.Clone() as Restaurant;
        _restaurantDetailViewModel.Restaurant.Name = RestaurantNamesComboBox.SelectedItem?.ToString() ?? restaurant.Name;
        _restaurantDetailViewModel.Restaurant.SelectedFood = FoodNamesComboBox.SelectedItem?.ToString() ?? restaurant.SelectedFood;
        _restaurantDetailViewModel.Restaurant.SelectedDrink = DrinkNamesComboBox.SelectedItem?.ToString() ?? restaurant.SelectedDrink;
        _restaurantDetailViewModel.Restaurant.CheckAndSetRestaurantEdited(restaurant);
        App._MainViewModel.OnBackRequested(sender, e);
    }

    private void Panel_OnInitialized(object? sender, EventArgs e) {
        _restaurantDetailViewModel = (RestaurantDetailViewModel) DataContext;
        
        RestaurantNamesComboBox.ItemsSource = RestaurantsService.RestaurantNames;
        FoodNamesComboBox.ItemsSource = RestaurantsService.FoodNames;
        DrinkNamesComboBox.ItemsSource = RestaurantsService.DrinkNames;
        
        RestaurantNamesComboBox.PlaceholderText = _restaurantDetailViewModel.Restaurant.Name;
        FoodNamesComboBox.PlaceholderText = _restaurantDetailViewModel.Restaurant.SelectedFood;
        DrinkNamesComboBox.PlaceholderText = _restaurantDetailViewModel.Restaurant.SelectedDrink;
    }
}