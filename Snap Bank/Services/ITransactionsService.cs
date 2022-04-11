using Snap_Bank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap_Bank.Services
{
    public interface ITransactionsService
    {
        IEnumerable<Transactions> Get();

        bool Save(Transactions transaction);

        bool Delete(int id);

        bool Put(Transactions transaction);
    }
}
