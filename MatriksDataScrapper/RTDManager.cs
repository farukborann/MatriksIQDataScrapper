using RTD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatriksDataScrapper
{
    static class RTDManager
    {
        public static RtdClient Client;
        static RTDManager()
        {
            try
            {
                Client = new RtdClient("mtrtd.1");
            } catch
            {
                MessageBox.Show("Matrikse Ulaşılamıyor.");
                Environment.Exit(0);
            }
        }

    }
}
