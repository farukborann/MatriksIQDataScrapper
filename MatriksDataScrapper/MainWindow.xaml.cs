using MatriksDataScrapper.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MatriksDataScrapper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Ticker BTCLast = new Ticker("BTC_USDT_BIN", () =>
            {
                TickersDataGrid.Dispatcher.Invoke(new Action(() => { TickersDataGrid.Items.Refresh(); }));
            });

            TickersDataGrid.AutoGenerateColumns = false;
            List<Ticker> tickers = new()

            {
                BTCLast
            };

            TickersDataGrid.ItemsSource = tickers;
        }
    }
}
