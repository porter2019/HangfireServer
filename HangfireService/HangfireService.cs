using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.Owin;
using Microsoft.Owin.Hosting;

namespace HangfireService
{
    public partial class HangfireService : ServiceBase
    {
        private IDisposable apiserver = null;

        private BackgroundJobServer _hangfire_server;


        public HangfireService()
        {
            InitializeComponent();

            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);

        }

        protected override void OnStart(string[] args)
        {
            Trace.Listeners.Clear();
            Trace.Listeners.Add(new CustomTraceListener());//添加自定义监听器

            //Services URI
            string serveruri = System.Configuration.ConfigurationManager.AppSettings["WebAPIServerURI"].ToString();
            // Start OWIN host
            apiserver = WebApp.Start<Startup>(url: serveruri);

            _hangfire_server = new BackgroundJobServer();

            Trace.Write("Hangfire Service Start...");
        }

        protected override void OnStop()
        {
            if (apiserver != null)
                apiserver.Dispose();

            _hangfire_server.Dispose();

            Trace.Write("Hangfire Service Stop...");
        }
    }
}
