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
        bool Save(RegisterViewModel registerViewModel);

        void ValidateAccountType(RegisterViewModel registerViewModel);

        bool CheckUserName(String username);

        bool CheckUserPassword(String username, String password);

        bool CheckUserPin(int accountnumber, int pin);

        String GetUserName(int accountnumber);

        String GetUserNumber(string username);

        RegisterViewModel GetUserDetails(int accountnumber, RegisterViewModel registerViewModel);

        int GetNumberOfUsers(String username);

        int GetNumberOfUsers(int accountnumber);

        string getUserAccountType(String username);

        HomePageDetailesViewModel GetUserByName(String username);

        HomePageDetailesViewModel GetUserByNumber(int accountnumber);
        bool TransferAmountFromTo(SelfAccountTransferViewModel selfAccountViewModel, int fromAccount, int toAccount);
        bool TransferAmountFromTo(DifferentAccountTransferModel differentAccountTransferModel, int fromAccount, int toAccount);
        int GetAccountNumberByAccountType(string username, string AccountType);
        decimal GetSavingAccountAmount(string username);
        decimal GetCurrentAccountAmount(string username);
        int GetAccountNumber(string username, string AccountType);
    }
}
