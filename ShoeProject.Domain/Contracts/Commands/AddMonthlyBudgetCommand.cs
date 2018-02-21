using ShoeTracker.Core;

namespace ShoeProject.Domain.Contracts.Commands
{
    public class AddMonthlyBudgetCommand : ICommand
    {
        public string Category { get; set; }

        public int Amount { get; set; }

        public AddMonthlyBudgetCommand(string category, int amount)
        {
            Category = category;
            Amount = amount;
        }
    }
}
