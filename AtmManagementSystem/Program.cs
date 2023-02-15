namespace AtmManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        AtmProcessor.Start();

        GeneralMenu.MenuOption();
    }
}
