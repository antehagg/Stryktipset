using System.Windows;
using System.Windows.Controls;
using Stryktipset.Core.Data;
using Stryktipset.WPF.Validation.InputValidation;

namespace Stryktipset.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public Week InputWeek { get; set; }
        public string[] ResultArray { get; set; }
        public int[,] PermilleArray { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            ResultArray = new string[13];
            PermilleArray = new int[13, 3];
            // InputWeek = new Week(ResultArray, PermilleArray);
        }

        #region BuildGrids

        private void BuildPermilleGrid()
        {
            for (var i = 0; i >= 12; i++)
            {
                var stackPanel = CreatePermilleStackPanel(i);

                for (var j = 0; j >= 3; j++)
                {

                }
            }
        }

        private StackPanel CreatePermilleStackPanel(int gridRow)
        {
            var name = "Permille" + gridRow  + "StackPanel";
            var stackPanel = new StackPanel
            {
                Name = name,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Orientation = Orientation.Horizontal,
                Height = 23,
                Width = 173,
                Margin = new Thickness(0, 10, 0, 0)
            };

            stackPanel.SetValue(Grid.RowProperty, gridRow);

            return stackPanel;
        }
        #endregion
    }
}