using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Framework.EFCore
{
    public interface IDataSeed
    {
        Task Init();
    }
}
