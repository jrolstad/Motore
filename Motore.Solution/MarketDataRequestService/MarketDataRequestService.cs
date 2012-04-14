using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Motore.Library.Logging;
using Motore.Library.MarketData;

namespace MarketDataRequestService
{
    public partial class MarketDataRequestService : ServiceBase
    {
        private bool _running = false;
        private MarketDataRequestProcessor _processor = null;

        public MarketDataRequestService()
        {
            InitializeComponent();
        }

        protected internal virtual void Run()
        {
            try
            {
                var processor = this.Processor;

                while (_running)
                {
                    processor.Process();
                    Thread.Sleep(1000);
                }
            }
            catch (Exception exc)
            {
                _running = false;
                var log = new Log();
                log.LogException(exc);
            }
        }

        protected override void OnStart(string[] args)
        {
            _running = true;
            base.OnStart(args);
            Run();
        }

        protected override void OnStop()
        {
            _running = false;
            base.OnStop();
        }

        protected internal virtual MarketDataRequestProcessor Processor
        {
            get { return _processor ?? (_processor = new MarketDataRequestProcessor()); }
        }
    }
}
