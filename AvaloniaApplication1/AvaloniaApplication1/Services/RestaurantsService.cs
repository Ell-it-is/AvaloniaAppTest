using System.Collections.Generic;

namespace AvaloniaApplication1.Services;

public static class RestaurantsService {
    public static readonly List<string> RestaurantNames = new List<string>() {
        "Restaurace Kobyla",
        "Restaurace u Socku",
        "Restaurace Na Pesinach",
        "Mame dobry jidlo",
        "Dogshit, tady jist nebudu"
    };
    
    public static readonly List<string> FoodNames = new List<string>() {
        "Zadne",
        "Bramborak",
        "Pizza",
        "Svickova",
        "Hovno",
        "Kytky"
    };
    
    public static readonly List<string> DrinkNames = new List<string>() {
        "Zadne",
        "Cola",
        "Kofola",
        "Malinovka",
        "Pivo",
        "Hardcore"
    };
}