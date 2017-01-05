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

namespace ModulusFE.Demo
{
    /// <summary>
    /// UCFirst.xaml 的交互逻辑
    /// </summary>
    public partial class UCFirst : UserControl
    {
        public UCFirst()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if((sender as ListBox).SelectedIndex == 0)
            {
                trend.Visibility = Visibility.Visible;
                kline.Visibility = Visibility.Collapsed;
            }
            else
            {
                trend.Visibility = Visibility.Collapsed;
                kline.Visibility = Visibility.Visible;
            }
            //MessageBox.Show(sender.ToString() + " e: " + e.ToString());
        }
    }
}
