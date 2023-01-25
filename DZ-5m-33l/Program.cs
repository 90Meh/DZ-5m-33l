
using System.Collections.ObjectModel;
using System.Collections.Specialized;


internal class Program
{
    private static void Main(string[] args)
    {
        //Создание объектов классов и подписка на событие
        Shop shop = new Shop();
        Customer customer = new Customer();
        shop.product.CollectionChanged += customer.OnItemChanged;

        ///////////
        ///Цикл выполнения
        bool start = true;
        do
        {
            Console.WriteLine("По нажатии клавиши A добавляется новый товар в магазин");
            Console.WriteLine("По нажатии клавиши D удаление товара");
            Console.WriteLine("X выход из программы");

            string action = Console.ReadLine().ToUpper();
            switch (action)
            {
                case "A":
                    shop.Add();
                    break;
                case "D":
                    //Надеюсь пользователь не дурак)
                    Console.WriteLine("Введите индекс");
                    shop.Remove(Int32.Parse(Console.ReadLine()));                    
                    break;
                case "X":
                    start = false;
                    break;
                default:
                    Console.WriteLine("Ты чё дурак?");
                    break;
            }
            foreach (var item in shop.product)
            {
                Shop.Item.Print(item);
            }



        } while (start);

        

    }

   


    //Класс магазин
    public class Shop
    {
        //Коллекция товаров
       public ObservableCollection<Item> product = new ObservableCollection<Item>();
        //Счётчик для индексов
        int counter = 1;

        //Класс Товар
        public class Item
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public static void Print(Item a)
            {
                Console.WriteLine(a.Name);
                Console.WriteLine(a.Id);

            }
        }

        //Методы класса
        public void Add()
        {
            product.Add(new Item { Id = counter, Name = $"Товар от {DateTime.Now}" });
            counter++;
        }
        public void Remove(int Id)
        {
            foreach (var item in product)
            {
                if (item.Id == Id)
                {
                    product.Remove(item);
                    counter--;
                    break;
                }
            }
        }
    }

    //Класс покупатель
    public class Customer
    {
        public void OnItemChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    Console.WriteLine("Добавлен элемент");
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Console.WriteLine("Удалён элемент");
                    break;
                default:
                    break;
            }

        }
    }

    
}