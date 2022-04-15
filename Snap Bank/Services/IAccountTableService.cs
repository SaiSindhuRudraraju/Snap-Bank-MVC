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
        RegisterViewModel GetUserData(String Username);
        bool CheckUserName(String username);
        void ValidateAccountType(RegisterViewModel registerViewModel);
        int GetNumberOfUsers(String username);
        int GetNumberOfUsers(int accountnumber);

        string getUserAccountType(String username);
        string getUserAccountType(int accountnumber);
        RegisterViewModel GetUserData(int AccountNumber);
        bool CheckUserPassword(String username, String password);

        bool CheckUserPin(int accountnumber, int pin);

        HomePageDetailesViewModel GetUserByName(String username);

        HomePageDetailesViewModel GerUserByNumber(int accountnumber);

        bool Delete(int id);

        bool Put(RegisterViewModel registerViewModel);
    }
}
