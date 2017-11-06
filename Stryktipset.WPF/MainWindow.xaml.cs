using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Stryktipset.Core.Data;
using Stryktipset.Core.Storage.JSON;

namespace Stryktipset.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly log4net.ILog Log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private DataContext _dataContext;
        private LoadDataJson _loadJson;
        private StoreDataAsJson _storeJson;

        public MainWindow()
        {
            InitializeComponent();
            _loadJson = new LoadDataJson();
            _storeJson = new StoreDataAsJson();
            _dataContext = _loadJson.ReadData();
        }

        private void ValidateInput(object sender)
        {
            var tb = (TextBox) sender;

            if (tb.Text.ToLower().Equals("x") || tb.Text.ToLower().Equals("1") || tb.Text.ToLower().Equals("2"))
            {
                tb.Background = Brushes.LawnGreen;
            }
            else
                tb.Background = Brushes.Red;
        }

        private void ValidateResultInput(object sender, TextChangedEventArgs e)
        {
            ValidateInput(sender);
        }

        private void ValidateResultInput(object sender, TextCompositionEventArgs e)
        {
            ValidateInput(sender);
        }

        private void CalculatePercent(object sender)
        {
            var textBoxList = new List<TextBox>();
            var senderTb = (TextBox) sender;

            var senderTbName = senderTb.Name.Replace("Percent", "");

            CreateTextBoxList(textBoxList, senderTbName);

            if (textBoxList.Count != 3) return;

            var totalValue = 0;
            var totalInputs = 0;
            foreach (var tb in textBoxList)
            {
                if (!ValidatePercent(tb.Text)) continue;

                totalInputs++;
                totalValue += Convert.ToInt32(tb.Text);
            }

            if (totalInputs == 2 && Equals(senderTb, textBoxList[2]))
            {
                var addedValue = (100 - totalValue);
                totalValue += addedValue;
                ++totalInputs;

                textBoxList[2].Text = addedValue.ToString();
            }
            if (totalInputs == 3)
                ValidateFullPercent(totalValue, textBoxList);

            CalculateRank();
        }

        private void CalculateRank()
        {
            var permilleList = new List<int>();

            foreach (var o in PercentGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox) o;

                if (string.IsNullOrEmpty(tb.Text))
                    break;

                int value;
                try
                {
                    value = Convert.ToInt32(tb.Text);
                }
                catch (Exception)
                {
                    break;
                }

                permilleList.Add(value);
            }

            if (permilleList.Count != 39)
                    return;

            var resultList = new List<string>();

            foreach (var o in ResultGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox)o;

                if (string.IsNullOrEmpty(tb.Text))
                    break;

                resultList.Add(tb.Text);
            }

            if (resultList.Count != 13)
                return;

            var counter = 1;

            var totalPermille = 0;
            foreach (var r in resultList)
            {
                if (r == "1")
                {
                    totalPermille += permilleList[counter - 1];
                }
                else if (r == "x")
                {
                    totalPermille += permilleList[counter];
                }
                else if (r == "2")
                {
                    totalPermille += permilleList[counter + 1];
                }

                counter += 3;
            }

            PercentTextBlox.Text = (totalPermille * 10).ToString();
        }

        private void ValidateFullPercent(int value, IEnumerable<TextBox> tbList)
        {
            var isFull = value == 100;

            foreach (var tb in tbList)
            {
                tb.Background = isFull ? Brushes.LawnGreen : Brushes.Red;
            }

        }

        private void CreateTextBoxList(List<TextBox> textBoxList, string senderTbName)
        {
            if (senderTbName.StartsWith("One"))
            {
                textBoxList.Add(PercentOne_One);
                textBoxList.Add(PercentOne_X);
                textBoxList.Add(PercentOne_Two);
            }
            else if (senderTbName.StartsWith("Two"))
            {
                textBoxList.Add(PercentTwo_One);
                textBoxList.Add(PercentTwo_X);
                textBoxList.Add(PercentTwo_Two);
            }
            else if (senderTbName.StartsWith("Three"))
            {
                textBoxList.Add(PercentThree_One);
                textBoxList.Add(PercentThree_X);
                textBoxList.Add(PercentThree_Two);
            }
            else if (senderTbName.StartsWith("Four"))
            {
                textBoxList.Add(PercentFour_One);
                textBoxList.Add(PercentFour_X);
                textBoxList.Add(PercentFour_Two);
            }
            else if (senderTbName.StartsWith("Five"))
            {
                textBoxList.Add(PercentFive_One);
                textBoxList.Add(PercentFive_X);
                textBoxList.Add(PercentFive_Two);
            }
            else if (senderTbName.StartsWith("Six"))
            {
                textBoxList.Add(PercentSix_One);
                textBoxList.Add(PercentSix_X);
                textBoxList.Add(PercentSix_Two);
            }
            else if (senderTbName.StartsWith("Seven"))
            {
                textBoxList.Add(PercentSeven_One);
                textBoxList.Add(PercentSeven_X);
                textBoxList.Add(PercentSeven_Two);
            }
            else if (senderTbName.StartsWith("Eight"))
            {
                textBoxList.Add(PercentEight_One);
                textBoxList.Add(PercentEight_X);
                textBoxList.Add(PercentEight_Two);
            }
            else if (senderTbName.StartsWith("Nine"))
            {
                textBoxList.Add(PercentNine_One);
                textBoxList.Add(PercentNine_X);
                textBoxList.Add(PercentNine_Two);
            }
            else if (senderTbName.StartsWith("Ten"))
            {
                textBoxList.Add(PercentTen_One);
                textBoxList.Add(PercentTen_X);
                textBoxList.Add(PercentTen_Two);
            }
            else if (senderTbName.StartsWith("Eleven"))
            {
                textBoxList.Add(PercentEleven_One);
                textBoxList.Add(PercentEleven_X);
                textBoxList.Add(PercentEleven_Two);
            }
            else if (senderTbName.StartsWith("Twelve"))
            {
                textBoxList.Add(PercentTwelve_One);
                textBoxList.Add(PercentTwelve_X);
                textBoxList.Add(PercentTwelve_Two);
            }
            else if (senderTbName.StartsWith("Thirteen"))
            {
                textBoxList.Add(PercentThirteen_One);
                textBoxList.Add(PercentThirteen_X);
                textBoxList.Add(PercentThirteen_Two);
            }
        }

        private bool ValidatePercent(string senderText)
        {
            var isInt = int.TryParse(senderText, out _);

            return isInt;
        }

        private void CalculateAndValidatePercent(object sender, RoutedEventArgs e)
        {
            CalculatePercent(sender);
        }

        private void CalculateAndValidatePercent(object sender, TextChangedEventArgs e)
        {
            CalculatePercent(sender);
        }

        private void ValidateRank(object sender, TextChangedEventArgs e)
        {
            var validateArray = new bool[39];
            var validated = true;

            foreach (var o in RankGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox) o;
                if (string.IsNullOrEmpty(tb.Text))
                {
                    validated = false;
                    break;
                }

                try
                {
                    var value = Convert.ToInt32(tb.Text) - 1;

                    if (validateArray[value])
                    {
                        validated = false;
                        break;
                    }

                    validateArray[value] = true;
                }
                catch (Exception)
                {
                    validated = false;
                    break;
                }
            }

            PaintRankGrid(validated);
        }

        public DataContext TestDatacontext()
        {
            var results = new List<string>();
            for (var i = 1; i <= 13; i++)
            {
                results.Add("1");
            }

            var result = new Result(results);

            var permilleList = new List<int>();

            for (var i = 1; i <= 39; i++)
            {
                permilleList.Add(i % 3 == 0 ? 98 : 1);
            }

            var permille = new Permille(permilleList);

            var rankList = new List<int>();

            for (var i = 1; i <= 39; i++)
            {
                rankList.Add(i);
            }

            var rank = new Rank(rankList);

            var week = new Week(result, permille, rank, DateTime.Now, 123, 123);
            var weeks = new List<Week>() {week};
            var dc = new DataContext(weeks);

            return dc;
        }

        private void PaintRankGrid(bool validated)
        {
            if (!validated)
                return;

            var color = Brushes.LawnGreen;

            foreach (var o in RankGrid.Children)
            {
                if (o.GetType() == typeof(TextBox))
                {
                    var tb = (TextBox) o;
                    tb.Background = color;
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var result = GetResults();
            var permille = GetPermille();
            var rank = GetRank();
            var date = GetDate();
            var payout = GetPayout();
            var totalPermille = GetTotalPermille();

            ValidateAndSave(result, permille, rank, date, payout, totalPermille);
        }

        private void ValidateAndSave(Result result, Permille permille, Rank rank, DateTime? date, int? payout, int? totalPermille)
        {
            if (result == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel i resultatlistan!";
            }
            else if (permille == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel i procentlistan!";
            }
            else if(rank == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel i ranklistan!";
            }
            else if(date == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel datum!";
            }
            else if(payout == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel utdelning!";
            }
            else if (totalPermille == null)
            {
                ErrorTextBlock.Foreground = Brushes.Red;
                ErrorTextBlock.Text = "Fel total promille!";
            }
            else
            {
                var week = new Week(result, permille, rank, date, payout, totalPermille);
                _dataContext.AddWeek(week);
                _storeJson.SaveData(_dataContext);
                ErrorTextBlock.Foreground = Brushes.DarkGreen;
                ErrorTextBlock.Text = "Veckan inmatad!";
            }
        }
        private int? GetTotalPermille()
        {
            var percentString = PercentTextBlox.Text;

            if (!string.IsNullOrEmpty(percentString))
            {
                try
                {
                    var percent = Convert.ToInt32(percentString);
                    return percent;
                }
                catch (FormatException)
                {
                    return null;
                }
            }

            return null;
        }

        private int? GetPayout()
        {
            var payoutString = PayoutTextBox.Text;

            if (!string.IsNullOrEmpty(payoutString))
            {
                try
                {
                    var payout = Convert.ToInt32(payoutString);
                    return payout;
                }
                catch (FormatException)
                {
                    return null;
                }
            }

            return null;
        }

        private DateTime? GetDate()
        {
            var date = WeekDatePicker.SelectedDate;

            return date;
        }

        private Rank GetRank()
        {
            var rankList = new List<int>();

            foreach (var o in RankGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox)o;
                int value;

                try
                {
                    value = Convert.ToInt32(tb.Text);
                }
                catch (FormatException)
                {
                    continue;
                }


                if (!string.IsNullOrEmpty(tb.Text) && value > 0 && value <= 39 && !rankList.Contains(value))
                {
                    rankList.Add(value);
                }
            }

            return rankList.Count == 39 ? new Rank(rankList) : null;
        }

        private Permille GetPermille()
        {
            var permilleList = new List<int>();

            foreach (var o in PercentGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox)o;
                int value;

                try
                {
                    value = Convert.ToInt32(tb.Text);
                }
                catch (FormatException)
                {
                    continue;
                }
               

                if (!string.IsNullOrEmpty(tb.Text) && value > 0 && value <=100)
                {
                    permilleList.Add(value*10);
                }
            }

            var percentValue = 0;
            for (var i = 0; i < permilleList.Count; i++)
            {
                percentValue += permilleList[i];

                if ((i+1) % 3 != 0)
                    continue;

                if (percentValue != 1000)
                {
                    return null;
                }

                percentValue = 0;
            }

            return permilleList.Count == 39 ? new Permille(permilleList) : null;
        }

        private Result GetResults()
        {
            var resultList = new List<string>();

            foreach (var o in ResultGrid.Children)
            {
                if (o.GetType() != typeof(TextBox))
                    continue;

                var tb = (TextBox) o;
                if (!string.IsNullOrEmpty(tb.Text) &&
                    (tb.Text.ToLower().Equals("x") || tb.Text.ToLower().Equals("1") ||
                     tb.Text.ToLower().Equals("2")))
                {
                    resultList.Add(tb.Text);
                }
            }

            return resultList.Count == 13 ? new Result(resultList) : null;
        }
    }

}