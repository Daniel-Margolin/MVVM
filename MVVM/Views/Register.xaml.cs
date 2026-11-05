using MVVM.ViewModels;

namespace MVVM.Views;

public partial class Register : ContentPage
{
    #region Constructor
    public Register()
    {
        RegisterViewModel vm = new RegisterViewModel();
        InitializeComponent();
        BindingContext = vm;
    }

    #endregion
}