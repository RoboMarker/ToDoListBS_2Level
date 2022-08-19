using Generally.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Generally.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        public ITodoListRepository Task => throw new NotImplementedException();

        public UnitOfWork(ITodoListRepository task)
        {
            Tasks = task;
        }
        public ITodoListRepository Tasks { get; }
    }
}
