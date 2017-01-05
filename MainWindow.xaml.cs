using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ModulusFE.Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {   
        public MainWindow()
        {
            InitializeComponent();
        }

        private void KlineChartButton_Click(object sender, RoutedEventArgs e)
        {
            //new KlineChartWindow().Show();
        }

        private void TrendChartButton_Click(object sender, RoutedEventArgs e)
        {
            //new TrendChartWindow().Show();
            new TrendWindow().Show();
        }
    }
}
