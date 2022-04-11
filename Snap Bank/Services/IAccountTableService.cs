using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap_Bank.Services
{
    public interface IAccountTableService
    {
        IEnumerable<AccountTable> Get();

        bool Save(RegisterViewModel registerViewModel);

        bool Delete(int id);

        bool Put(RegisterViewModel registerViewModel);
    }
}
