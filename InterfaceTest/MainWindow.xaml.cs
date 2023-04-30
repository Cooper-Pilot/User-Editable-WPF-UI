using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace InterfaceTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SplitHorizontally_Click(object sender, RoutedEventArgs e)
        {
            ContentControl contentControl = (((sender as MenuItem).Parent as ContextMenu).PlacementTarget as DockPanel).Parent as ContentControl;
            if (contentControl is null) return;
            Grid grid = contentControl.Parent as Grid;
            grid.Children.Remove(contentControl);

            Grid newGrid = new Grid();
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(10) });
            newGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
            Grid.SetRow(newGrid, (int)contentControl.GetValue(Grid.RowProperty));
            Grid.SetColumn(newGrid, (int)contentControl.GetValue(Grid.ColumnProperty));

            GridSplitter splitter = new GridSplitter();
            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
            splitter.VerticalAlignment = VerticalAlignment.Stretch;

            ContentControl newControl = new ContentControl();
            newControl.Content = FindResource("Border");

            Grid.SetColumn(contentControl, 0);
            Grid.SetRow(contentControl, 0);
            Grid.SetRow(splitter, 1);
            Grid.SetRow(newControl, 2);

            newGrid.Children.Add(contentControl);
            newGrid.Children.Add(splitter);
            newGrid.Children.Add(newControl);

            grid.Children.Add(newGrid);
        }

        private void SplitVertically_Click(object sender, RoutedEventArgs e)
        {
            ContentControl contentControl = (((sender as MenuItem).Parent as ContextMenu).PlacementTarget as DockPanel).Parent as ContentControl;
            if (contentControl is null) return;
            Grid grid = contentControl.Parent as Grid;
            grid.Children.Remove(contentControl);

            Grid newGrid = new Grid();
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10) });
            newGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            Grid.SetRow(newGrid, (int)contentControl.GetValue(Grid.RowProperty));
            Grid.SetColumn(newGrid, (int)contentControl.GetValue(Grid.ColumnProperty));

            GridSplitter splitter = new GridSplitter();
            splitter.HorizontalAlignment = HorizontalAlignment.Stretch;
            splitter.VerticalAlignment = VerticalAlignment.Stretch;

            ContentControl newControl = new ContentControl();
            newControl.Content = FindResource("Border");

            Grid.SetRow(contentControl, 0);
            Grid.SetColumn(contentControl, 0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(newControl, 2);

            newGrid.Children.Add(contentControl);
            newGrid.Children.Add(splitter);
            newGrid.Children.Add(newControl);

            grid.Children.Add(newGrid);
        }
    }
}
