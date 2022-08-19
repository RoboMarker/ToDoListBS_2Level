using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generally.Interfaces
{
    public interface IUnitOfWork
    {
        ITodoListRepository Task { get; }
    }
}
