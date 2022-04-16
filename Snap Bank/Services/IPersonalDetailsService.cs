using Snap_Bank.Models;
using Snap_Bank.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snap_Bank.Services
{
    public interface IPersonalDetailsService
    {
        bool Save(RegisterViewModel registerViewModel);

        RegisterViewModel GetUserDetails(int accountnumber, RegisterViewModel registerViewModel);
    }
}
