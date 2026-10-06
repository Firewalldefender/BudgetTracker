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

    public bool Add(TransactionType type, string description, decimal amount){
        if (amount <= 0) { return false; }
        if (string.IsNullOrWhiteSpace(description)) { return false; }
        _transactions.Add(
            new Transaction(Guid.NewGuid(),
            DateTimeOffset.Now,
            type,
            description,
            amount));
        return true;
    }
}