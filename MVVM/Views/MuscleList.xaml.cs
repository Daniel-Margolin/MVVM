using MVVM.ViewModels;

namespace MVVM.Views;

public partial class MuscleList : ContentPage
{
    #region Constructor

    public MuscleList()
    {
        MuscleListViewModel vm = new MuscleListViewModel();
        InitializeComponent();
        BindingContext = vm;
    }

    #endregion
}