using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Stryktipset.Core.Data;
using Stryktipset.Core.Storage.JSON;
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

        public static int[] PermilleArray1 { get; set; }
        public static int[] PermilleArrayX { get; set; }
        public static int[] PermilleArray2 { get; set; }

        public static int[] RankArray1 { get; set; }
        public static int[] RankArrayX { get; set; }
        public static int[] RankArray2 { get; set; }

        private readonly DataContext _dataContext;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            var loadData = new LoadDataJson();
            _dataContext = loadData.ReadData();

            ResultArray = new string[13];

            PermilleArray1 = new int[13];
            PermilleArrayX = new int[13];
            PermilleArray2 = new int[13];

            RankArray1 = new int[13];
            RankArrayX = new int[13];
            RankArray2 = new int[13];

            BuildPermilleGrid();
            BuildRankGrid();
        }

        private Week CreateWeek()
        {
            var result = new Result(ResultArray);
            var permilleArray = CreateMultiDimArray(PermilleArray1, PermilleArrayX, PermilleArray2, out var permilleErrorMessage);
            var permille = new Permille(permilleArray);
            var rankArray = CreateMultiDimArray(RankArray1, RankArrayX, RankArray2, out var rankErrorMessage);
            var rank = new Rank(rankArray);
            var turnout = GetTurnout(out var turnoutErrorMessage);
            var date = GetDateTime();

            var week = new Week(result, permille, rank, date, turnout);
            return week;
        }

        private DateTime? GetDateTime()
        {
            return WeekDateTimePicker.SelectedDate;
        }

        private int GetTurnout(out string turnoutErrorMessage)
        {
            var turnOutString = TurnoutTextBox.Text;

            try
            {
                var turnout = Convert.ToInt32(turnOutString);
                turnoutErrorMessage = "";
                return turnout;
            }
            catch (Exception e)
            {
                turnoutErrorMessage = e.Message;
                return 0;
            }
        }

        private int[,] CreateMultiDimArray(int[] firstArray, int[] secondArray, int[] thirdArray, out string errorMessage)
        {
            var mdArray = new int[13, 3];

            if (firstArray.Length == 13 && secondArray.Length == 13 && thirdArray.Length == 13)
            {
                for (var i = 0; i <= 12; i++)
                {
                    if (firstArray[i] != 0 && secondArray[i] != 0 && thirdArray[i] != 0)
                    {
                        mdArray[i, 0] = firstArray[i];
                        mdArray[i, 1] = secondArray[i];
                        mdArray[i, 2] = thirdArray[i];
                    }
                    else
                    {
                        errorMessage = "All fields are not valid.";
                        return mdArray;
                    }
                } 

                errorMessage = "";
                return mdArray;
            }

            errorMessage = "All fields are not valid.";
            return mdArray;
        }

        #region BuildPermilleGrid

        private void BuildPermilleGrid()
        {
            for (var i = 0; i <= 12; i++)
            {
                var stackPanel = CreatePermilleStackPanel(i);
                PermilleGrid.Children.Add(stackPanel);
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

            PopulatePermilleStackPanel(stackPanel, gridRow);

            return stackPanel;
        }

        private static void PopulatePermilleStackPanel(Panel sp, int text)
        {
            var tb = new TextBlock
            {
                Width = 13,
                Text = (text + 1).ToString(),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };

            sp.Children.Add(tb);

            for (var i = 0; i <= 2; i++)
            {
                var path = "";

                var tbox = new TextBox
                {
                    Height = 23,
                    Width = 35,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10, 0, 0, 0),
                    Text = ""
                };

                switch (i)
                {
                    case 0:
                        path = "PermilleArray1[" + text + "]";
                        break;
                    case 1:
                        path = "PermilleArrayX[" + text + "]";
                        break;
                    case 2:
                        path = "PermilleArray2[" + text + "]";
                        break;
                }

                var b = new Binding
                {
                    Path = new PropertyPath(path),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    ValidatesOnNotifyDataErrors = true,
                    ValidatesOnDataErrors = true,
                    NotifyOnValidationError = true,
                    Mode = BindingMode.OneWayToSource
                };

                b.ValidationRules.Add(new PermilleValidationRule());

                BindingOperations.SetBinding(tbox, TextBox.TextProperty, b);
                sp.Children.Add(tbox);
            }
        }
        #endregion

        #region BuildRankGrid

        private void BuildRankGrid()
        {
            for (var i = 0; i <= 12; i++)
            {
                var stackPanel = CreateRankStackPanel(i);
                RankGrid.Children.Add(stackPanel);
            }
        }

        private StackPanel CreateRankStackPanel(int gridRow)
        {
            var name = "Rank" + gridRow + "StackPanel";
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

            PopulateRankStackPanel(stackPanel, gridRow);

            return stackPanel;
        }

        private static void PopulateRankStackPanel(Panel sp, int text)
        {
            var tb = new TextBlock
            {
                Width = 13,
                Text = (text + 1).ToString(),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(10, 0, 0, 0)
            };

            sp.Children.Add(tb);

            for (var i = 0; i <= 2; i++)
            {
                var path = "";

                var tbox = new TextBox
                {
                    Height = 23,
                    Width = 35,
                    TextWrapping = TextWrapping.Wrap,
                    Margin = new Thickness(10, 0, 0, 0),
                    Text = ""
                };

                switch (i)
                {
                    case 0:
                        path = "RankArray1[" + text + "]";
                        break;
                    case 1:
                        path = "RankArrayX[" + text + "]";
                        break;
                    case 2:
                        path = "RankArray2[" + text + "]";
                        break;
                }

                var b = new Binding
                {
                    Path = new PropertyPath(path),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                    ValidatesOnNotifyDataErrors = true,
                    ValidatesOnDataErrors = true,
                    NotifyOnValidationError = true,
                    Mode = BindingMode.OneWayToSource
                };

                b.ValidationRules.Add(new RankValidationRule());

                BindingOperations.SetBinding(tbox, TextBox.TextProperty, b);
                sp.Children.Add(tbox);
            }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ResultErrorBlock.Text = "";
            InputWeek = CreateWeek();
            var validated = InputWeek.ValidateWeekAndSave(out var errorMessage);

            if (!validated)
                GeneralErrorTextBlock.Text = errorMessage;
            else
            {
                _dataContext.AddWeek(InputWeek);
                _dataContext.Save();
            }
        }
    }
}