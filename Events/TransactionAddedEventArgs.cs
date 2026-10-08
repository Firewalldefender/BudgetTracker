using BudgetTracker.Models;

namespace BudgetTracker.Events;

public sealed class TransactionAddedEvent : EventArgs
{
    public TransactionAddedEvent(Transaction transaction)
    {
        Transaction = transaction;
    }
    public Transaction Transaction { get; }
    public DateTimeOffset AddedAt = DateTimeOffset.Now;
}