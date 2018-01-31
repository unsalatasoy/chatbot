using Microsoft.Bot.Builder.FormFlow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reflex.Models
{

    public enum FinanceTypeOption
    {
        CustomerLoan,
        Business,
        HomeLoan,
        VehicleLoan,
    }

    [Serializable]
    public class FinanceCalculation
    {
        public FinanceTypeOption FinanceTypeOption;
        public int NumberOfInstallment;
        public float AmountOfFinance;

        public static IForm<FinanceCalculation> BuildForm()
        {
            return new FormBuilder<FinanceCalculation>()
                .Message("Welcome to the Finance Calculator!")
                .Build();
        }

    }
}