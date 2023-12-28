using System;
using AvaloniaApplication1.ViewModels.Customer;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels;

public class CustomerViewModel : ViewModelBase {
    private ViewModelBase[] SubPages = {
        new MapViewModel(),
        new CouriersListViewModel(),
        new PrefListViewModel(),
        new OrderHistoryViewModel()
    };
    public ViewModelBase[] GetSubPages => SubPages;
    
    public ViewModelBase _Content;
    public ViewModelBase Content {
        get => _Content;
        set => this.RaiseAndSetIfChanged(ref _Content, value);
    }

    public CustomerViewModel() {
        // Inital page
        Content = SubPages[2];
    }
    
    public void RenderMapView() => Content = SubPages[0];
    public void RenderCouriersListView() => Content = SubPages[1];
    public void RenderPrefListView() => Content = SubPages[2];
    public void RenderOrderHistoryView() => Content = SubPages[3];
}