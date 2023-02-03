using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Converter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SaveIt style = new SaveIt();
            Console.WriteLine("Введите адрес файла для чтения: ");
            string address = Console.ReadLine();
            style.Choose(address);
        }
    }
}