using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heeelp.Core.Logging
{
    public enum EventSource
    {
        DataAccessSQL,
        DataAccessAD,
        Encryption,
        SyncService,
        AdminPanel,
        ControlPanel,
        PermissionServer,
        Portal,
        GarbageCleaner,
        FileExtraction
    }

    public enum EventType
    {
        Error = 1,
        Warning = 2,
        Information = 4,
    }

    public enum enumTraceSource
    {
        HeeelpWebApi,
        HeeelpUI
        
    }
}
