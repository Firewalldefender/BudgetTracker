using BudgetTracker.Models;

namespace BudgetTracker.Services;

public class TransactionService
{
    private readonly List<Transaction> _transactions;

    private readonly Action<string> _log;

    public TransactionService(List<Transaction> transactions, Action<string> log)
    {
        _transactions = transactions;
        _log = log;
    }
}