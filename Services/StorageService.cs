using BudgetTracker.Models;
using System.Text.Json;
using System.Net.WebSockets;
using System.Runtime.InteropServices;

namespace BudgetTracker.Services;

public class StorageService
{
    private readonly string _filePath;

    public StorageService(string filePath)
    {
        _filePath = filePath;
    }

    public void AddTransaction(Transaction transaction)
    {
        var transactions = LoadTransactions();
        transactions.Add(transaction);
        SaveTransactions(transactions);
        Console.WriteLine("Transaction added.");
    }

    public bool RemoveTransaction(Guid id)
    {
        var transactions = LoadTransactions();
        var removed = transactions.RemoveAll(c => c.Id == id);
        SaveTransactions(transactions);

        if (removed > 0)
        {            
            Console.WriteLine("Transaction removed.");
            return true;
        }
        else
        {
            Console.WriteLine("No transaction found with that ID.");
            return false;
        }
    }

    public List<Transaction> LoadTransactions() //ist public hier okay?
    {
        try
        {
            if (!File.Exists(_filePath)) return new List<Transaction>();
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Transaction>>(json) ?? new List<Transaction>();
        }
        catch (Exception ex) when (ex is IOException || ex is JsonException) 
        {
            Console.WriteLine($"Error loading transactions:  {ex.Message}");
            return new List<Transaction>();
        }}
    
        private void SaveTransactions(List<Transaction> transactions)
    {
        try
        {
            string json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex) when (ex is IOException || ex is JsonException)
        {
            Console.WriteLine($"Error saving transactions:  {ex.Message}");
        }
    }
    }