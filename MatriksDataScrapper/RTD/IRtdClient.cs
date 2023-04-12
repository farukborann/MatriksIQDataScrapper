using System;
using System.Collections.Generic;
using System.Text;

namespace RTD
{
    /// <summary>
    /// IRTDClient is a client which can get data from RTD Server.
    /// </summary>
    public interface IRtdClient
    {
        /// <summary>
        /// Get the value of a stock price.
        /// </summary>
        /// <param name="args"></param>
        /// <returns>Stock price</returns>
        string GetValue(params object[] args);

        IRtdServer GetRtdServer();

    }
}
