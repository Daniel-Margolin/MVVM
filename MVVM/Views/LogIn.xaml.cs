using MVVM.ViewModels;

namespace MVVM.Views
{
    public partial class LogIn : ContentPage
    {
        #region Constructor

        public LogIn()
        {
            LogInViewModel vm = new LogInViewModel();
            InitializeComponent();
            BindingContext = vm;
        }

        #endregion
    }
}