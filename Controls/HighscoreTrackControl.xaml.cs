using System;
using System.Windows;
using System.Windows.Controls;

namespace ProjectCarsSeasonExtension.Controls
{
    /// <summary>
    /// Interaction logic for HighscoreTrackControl.xaml
    /// </summary>
    public partial class HighscoreTrackControl : UserControl
    {

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title", typeof(String),
                typeof(HighscoreTrackControl),
                new PropertyMetadata("Track Name")
                );

        public String Title
        {
            get { return (String)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public HighscoreTrackControl()
        {
            InitializeComponent();
        }
    }
}
