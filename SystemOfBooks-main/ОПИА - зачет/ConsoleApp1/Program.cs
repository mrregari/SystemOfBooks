using System.Globalization;

namespace ConsoleApp1;

class Program
{
    static void Main(string[] args)
    {
        LibrarySystem librarySystem = new LibrarySystem();
        librarySystem.LoadBooks();
        while(true){
            Console.WriteLine("1. Сгенерировать отчет");
            Console.WriteLine("2. Открыть справочник");
            Console.WriteLine("3. Управление записями");
            Console.WriteLine("4. Выйти из системы");
            int choise = Convert.ToInt16(Console.ReadLine());
            switch(choise){
                case 1:
                    
                    break;
                case 2:
                    librarySystem.LibMenu();
                    break;
                case 3:

                    break;
                case 4:
                    return;
                default:
                    break;
            }
        }
    }
}

