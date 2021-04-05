using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Module_3_Practice_4.Models;

namespace Module_3_Practice_4.Services.Abstractions
{
    public interface IConfigService
    {
        LoggerConfig Read();
    }
}
