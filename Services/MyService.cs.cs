namespace ApiCatalog.Services
{
    public class MyService: IMyService
    {
       public string Greeting(string name)
        {
            return $"Welcome, {name} \n\n{DateTime.UtcNow}";
        }
    }
}
