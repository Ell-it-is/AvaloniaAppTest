using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication1.Models;

public static class DefaultRestaurant {
    public static readonly string Name = "Nezname jmeno restaurace";
    public static readonly string SelectedFood = "Zadne";
    public static readonly string SelectedDrink = "Zadne";
}

public class Restaurant : ReactiveObject {
    [Reactive] public string Name { get; set; } = DefaultRestaurant.Name;
    [Reactive] public string SelectedFood { get; set; } = DefaultRestaurant.SelectedFood;
    [Reactive] public string SelectedDrink { get; set; } = DefaultRestaurant.SelectedDrink;
    [Reactive] public bool IsOpen { get; set; }
    public bool IsValid => Name != DefaultRestaurant.Name && (SelectedFood != DefaultRestaurant.SelectedFood || SelectedDrink != DefaultRestaurant.SelectedDrink);

    private bool _nameVisible => this.IsValid && Name != DefaultRestaurant.Name;
    public bool NameVisible => _nameVisible;
    
    private bool _foodVisible => this.IsValid && SelectedFood != DefaultRestaurant.SelectedFood;
    public bool FoodVisible => _foodVisible;

    private bool _drinkVisible => this.IsValid && SelectedDrink != DefaultRestaurant.SelectedDrink;
    public bool DrinkVisible => _drinkVisible;

    private bool _defaultTextVisible => !this.IsValid;
    public bool DefaultTextVisible => _defaultTextVisible;

    public Restaurant() {
        
    }
    
    public override string ToString() {
        return "Not initialized Restaurant";
    }
}