using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AvaloniaApplication1.Models;

public class Restaurant : ICloneable, INotifyPropertyChanged {
    public string Name { get; set; } = "Nezname jmeno restaurace";
    public string SelectedFood { get; set; } = "Nevybrano jidlo";
    public string SelectedDrink { get; set; } = "Nevybrano piti";
    public bool IsOpen { get; set; }
    public bool Edited { get; set; } = false;

    // clone should be previous state of this object 
    public void CheckAndSetRestaurantEdited(Restaurant clone) {
        bool change = false;
        if (Name != clone.Name ||
            SelectedFood != clone.SelectedFood ||
            SelectedDrink != clone.SelectedDrink) {
            change = true;
        }
        if (!Edited && change) Edited = true;

        if (Name == default &&
            SelectedFood == default &&
            SelectedDrink == default) {
            Edited = false;
        }
    }
    
    public override string ToString() {
        return "Not initialized Restaurant";
    }

    public object Clone() => this.MemberwiseClone();
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null) {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}