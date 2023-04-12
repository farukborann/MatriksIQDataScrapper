using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MatriksDataScrapper.Models
{
    class TickerSource: IDisposable
    {
        private string Name;
        public int[] TopicIds;

        string[] ValueKeys = new string[] { "ALIS", "SATIS", "SON" };

        private Dictionary<string, string> Values;
        
        public int DataCount;

        public TickerSource(string Name, Action<string, string, int> callback)
        {
            this.Name = Name;
            
            TopicIds = new int[ValueKeys.Length];
            Values = new Dictionary<string, string>();

            for(int i = 0;i < ValueKeys.Length; i++)
            {
                string Key = ValueKeys[i];
                Values[Key] = "-1";
            }

            this.DataCount = 0;

            Task.Run(() => { StartTransfer(callback); });
            Task.Run(() => { AutoSaveDatabase(); });
        }

        private async void StartTransfer(Action<string, string, int> callback)
        {
            for(int i = 0;i < ValueKeys.Length; i++) 
            {
                string Key = ValueKeys[i];
                TopicIds[i] = RTDManager.Client.AddTopic(Name + "." + Key, (Value) =>
                {
                    if (Values[Key] != Value)
                    {
                        Values[Key] = Value;

                        Database.Tick _tick = new(Name, Key, Value);
                        Database.Context.Init.Add(_tick);
                        DataCount++;

                        callback(Key, Value, DataCount);
                    }
                });
            }
        }

        private async void AutoSaveDatabase()
        {
            var periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(1));
            while (await periodicTimer.WaitForNextTickAsync())
            {
                Database.Context.Init.SaveChanges();
            }
        }

        public void Dispose ()
        {
            for(int i = 0;i < TopicIds.Length; i++)
            {
                RTDManager.Client.DeleteTopic(TopicIds[i]);
            }
        }
    }
}
