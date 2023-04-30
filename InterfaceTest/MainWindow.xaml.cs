using System.Windows;
using System.Windows.Controls;

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
            TabItem tabItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TabItem;
            ContentControl contentControl = ((tabItem.Parent as TabControl).Parent as DockPanel).Parent as ContentControl;
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
            newControl.Content = FindResource("DockPanel");

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
            TabItem tabItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TabItem;
            ContentControl contentControl = ((tabItem.Parent as TabControl).Parent as DockPanel).Parent as ContentControl;
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
            newControl.Content = FindResource("DockPanel");

            Grid.SetRow(contentControl, 0);
            Grid.SetColumn(contentControl, 0);
            Grid.SetColumn(splitter, 1);
            Grid.SetColumn(newControl, 2);

            newGrid.Children.Add(contentControl);
            newGrid.Children.Add(splitter);
            newGrid.Children.Add(newControl);

            grid.Children.Add(newGrid);
        }
        private void AddTab_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TabItem;
            if (tabItem.Parent is TabControl tabControl)
            {
                TabItem newTab = new TabItem()
                {
                    ContextMenu = FindResource("TabMenu") as ContextMenu,
                    Header = "New Tab"
                };
                tabControl.Items.Add(newTab);
            }
        }
        private void RemoveTab_Click(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = ((sender as MenuItem).Parent as ContextMenu).PlacementTarget as TabItem;
            if (!(tabItem.Parent is TabControl tabControl)) return;

            if (tabControl.Items.Count > 1)
            {
                tabControl.Items.Remove(tabItem);
                return;
            }

            ContentControl contentControl = (tabControl.Parent as DockPanel).Parent as ContentControl;
            Grid grid = contentControl.Parent as Grid;
            if (grid.RowDefinitions.Count > 1)
            {
                if ((int)contentControl.GetValue(Grid.RowProperty) == 0)
                    MergeSplit(grid, Grid.RowProperty, 2);
                else
                    MergeSplit(grid, Grid.RowProperty, 0);
            }
            else if (grid.ColumnDefinitions.Count > 1)
            {
                if ((int)contentControl.GetValue(Grid.ColumnProperty) == 0)
                    MergeSplit(grid, Grid.ColumnProperty, 2);
                else
                    MergeSplit(grid, Grid.ColumnProperty, 0);
            }
        }
        private void MergeSplit(Grid grid, DependencyProperty gridDirectionProperty, int propertyKeepValue)
        {
            UIElement kept = null;
            foreach (UIElement child in grid.Children)
            {
                if ((int)child.GetValue(gridDirectionProperty) == propertyKeepValue)
                    kept = child;
            }
            if (kept == null)
                return;
            Grid parentGrid = grid.Parent as Grid;
            Grid.SetColumn(kept, (int)grid.GetValue(Grid.ColumnProperty));
            Grid.SetRow(kept, (int)grid.GetValue(Grid.RowProperty));
            grid.Children.Remove(kept);
            parentGrid.Children.Remove(grid);
            parentGrid.Children.Add(kept);
        }
    }
}
