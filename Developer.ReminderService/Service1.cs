using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Developer.ReminderService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            int interval = int.Parse(ConfigurationSettings.AppSettings["StartTime"]);


        }

        protected override void OnStop()
        {
        }

        internal void Process()
        {
            throw new NotImplementedException();
        }
    }
}
