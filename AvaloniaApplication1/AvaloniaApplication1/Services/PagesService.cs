using AvaloniaApplication1.ViewModels;

namespace AvaloniaApplication1.Services;

public static class PagesService {
    private static readonly ViewModelBase[] Pages = {
        new DecisionViewModel(),
        new CustomerViewModel(),
        new CourierViewModel()
    };
    public static ViewModelBase[] GetPages() => Pages;
}