using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RTD
{
    public class RTDUpdateEvent : IRTDUpdateEvent
    {
        void IRTDUpdateEvent.UpdateNotify()
        {
            //throw new NotImplementedException();
        }

        int IRTDUpdateEvent.HeartbeatInterval { get => 1000; set => throw new NotImplementedException(); }

        void IRTDUpdateEvent.Disconnect()
        {
            //throw new NotImplementedException();
        }
    }
}