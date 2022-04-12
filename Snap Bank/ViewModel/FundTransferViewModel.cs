using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snap_Bank.ViewModel
{
    public class FundTransferViewModel
    {
        public SelfAccountTransferViewModel selfAccountViewModel { get; set; }
        public DifferentAccountTransferModel differentAccountTransferModel { get; set; }
    }
    public class SelfAccountTransferViewModel
    {
        public String FromAccountType { get; set; }
        public String ToAccountType { get; set; }
        public Decimal AmountToTransfer { get; set; }
    }
    public class DifferentAccountTransferModel
    {
        public String FromAccountType { get; set; }
        public String CountryName { get; set; }
        public Decimal AccountNumber { get; set; }
        public int sortcode1 { get; set; }
        public int sortcode2 { get; set; }
        public int sortcode3 { get; set; }
        public String AccountHolder { get; set; }
        public String BankName { get; set; }
        public Decimal AmountToTransfer { get; set; }
        public Decimal ConvertedAmount { get; set; }
        public int ConversionRate { get; set; }
        public int pin { get; set; }
    }
    public enum CountryName
    {
        UnitedKingdom,
        Europe,
        UnitedStatesOfAmerica
    }
}