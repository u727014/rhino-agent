﻿/*
 * CHANGE LOG - keep only last 5 threads
 * 
 * RESSOURCES
 */
using Microsoft.Extensions.Configuration;

using System.Net;
using System.Net.Sockets;

namespace Rhino.Controllers.Models
{
    public class AppSettings
    {
        public const string ApiVersion = "3";

        public AppSettings(IConfiguration configuration)
        {
            // setup
            Hub ??= new HubConfiguration();
            ReportsAndLogs ??= new ReportConfiguration();
            Worker ??= new WorkerConfiguration();
            Plugins ??= new PluginsConfiguration();
            Configuration = configuration;

            // bind
            configuration.GetSection("Rhino:HubConfiguration").Bind(Hub);
            configuration.GetSection("Rhino:ReportConfiguration").Bind(ReportsAndLogs);
            configuration.GetSection("Rhino:WorkerConfiguration").Bind(Worker);
            configuration.GetSection("Rhino:PluginsConfiguration").Bind(Worker);
        }

        public IConfiguration Configuration { get; }

        public HubConfiguration Hub { get; }

        public ReportConfiguration ReportsAndLogs { get; }

        public WorkerConfiguration Worker { get; }

        public PluginsConfiguration Plugins { get; }

        /// <summary>
        /// Gets the local IPv6 address.
        /// </summary>
        /// <returns>IPv6 Address.</returns>
        public static string GetLocalAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork))
            {
                if (!string.IsNullOrEmpty(ip.ToString())) return ip.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Child configuration
        /// </summary>
        public class HubConfiguration
        {
            public double CreationTimeout { get; set; }
            public int MaxCompleted { get; set; }
            public int RepairAttempts { get; set; }
            public double RunningTimeout { get; set; }
        }

        /// <summary>
        /// Child configuration
        /// </summary>
        public class WorkerConfiguration
        {
            public string HubAddress { get; set; }
            public string HubApiVersion { get; set; }
            public int MaxParallel { get; set; }
            public double ConnectionTimeout { get; set; }
        }

        /// <summary>
        /// Child configuration
        /// </summary>
        public class ReportConfiguration
        {
            public bool Archive { get; set; }
            public string LogsOut { get; set; }
            public IEnumerable<string> Reporters { get; set;}
            public string ReportsOut { get; set; }
        }

        /// <summary>
        /// Child configuration
        /// </summary>
        public class PluginsConfiguration
        {
            public IEnumerable<string> Locations { get; set;}
        }
    }
}