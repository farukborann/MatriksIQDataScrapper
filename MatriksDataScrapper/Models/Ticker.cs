using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatriksDataScrapper.Models
{
    class Ticker
    {
        public string Name { get; set; }
        public string Alis { get; set; }
        public string Satis { get; set; }
        public string Son { get; set; }
        public int UpdateCount { get; set; }

        TickerSource Source { get; set; }

        public Ticker(string Name, Action callback)
        {
            this.Name = Name;

            Source = new(Name, (Type, Value, DataCount) =>
            {
                switch (Type)
                {
                    case "ALIS":
                        Alis = Value;
                        break;
                    case "SATIS":
                        Satis = Value;
                        break;
                    case "SON":
                        Son = Value;
                        break;
                }
                UpdateCount = DataCount;
                callback();                
            });
        }
    }
}
