using System;

namespace AvaloniaApplication1.Models;

// When courier accepts a preference => order should be created
public class Order {
    public Courier Courier { get; set; }
    public Customer Customer { get; set; }
    public string OrderDetails { get; set; } // don't know the type yet
    public double Cost { get; set; }
    public TimeSpan TimeLeftToDeliver { get; set; }
    public OrderState State { get; set; }
}