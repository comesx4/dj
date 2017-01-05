using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace ModulusFE.Demo
{
    /// <summary>
    /// TrendWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TrendWindow : MetroWindow
    {
        public TrendWindow()
        {
            InitializeComponent();

            UserControl item = new UCFirst();
            flipView.Items.Add(item);
            flipView.SelectedItem = item;
            //item.Loaded += OnSubViewLoad;
            //flipView.Children.Add(item);
            //bool bRet = item.Focus();

        }

        public void OnSubViewLoad(object sender, RoutedEventArgs e)
        {
            bool Ret = (sender as UserControl).Focus();
        }

        private void SysSetting_ModifyPass_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void SysSetting_LockScreen_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void SysSetting_ChangeTheme_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void flipView_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.Key.ToString());
            //UserControl item;
            //item.PreviewKeyDown
            //bool bRet = (flipView.Items[0] as UserControl).Focus();
            //e.Handled = true;
        }

        private void flipView_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {

        }
    }
}
