using BudgetTracker.Events;

namespace BudgetTracker.Services;

public class LoggerService
{
    public void Subscribe(TransactionService transactionService)
    {
        transactionService.TransactionAdded += OnTransactionAdded;
    }

        public void OnTransactionAdded(object? sender, TransactionAddedEvent e) => Console.WriteLine($"[LOG] [{e.AddedAt}]: {e.Transaction.Description} with + {e.Transaction.Amount} EUR");
}