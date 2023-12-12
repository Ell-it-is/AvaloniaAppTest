using System;
using AvaloniaApplication1.Services;
using Splat;

namespace AvaloniaApplication1.ViewModels;

public class DecisionViewModel : ViewModelBase {
    public void NavigateToCustomer(MainViewModel mainViewModel) {
        mainViewModel.CurrentPage = PagesService.GetPages()[1];
    }

    public void NavigateToCourier(MainViewModel mainViewModel) {
        mainViewModel.CurrentPage = PagesService.GetPages()[2];
    }
}