using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using BruTile.Predefined;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.Tiling.Layers;
using Mapsui.UI.Avalonia;
using Svg.FilterEffects;
using Point = NetTopologySuite.Geometries.Point;

namespace AvaloniaApplication1.Views.Customer;

public partial class MapView : UserControl {
    
    public MapView() {
        InitializeComponent();

        /*var map = new Map();
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        _MapControl.Map = map;*/
    }
}