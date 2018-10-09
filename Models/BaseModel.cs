﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using ProjectCarsSeasonExtension.Annotations;

namespace ProjectCarsSeasonExtension.Models
{
    public abstract class BaseModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
