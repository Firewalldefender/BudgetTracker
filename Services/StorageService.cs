namespace BudgetTracker.Services;

public class StorageService
{
    private readonly string _filePath;

    public StorageService(string filePath)
    {
        _filePath = filePath;
    }
}