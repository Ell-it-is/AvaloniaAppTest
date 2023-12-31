using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace AvaloniaApplication1.Models;

public static class DefaultRestaurant {
    public static readonly string Name = "Nezname jmeno restaurace";
    public static readonly string SelectedFood = "Nevybrano jidlo";
    public static readonly string SelectedDrink = "Nevybrano piti";
}
public class Restaurant : ReactiveObject {
    
    [Reactive]
    public string Name { get; set; } = DefaultRestaurant.Name;
    [Reactive]
    public string SelectedFood { get; set; } = DefaultRestaurant.SelectedFood;
    [Reactive]
    public string SelectedDrink { get; set; } = DefaultRestaurant.SelectedDrink;
    [Reactive]
    public bool IsOpen { get; set; }
    public bool IsValid => 
        Name != DefaultRestaurant.Name && (SelectedFood != DefaultRestaurant.SelectedFood || SelectedDrink != DefaultRestaurant.SelectedDrink); 
    
    public override string ToString() {
        return "Not initialized Restaurant";
    }
}