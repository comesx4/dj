using System.Windows.Media;

namespace ModulusFE.Demo
{
    public static class StockChartHelper
    {

        private static readonly Brush ChartPanelBackground = new SolidColorBrush(Color.FromArgb(255, 47, 51, 59));

        private static readonly Brush InfoPanelBackground = new SolidColorBrush(Color.FromArgb(120, 64, 71, 81));
        private static readonly Brush ScrollerTrackButtonsBackground = new SolidColorBrush(Color.FromArgb(120, 0x3A, 0x3E, 0x45));
        private static readonly Brush TrendStroke = new SolidColorBrush(Color.FromRgb(0x7F, 0x81, 0x84));
        private static readonly Brush ThumbBackground = new SolidColorBrush(Color.FromArgb(80, 0xAC, 0xA6, 0xA6));

        public static void SetStyle(this StockChartX chart)
        {
            chart.CrossHairsPattern = new DoubleCollection(new double[] { 3, 3 });
            chart.InfoPanelItemsOrientation = Controls.InfoPanelItemsOrientation.Vertical;
            chart.CandleUpHollow = true;
            chart.CandleUpOutlineColor = Colors.Red;
            // 十字光标时间格式
            //chart.CrossHairsTextTimeFormat = "yyyy-MM-dd";
            chart.CrossHairsTextTimeFormat = "hh:mm:ss";

            chart.HorizontalGridLinePattern = chart.VerticalGridLinePattern = new DoubleCollection(new double[] { 3, 3 });

            chart.CalenderXAxisDateTimeFormat = "yyyy/MM";

            /* 背景颜色 */
            chart.Background = (Brush)System.Windows.Application.Current.Resources["AccentColorBrush"];
            /* 字体颜色 */
            chart.FontForeground = Brushes.White;
            /* 各种颜色 */
            chart.HeatPanelLabelsBackground = (Brush)System.Windows.Application.Current.Resources["AccentColorBrush"];
            chart.CalendarBackground = (Brush)System.Windows.Application.Current.Resources["AccentColorBrush"];
            chart.IndicatorDialogLabelForeground = (Brush)System.Windows.Application.Current.Resources["AccentColorBrush"];
            chart.CalendarBackground = ChartPanelBackground;

            chart.ChartScrollerProperties.Background = ChartPanelBackground;
            chart.ChartScrollerProperties.TrendBackground = ChartPanelBackground;
            chart.ChartScrollerProperties.TrendStroke = TrendStroke;
            chart.ChartScrollerProperties.ThumbBackground = ThumbBackground;

            /* Y坐标设置在左侧 */
            chart.ScaleAlignment = ScaleAlignmentTypeEnum.Left;

            /* 消息面板固定位置 */
            //chart.InfoPanelPosition = InfoPanelPositionEnum.FixedPosition;
            /* 消息面板样式 */
            chart.InfoPanelLabelsBackground = InfoPanelBackground;
            chart.InfoPanelValuesBackground = InfoPanelBackground;
            chart.InfoPanelLabelsForeground = Brushes.WhiteSmoke;
            chart.InfoPanelValuesForeground = Brushes.WhiteSmoke;

            /* 涨跌颜色 */
            chart.DownColor = Color.FromRgb(0x29,0xb8,0x1e);
            chart.UpColor = Color.FromRgb(0xf5, 0x1d, 0x27);

            /* 左右保留距离 */
            chart.LeftChartSpace = 0;
            chart.RightChartSpace = 0;

            /* 不显示X轴Grid */
            chart.XGrid = false;

            /* 显示Y轴Grid */
            chart.YGrid = true;

            /* Grid颜色 */
            chart.GridStroke = new SolidColorBrush(Color.FromRgb(0x27, 0x2C, 0x32));

            /* Grid分布类型 */
            chart.YGridStepType = YGridStepType.NiceStep;

            /* 最大可显示记录 */
            chart.MaxVisibleRecords = 100;

            /* X轴时间 */
            chart.RealTimeXLabels = true;

            /* 必须设置 */
            chart.OptimizePainting = true;

            /* 十字线 */
            chart.CrossHairs = true;

            /* 十字线颜色 */
            chart.CrossHairsStroke = new SolidColorBrush(Colors.White);

            /* 成交量颜色随OHLC变化 */
            chart.UseVolumeUpDownColors = true;

            /* 不显示矩形拉框放大 */
            chart.DisableZoomArea = true;
        }

        public static void SetStyle(this ChartPanel panel)
        {
            /* 不显示最大化,最小化,关闭 */
            panel.CloseBox = false;
            panel.MaximizeBox = false;
            panel.MinimizeBox = false;

            /* 颜色 */
            panel.Background = ChartPanelBackground;
            panel.TitleBarBackground = new SolidColorBrush(Color.FromRgb(0x24, 0x28, 0x2E));
            panel.YAxesBackground = ChartPanelBackground;
        }
    }
}
