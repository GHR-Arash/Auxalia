using Autodesk.Revit.UI;
using AuxaliaRevitToolkit.Services;
using AuxaliaRevitToolkit.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace AuxaliaRevitToolkit.ViewModels
{

    public class LevelManagementViewModel : ObservableObject
    {
        private readonly RevitService _revitService;
        private readonly IUserInteractionService _userInteractionService;
        private readonly ILevelCreationDialogService _levelCreationDialogService;


        private LevelViewModel _selectedLevel;
        public LevelViewModel SelectedLevel
        {
            get => _selectedLevel;
            set
            {
                SetProperty(ref _selectedLevel, value);
                DeleteLevelCommand.NotifyCanExecuteChanged();
            }
        }

        private ObservableCollection<LevelViewModel> _levels;
        public ObservableCollection<LevelViewModel> Levels
        {
            get => _levels;
            set => SetProperty(ref _levels, value);
        }

        public RelayCommand CreateLevelCommand { get; private set; }
        public RelayCommand DeleteLevelCommand { get; private set; }

        // Constructor
        public LevelManagementViewModel(RevitService revitService, IUserInteractionService userInteractionService, ILevelCreationDialogService levelCreationDialogService)
        {
            _revitService = revitService;
            _userInteractionService = userInteractionService;
            _levelCreationDialogService = levelCreationDialogService;
            // Initialize commands and load levels
            CreateLevelCommand = new RelayCommand(ExecuteCreateLevel, CanExecuteCreateLevel);
            DeleteLevelCommand = new RelayCommand(ExecuteDeleteLevel, CanExecuteDeleteLevel);
            LoadLevels();
        }

        private void LoadLevels()
        {
            var levelModels = _revitService.GetLevels();
            _levels = new ObservableCollection<LevelViewModel>(
                levelModels.Select(l => new LevelViewModel()
                {
                    Name = l.Name,
                    Elevation = l.Elevation,
                    BasePoint = l.BasePoint
                }));
            OnPropertyChanged(nameof(Levels)); // Notify that the Levels property has changed


            Debug.WriteLine($"Initial SelectedLevel: {SelectedLevel}");

        }

        private void ExecuteCreateLevel()
        {
            var dialog = new NewLevelDialog();
            if (_levelCreationDialogService.ShowDialog(out string levelName, out double levelElevation))
            {
                _revitService.CreateLevel(levelName, levelElevation);
                LoadLevels(); // Refresh levels list
            }
        }
        private bool CanExecuteCreateLevel() => true;

        private void ExecuteDeleteLevel()
        {
            if (SelectedLevel == null) return;

            if (_userInteractionService.ConfirmDeletion(SelectedLevel.Name))
            {
                try
                {
                    _revitService.DeleteLevel(SelectedLevel.Name);
                    LoadLevels();
                }
                catch (Exception ex)
                {
                    _userInteractionService.ShowErrorMessage($"Failed to delete level: {ex.Message}");
                }
            }
        }
        private bool CanExecuteDeleteLevel() => SelectedLevel != null;
    }
}
