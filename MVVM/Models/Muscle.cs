using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM.Models
{
    public class Muscle : INotifyPropertyChanged
    {
        #region Fields

        private string name = string.Empty;
        private string bodyPart = string.Empty;

        #endregion

        #region Properties

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BodyPart
        {
            get { return bodyPart; }
            set
            {
                if (bodyPart != value)
                {
                    bodyPart = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Methods

        protected void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}