using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Converter
{
    public class SaveIt
    {
        public void Choose(string name)
        {
            List<homework> HWork = new List<homework>();
            string NewPath;
            bool TF = true;
            while (TF)
            {
                if (name.Contains(".xml"))
                {
                    using (FileStream filestream = new FileStream(name, FileMode.Open))
                    {
                        var xmlser = new XmlSerializer(typeof(List<homework>));
                        HWork = (List<homework>)xmlser.Deserialize(filestream);
                    }
                    TF = !TF;
                }
                else if (name.Contains(".json"))
                {
                    HWork = JsonConvert.DeserializeObject<List<homework>>(name);
                    TF = !TF;
                }
                else if (name.Contains(".txt"))
                {
                    string[] lines = File.ReadAllLines(name);
                    for (int i = 0; i < lines.Length; i += 3)
                    {
                        HWork.Add(new homework(
                            lines[i],
                            Convert.ToInt16(lines[i + 1]),
                            Convert.ToInt16(lines[i + 2])));
                    }
                    TF = !TF;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Введён некоректный адрес файла\nПовторите попытку: ");
                    name = Console.ReadLine();
                }
            }
            foreach (var item in HWork)
            {
                Console.WriteLine(item.Fname);
                Console.WriteLine(item.Num);
                Console.WriteLine(item.Mark);
            }
            ConsoleKey kluch; bool BOOL = true;
            do
            {
                kluch = Console.ReadKey().Key;
                if (kluch == ConsoleKey.Escape)
                {
                    BOOL = false;
                }
                else if (kluch == ConsoleKey.F1)
                {
                    BOOL = false;
                }
            } while (BOOL);
            if (kluch == ConsoleKey.Escape)
            {
                Console.Write("Вы завершили программу без сохранения!\nВсего хорошего)");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Введите путь до файла вместе с названием.");
                NewPath = Console.ReadLine();
                if (NewPath.Contains(".txt"))
                {
                    foreach (var item in HWork)
                    {
                        if (HWork.IndexOf(item) == 0)
                        {
                            File.WriteAllText(NewPath, $"{item.Fname}\n{item.Num}\n{item.Mark}");
                        }
                        else
                        {
                            File.AppendAllText(NewPath, $"\n{item.Fname}\n{item.Num}\n{item.Mark}");
                        }
                    }
                }
                else if (NewPath.Contains(".xml"))
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<homework>));
                    using (FileStream fileStream = new FileStream(NewPath, FileMode.OpenOrCreate))
                    {
                        xml.Serialize(fileStream, HWork);
                    }
                }
                else if (NewPath.Contains(".json"))
                {
                    string json = JsonConvert.SerializeObject(HWork);
                    File.WriteAllText(NewPath, json);
                }
                Console.WriteLine("Конвертация прошла успешно!\nВсего доброго)");
            }
        }
    }
}
