using System;

namespace MatriksDataScrapper.Database
{
    class Tick
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime Date { get; set; }

        public Tick(string Name, string Key, string Value)
        {
            this.Name = Name;
            this.Key = Key; 
            this.Value = Value;
            this.Date = DateTime.Now;
        }
    }
}
