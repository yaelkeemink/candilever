﻿using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Serilog;
using RabbitMQ.Client.Events;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.Common.Events;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using Can.BackOffice.Domain.Entities;
using CAN.Webwinkel.Infrastructure.EventListener;
using CAN.BackOffice.Domain.Entities;
using System.Text;
using Newtonsoft.Json;
using System;

namespace CAN.BackOffice.Infrastructure.EventListener.Dispatchers
{
    public partial class BackOfficeEventDispatcher : EventDispatcher
    {
        private DbContextOptions<DatabaseContext> _dbOptions;
        private ILogger _logger;
        private EventListenerLock _locker;
        public BackOfficeEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
        }

        public BackOfficeEventDispatcher(BusOptions options, DbContextOptions<DatabaseContext> dbOptions, ILogger logger, EventListenerLock locker) : base(options)
        {
            _logger = logger;
            _dbOptions = dbOptions;
            _locker = locker;
        }


        protected override void EventReceived(object sender, BasicDeliverEventArgs e)
        {
            if (_locker != null)
            {
                _logger.Debug($"Event received");
                _locker.EventReceived();
            }

            base.EventReceived(sender, e);
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            if (Channel == null)
            {
                return false;
            }
            return Channel.IsOpen;
        }
    }
}