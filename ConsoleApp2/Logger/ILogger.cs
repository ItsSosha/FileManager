using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loggers
{
    public interface ILogger
    {
        public void Log(bool equalty, int testNumber);
    }
}
