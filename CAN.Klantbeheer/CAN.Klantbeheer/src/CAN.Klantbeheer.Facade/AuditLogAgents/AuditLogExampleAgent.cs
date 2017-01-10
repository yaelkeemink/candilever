using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CAN.Klantbeheer.Facade.AuditLogAgents
{
    /// <summary>
    /// Don't know for now. You should retrieve events from auditlog here
    /// You should use a custom exchange name, that doesn't interfere with the default one
    /// Send command with this custom exchange name to the AuditLog, to specific method.
    /// Do a HttpRequest to your AuditLog. dockerport/auditlogs/retrieve
    /// </summary>
    public class AuditLogExampleAgent
    {
    }
}
