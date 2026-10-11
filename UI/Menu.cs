using System.ComponentModel;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using BudgetTracker.Models;
using BudgetTracker.Services;

namespace BudgetTracker.UI;

class Menu
{
    static StorageService storage = new StorageService("data/storage.json");
    static TransactionService service = new TransactionService(storage);
    static LoggerService logger = new LoggerService();

    public void Start()
    {   
        logger.Subscribe(service);
        while (true)
        {
            Console.WriteLine($"");
            Console.WriteLine($"");
            Console.WriteLine($"=====================================================");
            Console.WriteLine($"Welcome to Marlin's Money-Transactions");
            Console.WriteLine($"Please Choose an Option between 1 and 8");
            Console.WriteLine($"1 Check your Balance");
            Console.WriteLine($"2 Add a Income");
            Console.WriteLine($"3 Add a Expense");
            Console.WriteLine($"4 Remove a Transaction by ID");
            Console.WriteLine($"5 Find Transaction by ID");
            Console.WriteLine($"6 Show all Transactions");
            Console.WriteLine($"7 Show Transaction between Start_Day and End_Day, may filtered for Income or Expense");
            Console.WriteLine($"8 Quit Service");
            Console.WriteLine($"=====================================================");
            
            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1": 
                decimal Balance = 0;
                foreach(Transaction transaction in storage.LoadTransactions())
                    {
                        if(transaction.Type == TransactionType.Income)
                        {
                            Balance += transaction.Amount;
                        }else if(transaction.Type == TransactionType.Expense)
                        {
                            Balance -= transaction.Amount;
                        }
                    }
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"Your Balance is {Balance} EUR");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");
                continue;

                case "2":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"You choose to add a new Income");
                Console.WriteLine($"Please type in a Description");
                var description = Console.ReadLine();
                if (description == null)
                {
                Console.WriteLine($"Please type something, returning to Menu...");
                continue;
                }
                Console.WriteLine($"Please type in an Amount");
                decimal amount = Decimal.Parse(Console.ReadLine());
                service.Add(TransactionType.Income,description,amount);
                Console.WriteLine($"====================================================="); 
                Console.WriteLine($""); 
                continue;

                case "3":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"You choose to add a new Expense");
                Console.WriteLine($"Please type in a Description");
                var description1 = Console.ReadLine();
                if (description1 == null)
                {
                Console.WriteLine($"Please type something, returning to Menu...");
                continue;
                }
                Console.WriteLine($"Please type in an Amount");
                decimal amount1 = Decimal.Parse(Console.ReadLine());
                service.Add(TransactionType.Expense,description1,amount1);
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");  
                continue;

                case "4":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"You choose to Remove an Transaction");
                Console.WriteLine($"Please give the correct ID of the Transaction");
                Guid id = Guid.Parse(Console.ReadLine());
                service.Remove(id);
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");
                continue;

                case "5":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"You choose to find an Transaction");
                Console.WriteLine($"Please give the correct ID of the Transaction");
                Guid id1 = Guid.Parse(Console.ReadLine());
                service.Remove(id1);
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");
                continue;

                case "6":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"All transactions Loading");
                List<Transaction> transactions = storage.LoadTransactions();
                foreach(Transaction transaction in transactions)
                    {
                        Console.WriteLine($"-----------------------------------------------------");
                        Console.WriteLine($"Description: {transaction.Description}");
                        Console.WriteLine($"Amount: {transaction.Amount}");
                        Console.WriteLine($"Date: {transaction.Timestamp}");
                        Console.WriteLine($"ID: {transaction.Id}");
                        Console.WriteLine($"Type: {transaction.Type}");
                        Console.WriteLine($"-----------------------------------------------------");
                        Console.WriteLine($"");

                        
                    }
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");
                continue;
                
                case "7":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"You choose to load from a Start- to a End-Point.");
                Console.WriteLine($"Please Type in a valid day (Start)");
                var day_start = DateTimeOffset.Parse(Console.ReadLine());
                

                continue;

        
                case "8":
                Console.WriteLine($"");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"Thank you for using Marlin's Money-Transactions");
                Console.WriteLine($"=====================================================");
                Console.WriteLine($"");
                return;

                

                default:
                    Console.WriteLine($"Invalid option. Please choose 1-8.");
                    continue;
            }


            
            
            
            
            
            

        }
    }
}

//bei bedarf um null cases abzufangen cr in extra var und null-case abfangen