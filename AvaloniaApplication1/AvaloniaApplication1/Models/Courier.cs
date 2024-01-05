namespace AvaloniaApplication1.Models;

public class Courier : User {
    public bool Available { get; set; }
    public int DestinationLocation { get; set; } // data type for demo
    public CourierRating Rating { get; set; }
    
}