namespace AvaloniaApplication1.Models;

public class User {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public int CurrentLocation { get; set; } // data type for demo
    public bool CanBeCourier { get; set; }
    public bool IsCourier { get; set; }
}