using System.Collections.ObjectModel;

namespace AvaloniaApplication1.Models;

public class Restaurant {
    public string Name { get; set; }
    public bool IsOpen { get; set; }
    public bool Edited { get; set; }

    public override string ToString() {
        if (!string.IsNullOrEmpty(Name)) return Name;
        return "Add new preference";
    }
}