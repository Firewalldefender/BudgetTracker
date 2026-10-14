using BudgetTracker.Events;
using BudgetTracker.Services;
using BudgetTracker.Models;

var storage = new StorageService("data/storage.json");
var service = new TransactionService(storage);

var logger = new LoggerService();
logger.Subscribe(service);

service.Add(TransactionType.Income,"Work", 2000);
service.Add(TransactionType.Income,"Work2", 220);
service.Add(TransactionType.Income,"Schweigegeild", 10000);