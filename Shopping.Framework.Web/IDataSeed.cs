using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.Web
{
    public interface IDataSeed
    {
        Task Init();
    }
}
