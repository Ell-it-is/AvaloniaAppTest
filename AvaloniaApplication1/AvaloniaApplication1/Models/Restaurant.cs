using System.Collections.ObjectModel;

namespace AvaloniaApplication1.Models;

public class Restaurant {
    public string Name { get; set; } = "Nezname jmeno restaurace";
    public string SelectedFood { get; set; } = "Nevybrano jidlo";
    public string SelectedDrink { get; set; } = "Nevybrano piti";
    public bool IsOpen { get; set; }
    public bool Edited { get; set; }

    public override string ToString() {
        return "Not initialized Restaurant";
    }
}