using System;
using System.Globalization;
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
    public partial class TrendChartWindow : UserControl
    {
        private const string LastSeriesName = "最新价";
        private const string WavgSeriesName = "加权平均价";

        private ChartPanel _trendTopPanel;
        private ChartPanel _trendVolumePanel;

        private Standard _lastSeries;
        private Standard _wavgSeries;

        /// <summary>
        /// 定时器
        /// </summary>
        private System.Timers.Timer MyTimer;

        private Series _trendVolumeSeries;

        public TrendChartWindow()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            //InitTrendChart();
        }

        private void InitTrendChart()
        {

            /*

            DateTime dt;

            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo();
            dtFormat.ShortDatePattern = "hh:mm:ss";

            dt = Convert.ToDateTime("10:10:10", dtFormat);
            MessageBox.Show(dt.ToString());
            */

            TrendChart.InfoPanelUpdated += TrendChart_InfoPanelUpdated;
            TrendChart.ClearAll();
            TrendChart.Freeze();
            TrendChart.Symbol = "分时";
            _trendTopPanel = TrendChart.AddChartPanel();

            _trendVolumePanel = TrendChart.AddChartPanel(ChartPanel.PositionType.AlwaysBottom);

            TrendChart.SetStyle();
            _trendTopPanel.SetStyle();
            _trendVolumePanel.SetStyle();

            /* 成交量 */
            _trendVolumeSeries = TrendChart.AddVolumeSeries(TrendChart.Symbol, _trendVolumePanel.Index, StandSeriesBarType.Line);
            _trendVolumeSeries.StrokeThickness = 1;

            /* 最新价 加权平均价 */
            _lastSeries = (Standard)TrendChart.AddSeries(LastSeriesName, _trendTopPanel.Index);
            _wavgSeries = (Standard)TrendChart.AddSeries(WavgSeriesName, _trendTopPanel.Index);

            _trendTopPanel.YAxisUseUpDownColors = true;
            _trendTopPanel.YAxisColorsBaseOnSeries = _wavgSeries;

            _lastSeries.StrokeColor = Color.FromRgb(0x42, 0x9a, 0xee);
            _lastSeries.UpColor = Color.FromRgb(0x42, 0x9a, 0xee);
            _lastSeries.DownColor = Color.FromRgb(0x42, 0x9a, 0xee);

            _wavgSeries.StrokeColor = Colors.Orange;
            _wavgSeries.UpColor = Colors.Orange;
            _wavgSeries.DownColor = Colors.Orange;

            _trendVolumeSeries.DownColor = Color.FromRgb(0x29, 0xb8, 0x1e);
            _trendVolumeSeries.UpColor = Color.FromRgb(0xf5, 0x1d, 0x27);

            var date = DateTime.Now.Date.AddHours(9).AddMinutes(30);
            int amount = 100000;
            double lastPrice = 50;
            double wavgPrice = 40;
            Random r = new Random(DateTime.Now.Millisecond);

            while (date <= DateTime.Now.Date.AddHours(11).AddMinutes(30))
            {
                TrendChart.AppendVolumeValue(TrendChart.Symbol, date, amount);
                TrendChart.AppendValue(LastSeriesName, date, lastPrice);
                TrendChart.AppendValue(WavgSeriesName, date, wavgPrice);
                amount = 100000 + (int)(r.NextDouble() * 10000 - 5000);
                lastPrice = lastPrice + r.NextDouble() * 10 - 5;
                wavgPrice = wavgPrice + r.NextDouble() * 8 - 4;
                date = date.AddMinutes(1);
            }

            date = DateTime.Now.Date.AddHours(13).AddMinutes(0);
            while (date <= DateTime.Now.Date.AddHours(15))
            {
                TrendChart.AppendVolumeValue(TrendChart.Symbol, date, null);
                TrendChart.AppendValue(LastSeriesName, date, null);
                TrendChart.AppendValue(WavgSeriesName, date, null);
                date = date.AddMinutes(1);
            }
            

            TrendChart.MaxVisibleRecords = TrendChart.RecordCount;
            TrendChart.FirstVisibleRecord = 0;
            TrendChart.LastVisibleRecord = TrendChart.RecordCount;

            TrendChart.UseLineSeriesUpDownColors = true;
            double totalPanelHeight = TrendChart.PanelsCollection.Sum(p => p.Height);
            TrendChart.SetPanelHeight(0, totalPanelHeight * 0.75);

            /* 量单位 */
            if (_trendVolumeSeries.Max < 10000)
            {

            }
            else if (_trendVolumeSeries.Max < 10000 * 10000)
            {
                TrendChart.VolumePostfixLetter = "万";
                TrendChart.VolumeDivisor = 10000;
            }
            else //if (_trendVolumeSeries.Max < 10000 * 10000 * 1000d)
            {
                TrendChart.VolumePostfixLetter = "亿";
                TrendChart.VolumeDivisor = 10000 * 10000;
            }

            TrendChart.InfoPanelPosition = InfoPanelPositionEnum.FixedPosition;

            TrendChart.IsChartScrollerVisible = false;
            _trendTopPanel.YAxisScalePrecision = 5;
            TrendChart.UserXAxisLabels.Add(DateTime.Now.Date.AddHours(9).AddMinutes(30));
            TrendChart.UserXAxisLabels.Add(DateTime.Now.Date.AddHours(10).AddMinutes(30));
            TrendChart.UserXAxisLabels.Add(DateTime.Now.Date.AddHours(11).AddMinutes(30));
            TrendChart.UserXAxisLabels.Add(DateTime.Now.Date.AddHours(14).AddMinutes(00));
            TrendChart.UserXAxisLabels.Add(DateTime.Now.Date.AddHours(15).AddMinutes(00));

            TrendChart.CalendarVersion = CalendarVersionType.UserXAxis;

            TrendChart.DeleteTimestampsRange(DateTime.Now.Date.AddHours(11).AddMinutes(31),
                        DateTime.Now.Date.AddHours(12).AddMinutes(59));

            TrendChart.Melt();
            TrendChart.Update();
        }

        /*
        private void TrendChart_InfoPanelUpdated(object sender, StockChartX.InfoPanelUpdatedEventArgs e)
        {
            throw new NotImplementedException();
        }
        */

        private void TrendChart_Loaded(object sender, RoutedEventArgs e)
        {
            InitTrendChart();
            MyTimer = new System.Timers.Timer(10000);   ///10s
            MyTimer.Elapsed += myTimer_Elapsed;
            MyTimer.AutoReset = true;
            MyTimer.Enabled = true;
        }

        private int iM = 0;

        void myTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //if (TrendChart.Symbol != null && TrendChart.Symbol == detail.StockName && detail.BidGroup.Count > 0)
            {
                try
                {
                    //Chart.Freeze();
                    /*
                    Chart.AppendOHLCValues(Chart.Symbol, detail.date, detail.OpenPrice, detail.HighPrice, detail.LowPrice,
                           detail.ClosePrice);
                           */
                    var date = DateTime.Now.Date.AddHours(13).AddMinutes(iM);
                    iM = iM + 1;
                    int amount = 100000;
                    double lastPrice = 50;
                    double wavgPrice = 40;
                    Random r = new Random(DateTime.Now.Millisecond);

                    lastPrice = lastPrice + r.NextDouble() * 10 - 5;
                    wavgPrice = wavgPrice + r.NextDouble() * 8 - 4;
                    //date = DateTime.Now;

                    Dispatcher.BeginInvoke(new Action(() =>
                    {

                        TrendChart.Freeze();

                        /*
                        TrendChart.DeleteTimestampsRange(_lastData.DateTime.AddSeconds(-_lastData.DateTime.Second),
                        _lastData.DateTime.AddSeconds(-_lastData.DateTime.Second).AddMinutes(1));
                        */
                        TrendChart.AppendValue(LastSeriesName, date, lastPrice);
                        TrendChart.AppendVolumeValue(TrendChart.Symbol, date, amount);
                        TrendChart.Melt();
                        TrendChart.Update();
                    }));
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void TrendChart_InfoPanelUpdated(object sender, StockChartX.InfoPanelUpdatedEventArgs e)
        {
            foreach(var item in e.Items)
            {
                if(item is CalendarInfoPanelItem)
                {
                    var calenderItem = (CalendarInfoPanelItem)item;
                    //calenderItem.DateTimeFormat = "yyyyMMdd hh:mm:ss";
                    calenderItem.DateTimeFormat = "hh:mm";
                }
            }
        }
    }
}
