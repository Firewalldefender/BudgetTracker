using BudgetTracker.Events;
using BudgetTracker.Models;

namespace BudgetTracker.Services;

public class TransactionService
{
    private readonly StorageService _storage;

    public event EventHandler<TransactionAddedEvent>? TransactionAdded;

    public TransactionService(StorageService transactions)
    {
        _storage = transactions;
    }

    public bool Add(TransactionType type, string description, decimal amount){
        if (amount <= 0) { return false; }
        if (string.IsNullOrWhiteSpace(description)) { return false; }
        Transaction newTrans = new Transaction(
            Guid.NewGuid(),
            DateTimeOffset.Now,
            type,
            description,
            amount);
        _storage.AddTransaction(newTrans);
        OnTransactionAdded(new TransactionAddedEvent(newTrans));
        return true;
    }

    public bool Remove(Guid id)
    {   
        return _storage.RemoveTransaction(id);
    }

    public bool TryGet(Guid id, out Transaction? transaction)
    {
        transaction = _storage.LoadTransactions().Find(item => item.Id == id);
        return transaction is not null;
    }

    public IEnumerable<Transaction> Query(DateTime start,DateTime end, TransactionType? type) //warum hier DateTime und nicht Dateonly
    {
        if(type == null)
        {
          var matches = _storage.LoadTransactions().Where(transaction => transaction.Timestamp.Date >= start.Date && transaction.Timestamp.Date <= end.Date);
        //   if(matches == null){return Enumerable.Empty<Transaction>();} Unnötig weil where ein leeres IEnum returned
          return matches;
        }
        if(type == TransactionType.Income)
        {
          var matchesIncome = _storage.LoadTransactions().Where(
            transaction => transaction.Timestamp.Date >= start.Date &&
            transaction.Timestamp.Date <= end.Date&&
            transaction.Type == TransactionType.Income);
            // if(matchesIncome == null){return Enumerable.Empty<Transaction>();}
          return matchesIncome;
        }
        if(type == TransactionType.Expense)
        {
          var matchesExpense = _storage.LoadTransactions().Where(
            transaction => transaction.Timestamp.Date >= start.Date &&
            transaction.Timestamp.Date <= end.Date &&
            transaction.Type == TransactionType.Expense);
            // if(matchesExpense == null){return Enumerable.Empty<Transaction>();}
          return matchesExpense;
        }
        return Enumerable.Empty<Transaction>(); //hier wird das leere IEnum returned
    }

    protected virtual void OnTransactionAdded(TransactionAddedEvent e) => TransactionAdded?.Invoke(this, e);


}