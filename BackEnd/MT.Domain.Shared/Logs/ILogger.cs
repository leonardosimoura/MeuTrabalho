using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.Domain.Shared.Logs
{
    public interface ILogger
    {
        void AddLog(object data);
    }
}
