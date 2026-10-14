using BudgetTracker.Events;

namespace BudgetTracker.Services;

public class LoggerService
{
    public void Subscribe(TransactionService transactionService)
    {
        transactionService.TransactionAdded += OnTransactionAdded;
    }

        public void OnTransactionAdded(object? sender, TransactionAddedEvent e) => File.AppendAllText("data/transactions.log", $"[LOG] [{e.AddedAt}]: {e.Transaction.Description} with {e.Transaction.Type} {e.Transaction.Amount} EUR" + Environment.NewLine);
}