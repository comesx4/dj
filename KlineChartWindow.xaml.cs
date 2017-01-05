using ModulusFE.Indicators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace ModulusFE.Demo
{
    public partial class KlineChartWindow : UserControl
    {
        private const string LastSeriesName = "最新价";
        private const string WavgSeriesName = "加权平均价";


        private ChartPanel _klineTopPanel;
        private ChartPanel _klineVolumePanel;
        private Series _klineVolumeSeries;

        private ChartPanel _panel;

        private Indicator _macd;
        private Indicator _kdj;
        private Indicator _rsi;
        private Indicator _boll;
        private Indicator _wr;
        private Indicator _asi;
        private Indicator _vr;
        private Indicator _arbr;
        private Indicator _dpo;
        private Indicator _trix;
        private Indicator _dma;
        private Indicator _bbi;
        private Indicator _mtm;
        private Indicator _obv;

        public KlineChartWindow()
        {
            InitializeComponent();
        }

        private void InitKlineChart()
        {
            KlineChart.Freeze();
            KlineChart.ClearAll();
            KlineChart.Symbol = "恒生电子";

            _klineTopPanel = KlineChart.AddChartPanel();
            if (null == _klineTopPanel)
            {
                return;
            }
            _klineVolumePanel = KlineChart.AddChartPanel(ChartPanel.PositionType.AlwaysBottom);

            KlineChart.SetStyle();
            _klineTopPanel.SetStyle();
            _klineVolumePanel.SetStyle();

            _klineVolumeSeries = KlineChart.AddVolumeSeries(KlineChart.Symbol, _klineVolumePanel.Index);
            _klineVolumeSeries.StrokeThickness = 0;

            KlineChart.AddOHLCSeries(KlineChart.Symbol, 0);


            /* 最大可显示所有数据 */
            KlineChart.MaxVisibleRecords = 100;

            var json = File.ReadAllText("data\\sh600570.json");
            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<StockData>(json);

            /* 添加数据点 */
            foreach (var record in data.Record)
            {
                KlineChart.AppendOHLCValues(KlineChart.Symbol, DateTime.Parse(record[0]), double.Parse(record[1]), double.Parse(record[2]), double.Parse(record[4]), double.Parse(record[3]));
                KlineChart.AppendVolumeValue(KlineChart.Symbol, DateTime.Parse(record[0]), double.Parse(record[5]));
            }


            /* 设置Panel比例为3:1 */
            double totalPanelHeight = KlineChart.PanelsCollection.Sum(p => p.Height);
            KlineChart.SetPanelHeight(0, totalPanelHeight * 0.75);

            /* 量单位 */

            if (_klineVolumeSeries.Max < 10000)
            {


            }
            else if (_klineVolumeSeries.Max < 10000 * 10000)
            {
                KlineChart.VolumePostfixLetter = "万";
                KlineChart.VolumeDivisor = 10000;
            }
            else //if (_volumeSeries.Max < 10000 * 10000 * 1000d)
            {
                KlineChart.VolumePostfixLetter = "亿";
                KlineChart.VolumeDivisor = 10000 * 10000;
            }

            KlineChart.InfoPanelPosition = InfoPanelPositionEnum.FixedPosition;

            KlineChart.Melt();
            KlineChart.Update();
        }

        private void KlineChart_Loaded(object sender, RoutedEventArgs e)
        {
            InitKlineChart();
        }

        private void Clean()
        {
            if (null != _macd)
            {
                KlineChart.RemoveSeries(_macd);
                _macd = null;
            }
            if (null != _kdj)
            {
                KlineChart.RemoveSeries(_kdj);
                _kdj = null;
            }
            if (null != _rsi)
            {
                KlineChart.RemoveSeries(_rsi);
                _rsi = null;
            }
            if (null != _boll)
            {
                KlineChart.RemoveSeries(_boll);
                _boll = null;
            }
            if (null != _wr)
            {
                KlineChart.RemoveSeries(_wr);
                _wr = null;
            }
            if (null != _asi)
            {
                KlineChart.RemoveSeries(_asi);
                _asi = null;
            }
            if (null != _vr)
            {
                KlineChart.RemoveSeries(_vr);
                _vr = null;
            }
            if (null != _arbr)
            {
                KlineChart.RemoveSeries(_arbr);
                _arbr = null;
            }
            if (null != _dpo)
            {
                KlineChart.RemoveSeries(_dpo);
                _dpo = null;
            }
            if (null != _trix)
            {
                KlineChart.RemoveSeries(_trix);
                _trix = null;
            }
            if (null != _dma)
            {
                KlineChart.RemoveSeries(_dma);
                _dma = null;
            }
            if (null != _bbi)
            {
                KlineChart.RemoveSeries(_bbi);
                _bbi = null;
            }
            if (null != _mtm)
            {
                KlineChart.RemoveSeries(_mtm);
                _mtm = null;
            }
            if (null != _obv)
            {
                KlineChart.RemoveSeries(_obv);
                _obv = null;
            }


            _panel = KlineChart.AddChartPanel(ChartPanel.PositionType.AlwaysBottom);

            if (null == _panel)
            {
                return;
            }
            _panel.SetStyle();

            var sumHeight = KlineChart.PanelsCollection.Sum(panel => panel.Height);
            KlineChart.SetPanelHeight(0, sumHeight * 0.33);
            KlineChart.SetPanelHeight(1, sumHeight * 0.33);
            KlineChart.SetPanelHeight(2, sumHeight * 0.33);
        }

        private void MACD_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _macd = KlineChart.AddIndicator(IndicatorType.MACD, "MACD", _panel, true);

            KlineChart.Update();
        }

        private void KDJ_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _kdj = KlineChart.AddIndicator(IndicatorType.KDJ, "KDJ", _panel, true);
            _kdj.SetParameterValue(0, "StockName");
            _kdj.SetParameterValue(1, 9);
            _kdj.SetParameterValue(2, 3);
            _kdj.SetParameterValue(3, 3);

        }
        private void RSI_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _rsi = KlineChart.AddIndicator(IndicatorType.RelativeStrengthIndex, "RSI", _panel, true);
            KlineChart.Update();
        }

        private void BOLL_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _boll = KlineChart.AddIndicator(IndicatorType.BollingerBands, "BOLL", _panel, false);
        }

        private void WR_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _wr = KlineChart.AddIndicator(IndicatorType.WilliamsPctR, "WR", _panel, false);
        }

        private void BIAS_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _kdj = KlineChart.AddIndicator(IndicatorType.BIAS, "BIAS", _panel, false);
        }

        private void ASI_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _asi = KlineChart.AddIndicator(IndicatorType.AccumulativeSwingIndex, "ASI", _panel, false);
        }

        private void VR_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _vr = KlineChart.AddIndicator(IndicatorType.VR, "VR", _panel, false);
        }

        private void ARBR_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _arbr = KlineChart.AddIndicator(IndicatorType.ARBR, "ARBR", _panel, false);
        }

        private void DPO_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _dpo = KlineChart.AddIndicator(IndicatorType.DPO, "DPO", _panel, false);
        }

        private void TRIX_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _trix = KlineChart.AddIndicator(IndicatorType.TRIX, "TRIX", _panel, false);
        }

        private void DMA_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _dma = KlineChart.AddIndicator(IndicatorType.DMA, "DMA", _panel, false);
        }

        private void BBI_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _bbi = KlineChart.AddIndicator(IndicatorType.BBI, "BBI", _panel, false);
        }

        private void MTM_Click(object sender, RoutedEventArgs e)
        {
            Clean();
            _mtm = KlineChart.AddIndicator(IndicatorType.MTM, "MTM", _panel, false);
        }

        private void OBV_Click_15(object sender, RoutedEventArgs e)
        {
            Clean();
            _obv = KlineChart.AddIndicator(IndicatorType.OnBalanceVolume, "OBV", _panel, false);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (KlineChart.InfoPanelItemsOrientation == Controls.InfoPanelItemsOrientation.Horizontal)
            {
                KlineChart.InfoPanelItemsOrientation = Controls.InfoPanelItemsOrientation.Vertical;
            }
            else
            {
                KlineChart.InfoPanelItemsOrientation = Controls.InfoPanelItemsOrientation.Horizontal;
            }
        }
    }

    public class StockData
    {
        public List<List<string>> Record { get; set; }
    }
}