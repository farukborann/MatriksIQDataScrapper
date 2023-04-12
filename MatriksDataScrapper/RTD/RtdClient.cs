using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RTD
{
    public class RtdClient
    {
        private readonly string _rtdProgId;

        private Dictionary<int, Action<string>> Topics;

        static IRtdServer _rtdServer;
        public RtdClient(string rtdProgId)
        {
            _rtdProgId = rtdProgId;

            Topics = new Dictionary<int, Action<string>>();

            GetRtdServer();

            Task.Run(() => { StartTransfer(); });
        }

        async void StartTransfer ()
        {
            while (true)
            {
                var alive = _rtdServer.Heartbeat();
                if (alive != 1) GetRtdServer();

                var refresh = _rtdServer.RefreshData(Topics.Count);
                if (refresh.Length <= 0) continue;

                for (int i = 0;i < refresh.Length/2; i++)
                {
                    var topic = refresh[0,i].ToString();

                    if(topic == null) continue;

                    int topicId = Convert.ToInt32(topic);
                    if (!Topics.ContainsKey(topicId)) continue;

                    var callback = Topics[topicId];
                    callback(refresh[1, i].ToString() ?? "");
                }
            }
        }

        public int AddTopic(string name, Action<string> callback)
        {
            var rand = new Random();
            var topicId = rand.Next(int.MaxValue);

            _rtdServer.ConnectData(topicId, new object[] {name}, false);
            Topics.Add(topicId, callback);
            return topicId;
        }

        public void DeleteTopic(int topicId)
        {
            _rtdServer.DisconnectData(topicId);
            Topics.Remove(topicId);
        }

        public IRtdServer GetRtdServer()
        {
            if (_rtdServer == null)
            {
                Type rtd = Type.GetTypeFromProgID(_rtdProgId);
                _rtdServer = (IRtdServer)Activator.CreateInstance(rtd);
                _rtdServer.ServerStart(new RTDUpdateEvent());
            }
            return _rtdServer;
        }

    }
}
