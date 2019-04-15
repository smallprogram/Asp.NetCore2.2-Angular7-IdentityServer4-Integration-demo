using SmallProgramDemo.Core.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmallProgramDemo.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyContext myContext;

        public UnitOfWork(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public async Task<bool> SaveAsync()
        {
            return await myContext.SaveChangesAsync() > 0;
        }
    }
}
