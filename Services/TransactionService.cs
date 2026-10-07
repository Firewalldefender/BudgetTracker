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

    public bool Remove(Guid id)
    {   
        if(!TryGet(id, out Transaction? transaction) || transaction is null)
            {
            return false;
            }
        _transactions.Remove(transaction);
        return true;
    }

    public bool TryGet(Guid id, out Transaction? transaction)
    {
        transaction = _transactions.Find(item => item.Id == id);
        return transaction is not null;
    }

    public IEnumerable<Transaction> Query(DateTime start,DateTime end, TransactionType? type) //warum hier DateTime und nicht Dateonly
    {
        if(type == null)
        {
          var matches = _transactions.Where(transaction => transaction.Timestamp.Date >= start.Date && transaction.Timestamp.Date <= end.Date);
        //   if(matches == null){return Enumerable.Empty<Transaction>();} Unnötig weil where ein leeres IEnum returned
          return matches;
        }
        if(type == TransactionType.Income)
        {
          var matchesIncome = _transactions.Where(
            transaction => transaction.Timestamp.Date >= start.Date &&
            transaction.Timestamp.Date <= end.Date&&
            transaction.Type == TransactionType.Income);
            // if(matchesIncome == null){return Enumerable.Empty<Transaction>();}
          return matchesIncome;
        }
        if(type == TransactionType.Expense)
        {
          var matchesExpense = _transactions.Where(
            transaction => transaction.Timestamp.Date >= start.Date &&
            transaction.Timestamp.Date <= end.Date &&
            transaction.Type == TransactionType.Expense);
            // if(matchesExpense == null){return Enumerable.Empty<Transaction>();}
          return matchesExpense;
        }
        return Enumerable.Empty<Transaction>(); //hier wird das leere IEnum returned
    }


}