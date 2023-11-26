using AuxaliaRevitToolkit.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AuxaliaRevitToolkit.ViewModels
{
    public partial class LevelViewModel : ObservableObject
    {
        private string _name;
        private double _elevation;
        private LevelModel.BasePointEnum _basePoint;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public double Elevation
        {
            get => _elevation;
            set => SetProperty(ref _elevation, value);
        }

        public LevelModel.BasePointEnum BasePoint
        {
            get => _basePoint;
            set => SetProperty(ref _basePoint, value);
        }



    }
}

