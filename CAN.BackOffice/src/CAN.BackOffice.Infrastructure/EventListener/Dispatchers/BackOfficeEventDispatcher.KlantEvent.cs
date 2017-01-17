using CAN.BackOffice.Domain.Entities;
using CAN.BackOffice.Infrastructure.DAL;
using CAN.BackOffice.Infrastructure.DAL.Repositories;
using CAN.Common.Events;
using CAN.Webwinkel.Infrastructure.EventListener;
using InfoSupport.WSA.Infrastructure;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client.Events;
using Serilog;

namespace CAN.BackOffice.Infrastructure.EventListener.Dispatchers
{

    public partial class BackOfficeEventDispatcher
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="evt"></param>
        public void KlantAangemaakt(KlantCreatedEvent evt)
        {
            _logger.Debug($"Klant aangemaakt {evt.Klantnummer}");
            using (var context = new DatabaseContext(_dbOptions))
            using (var repo = new KlantRepository(context))
            {
                var klant = new Klant(evt);
                repo.Insert(klant);
            }
        }

    }
}
