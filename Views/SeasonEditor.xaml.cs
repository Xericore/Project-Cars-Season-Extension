using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using ProjectCarsSeasonExtension.Annotations;
using ProjectCarsSeasonExtension.Models;

namespace ProjectCarsSeasonExtension.Views
{
    /// <summary>
    /// Interaction logic for SeasonEditor.xaml
    /// </summary>
    public partial class SeasonEditor : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Challenge> Challenges => _dataView.CurrentSeason.Challenges;
        public Challenge SelectedChallenge { get; set; }

        public ObservableCollection<Season> Seasons => _dataView.Seasons;
        public Season SelectedSeason { get; set; }

        private readonly DataView _dataView;
        
        public SeasonEditor(DataView dataView)
        {
            _dataView = dataView;

            InitializeComponent();

            ChallengesComboBox.SelectedIndex = 0;
            SeasonComboBox.SelectedIndex = 0;
        }

        private void SeasonSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedSeason = e.AddedItems[0] as Season;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            OnPropertyChanged(nameof(SelectedSeason));
        }

        private void ChallengeSelector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedChallenge = e.AddedItems[0] as Challenge;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            
            OnPropertyChanged(nameof(SelectedChallenge));
        }


        private void SeasonChallenges_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var listBox = sender as ListBox;
                var selectedItems = listBox?.SelectedItems;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
