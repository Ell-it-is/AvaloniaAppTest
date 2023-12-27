using System.Collections.ObjectModel;

namespace AvaloniaApplication1.ViewModels.Customer;

public class PrefListViewModel : ViewModelBase {
    public ObservableCollection<string> Restaurants { get; private set; }

    public PrefListViewModel() {
        Restaurants = new ObservableCollection<string>() {
            "Restaurant 1",
            "Restaurant 2", 
            "Restaurant 3",
            "Restaurant 4"
        };
    }
        
    /*
     * After tapping on restaurant twice (double tap)
     * -> open screen where you can edit the details
     */
    public void EditRestaurant() {
        
    }
    
    /*
     * Tap once on a restaurant 'r1'
     * Then tap again on a restaurant 'r2' and swap its positions
     */
    public void SwapRestaurants(int fromIdx, int toIdx) {
        string temp = Restaurants[fromIdx];
        Restaurants[fromIdx] = Restaurants[toIdx];
        Restaurants[toIdx] = temp;
    }
}