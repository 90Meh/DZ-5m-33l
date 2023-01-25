using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

internal class Program
{
    private static void Main(string[] args)
    {

        //Коллекция книг
        ConcurrentDictionary<string, int> bookCollection = new ConcurrentDictionary<string, int>();

        // Поток для вычисления процентов
        Task task = Task.Run(() => { PrecentBook(bookCollection); });
        
        //Главный цикл
        var start = true;
        do
        {
            Console.WriteLine("1 - добавить книгу|||2 - вывести список непрочитанного|||3 - выйти");

            
            var operB = int.TryParse(Console.ReadLine(), out int oper);
            if (operB)
            {
                Console.Clear();
                switch (oper)
                {
                    case 1:
                        Console.WriteLine("Введите название книги:");
                        AddBook(Console.ReadLine(), bookCollection);
                        break;
                    case 2:
                        ShowBooks(bookCollection);
                        break;
                    case 3:
                        start = false;
                        break;
                    default:
                        break;
                }
            }
           
        } while (start);

        
    }

    //Методы коллекции
    public static void AddBook(string bookName, ConcurrentDictionary<string, int> bookCollection)
    {
        foreach (var item in bookCollection)
        {
            if (item.Key == bookName)
            {
                Console.WriteLine("Книга уже есть!");
                break;
            }
        }
        bookCollection.TryAdd(bookName, 0);
    }

    public static void ShowBooks(ConcurrentDictionary<string,int> bookCollection)
    {
        foreach (var item in bookCollection)
        {
            Console.WriteLine($"Название книги {item.Key} || Книга прочитана на {item.Value} процентов.");
        }
    }

    //Метод расчёта процентов
        public static void PrecentBook(ConcurrentDictionary<string, int> bookCollection)
    {
        while (true)
        {
            foreach (var item in bookCollection)
            {
                Thread.Sleep(100);
                bookCollection[item.Key] = item.Value+1;
                if (item.Value == 100)
                {
                    if (bookCollection.TryRemove(item.Key, out int retrievedValue))
                    {
                        Console.WriteLine($"Книга {item.Key} удалена");
                    }
                }
            }
        }
        
    }

}