using System.Transactions;
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
        // contacts.Add(contact);
        // SaveContacts(contacts);
        // Console.WriteLine("Contact added.");
    }

    private List<Transaction> LoadTransactions()
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
        }
    }


}