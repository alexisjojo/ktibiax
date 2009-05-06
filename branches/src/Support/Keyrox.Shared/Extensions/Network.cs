using System;
using System.Linq;
using System.Net.NetworkInformation;

namespace Keyrox.Shared.Extensions {
    public static class Network {

        public static int GetNextAvailablePort(int port) {

            var ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            var tcpConnInfoArray = ipGlobalProperties.GetActiveTcpConnections();

            var currentPort = port;
            var res = from tcpi in tcpConnInfoArray where tcpi.LocalEndPoint.Port == currentPort select tcpi;

            while (res.Count() > 0) {
                currentPort += 1;
                res = from tcpi in tcpConnInfoArray where tcpi.LocalEndPoint.Port == currentPort select tcpi;
            }
            return currentPort;
        }

    }
}
