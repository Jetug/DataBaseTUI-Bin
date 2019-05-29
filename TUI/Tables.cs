// Программа для работы с базой данных посёлков
//                Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Xml;
using Shell;
using Correct_Input;
using System.IO;
using DataBaseLogic;

namespace TUI
{
    public class Tables
    {
        public List<Village> Search(string name)
        {
            List<Village> sortedList = new List<Village>();

            foreach (Village vill in BinFile.villages)
            {
                if (vill.name == name)
                {
                    sortedList.Add(vill);
                }
            }
            return sortedList;
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о посёлке 
        /// </summary>
        public void ReadVellage()
        {
            Console.Clear();
            BinFile file = new BinFile();
            file.Load_DataBase();
            Frames frame = new Frames();
            if (BinFile.developers.Count == 0)
            {
                Console.SetCursorPosition(14, 6);
                frame.Continuous(50, "Ошибка!", "Необходимо сначала заполнить таблицу девелоперов");
                Console.ReadKey(true);
            }
            else
            {
                //file.Load_DataBase();
                Input inp = new Input();
                List<Village> _villages = new List<Village>();
                bool canСontinue = true;

                Console.WriteLine("╔════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
                Console.WriteLine("║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
                Console.WriteLine("╠════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");
                Console.WriteLine("║                        │                       │               │           ║");
                Console.Write    ("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
                
                while (canСontinue)
                {
                    Village vill = new Village("", "", 0, 0);
                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.CursorVisible = true;
                    if (inp.ReadValid(ref vill.name, 22))
                    {
                        Console.SetCursorPosition(27, Console.CursorTop);
                        Console.CursorVisible = false;
                        vill.dev = ChoiceDeveloper(Console.CursorTop);
                        Console.CursorVisible = true;
                        Console.SetCursorPosition(51, Console.CursorTop);
                        inp.ReadValid(ref vill.area, 13);
                        Console.SetCursorPosition(67, Console.CursorTop);
                        inp.ReadValid(ref vill.people, 9);
                        Console.CursorVisible = false;
                        Console.SetCursorPosition(0, Console.CursorTop + 1);
                        Console.WriteLine("╟────────────────────────┼───────────────────────┼───────────────┼───────────╢");
                        Console.WriteLine("║                        │                       │               │           ║");
                        Console.Write    ("╚════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
                        _villages.Add(vill);
                    }
                    else
                    {
                        canСontinue = false;
                        if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                        {
                            file.SaveInFile(_villages, FileMode.Append);
                        }
                    }
                }
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о домах
        /// </summary>
        public void ReadHouse()
        {
            BinFile file = new BinFile();
            file.Load_DataBase();
            Input inp = new Input();
            Frames frame = new Frames();
            List<House> _houses = new List<House>();
            bool canСontinue = true;

            Console.Clear();
            Console.WriteLine("╔════════════════════════╤════════════╤═══════════════╤═══════════════╤══════════════════════╗");
            Console.WriteLine("║    Назвение посёлка    │ Номер дома │ Площадь в м^2 │ Кол-во этажей │       Тип дома       ║");
            Console.WriteLine("╠════════════════════════╪════════════╪═══════════════╪═══════════════╪══════════════════════╣");
            Console.WriteLine("║                        │            │               │               │                      ║");
            Console.Write    ("╚════════════════════════╧════════════╧═══════════════╧═══════════════╧══════════════════════╝");
           
            while (canСontinue)
            {
                House house = new House("", 0, 0, 0, "");
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref house.name, 22))
                {
                    Console.SetCursorPosition(27, Console.CursorTop);
                    inp.ReadValid(ref house.num, 10);
                    Console.SetCursorPosition(40, Console.CursorTop);
                    inp.ReadValid(ref house.area, 13);
                    Console.SetCursorPosition(56, Console.CursorTop);
                    inp.ReadValid(ref house.floor, 13);
                    Console.SetCursorPosition(72, Console.CursorTop);
                    inp.ReadValid(ref house.type, 20);
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    Console.WriteLine("╟────────────────────────┼────────────┼───────────────┼───────────────┼──────────────────────╢");
                    Console.WriteLine("║                        │            │               │               │                      ║");
                    Console.Write    ("╚════════════════════════╧════════════╧═══════════════╧═══════════════╧══════════════════════╝");
                    _houses.Add(house);

                }
                else
                {
                    canСontinue = false;

                    if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                    {
                        file.SaveInFile(_houses, FileMode.Append);
                    }
                }

            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу, внутри которой считывает данные о девелоперах
        /// </summary>
        public void ReadDeveloper()
        {
            BinFile file = new BinFile();
            file.Load_DataBase();
            Input inp = new Input();
            Frames frame = new Frames();
            List<Developer> _developers = new List<Developer>();
            bool canСontinue = true;

            Console.Clear();
            Console.WriteLine("╔═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine("║       Девелопер       │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine("╠═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            Console.WriteLine("║                       │               │                                  ║");
            Console.Write    ("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            while (canСontinue)
            {
                Developer dev = new Developer("", 0, "");
                Console.SetCursorPosition(2, Console.CursorTop - 1);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref dev.name, 21))
                {
                    Console.SetCursorPosition(26, Console.CursorTop);
                    inp.ReadValid(ref dev.inc, 13);
                    Console.SetCursorPosition(42, Console.CursorTop);
                    inp.ReadValid(ref dev.addr, 32);
                    Console.CursorVisible = false;
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                    Console.WriteLine("╟───────────────────────┼───────────────┼──────────────────────────────────╢");
                    Console.WriteLine("║                       │               │                                  ║");
                    Console.Write("╚═══════════════════════╧═══════════════╧══════════════════════════════════╝");
                    _developers.Add(dev);
                }
                else
                {
                    canСontinue = false;
                    if (frame.Call_MassageBox(30, 10, "Cозранить изменения?"))
                    {
                        file.SaveInFile(_developers, FileMode.Append);
                    }
                }
            }
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о посёлках
        /// </summary>
        public void WriteVillage()
        {
            //List<Village> villages = new List<Village>();
            BinFile file = new BinFile();
            file.Load_DataBase();
            Frames frame = new Frames();

            if (BinFile.villages.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);
            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, BinFile.villages);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < BinFile.villages.Count))
                    {
                        index += 10;
                        Write_Page(index, BinFile.villages);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref BinFile.villages, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                file.SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о домах
        /// </summary>
        public void WriteHouse()
        {
            BinFile file = new BinFile();
            //List<House> houses = new List<House>();
            file.Load_DataBase();
            Frames frame = new Frames();

            if (BinFile.houses.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);
            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, BinFile.houses);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < BinFile.houses.Count))
                    {
                        index += 10;
                        Write_Page(index, BinFile.houses);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref BinFile.houses, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                file.SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        /// <summary>
        /// Выводит таблицу с данными о девелоперах
        /// </summary>
        public void WriteDeveloper()
        {
            BinFile file = new BinFile();
            file.Load_DataBase();
            Frames frame = new Frames();

            if (BinFile.developers.Count() == 0)
            {
                Console.Clear();
                frame.Menu(35, 6, 12, "Файл пуст!");
                Console.ReadKey(true);
            }
            else
            {
                Input inp = new Input();
                ConsoleKey? key = ConsoleKey.RightArrow;
                int index = -10;
                while (key != ConsoleKey.Escape)
                {
                    if ((key == ConsoleKey.LeftArrow) && (index != 0))
                    {
                        index -= 10;
                        Write_Page(index, BinFile.developers);
                    }
                    else if ((key == ConsoleKey.RightArrow) && (index + 10 < BinFile.developers.Count))
                    {
                        index += 10;
                        Write_Page(index, BinFile.developers);
                    }
                    else if (key == ConsoleKey.Enter)
                        Choice(ref BinFile.developers, index, 7, 32, 56, 72);

                    key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                }
            }
            if (frame.Call_MassageBox(30, 6, "Сохранить изменения?"))
                file.SaveAll();
            Console.Clear();
            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
        }

        //private List<Village> Load_VillageBase()
        //{
        //    try
        //    {
        //        List<Village> villages = new List<Village>();
        //        XmlDocument xVill = new XmlDocument();
        //        xVill.Load($"C:/C#/RunDll/XMLfiles/{fileName}.xml");
        //        XmlElement villRoot = xVill.DocumentElement;

        //        foreach (XmlElement xnode in villRoot)
        //        {
        //            if (xnode.Name == "village")
        //            {
        //                Village vill = new Village();
        //                //XmlNode attr = xnode.Attributes.GetNamedItem("number");
        //                //if (attr != null)
        //                //    vill.number = UInt32.Parse(attr.Value);

        //                foreach (XmlNode childnode in xnode.ChildNodes)
        //                {
        //                    switch (childnode.Name)
        //                    {
        //                        case "name":
        //                            vill.name = childnode.InnerText;
        //                            break;
        //                        case "dev":
        //                            vill.dev = childnode.InnerText;
        //                            break;
        //                        case "area":
        //                            vill.area = float.Parse(childnode.InnerText);
        //                            break;
        //                        case "people":
        //                            vill.people = UInt32.Parse(childnode.InnerText);
        //                            break;
        //                    }
        //                }
        //                villages.Add(vill);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        villages = null;
        //    }
        //    return villages;
        //}


        private void Choice(ref List<Village> list, int page, params byte[] coordinates)
        {
            coordinates.ToList();
            Console.CursorVisible = true;
            int x = 0, y = 3;
            int i = page;
            Console.SetCursorPosition(coordinates[x], y);
            Input inp = new Input();
            ConsoleKey? key = null;
            while ((key != ConsoleKey.Escape) && (key != ConsoleKey.Delete))
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Delete, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= coordinates.Count() - 1)
                            x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= 0)
                            x--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= list.Count*2)
                        {
                            y += 2;
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= 3)
                        {
                            y -= 2;
                            i--;
                        }
                        break;
                    case ConsoleKey.Delete:
                        list.RemoveAt(i);
                        Write_Page(page, list);                       
                        break;
                    //case ConsoleKey.Enter:

                    //    break;
                }
                Console.SetCursorPosition(coordinates[x], y);
            }
            Console.CursorVisible = false;
        }

        private void Choice(ref List<House> list, int page, params byte[] coordinates)
        {
            coordinates.ToList();
            Console.CursorVisible = true;
            int x = 0, y = 3;
            int i = page;
            Console.SetCursorPosition(coordinates[x], y);
            Input inp = new Input();
            ConsoleKey? key = null;
            while ((key != ConsoleKey.Escape) && (key != ConsoleKey.Delete))
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Delete, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= coordinates.Count() - 1)
                            x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= 0)
                            x--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= list.Count * 2)
                        {
                            y += 2;
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= 3)
                        {
                            y -= 2;
                            i--;
                        }
                        break;
                    case ConsoleKey.Delete:
                        list.RemoveAt(i);
                        Write_Page(page, list);
                        break;
                        //case ConsoleKey.Enter:

                        //    break;
                }
                Console.SetCursorPosition(coordinates[x], y);
            }
            Console.CursorVisible = false;
        }

        private void Choice(ref List<Developer> list, int page, params byte[] coordinates)
        {
            coordinates.ToList();
            Console.CursorVisible = true;
            int x = 0, y = 3;
            int i = page;
            Console.SetCursorPosition(coordinates[x], y);
            Input inp = new Input();
            ConsoleKey? key = null;
            while ((key != ConsoleKey.Escape) && (key != ConsoleKey.Delete))
            {
                key = inp.InputKey(ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Delete, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= coordinates.Count() - 1)
                            x++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= 0)
                            x--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= list.Count * 2)
                        {
                            y += 2;
                            i++;
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= 3)
                        {
                            y -= 2;
                            i--;
                        }
                        break;
                    case ConsoleKey.Delete:
                        list.RemoveAt(i);
                        Write_Page(page, list);
                        break;
                        //case ConsoleKey.Enter:

                        //    break;
                }
                Console.SetCursorPosition(coordinates[x], y);
            }
            Console.CursorVisible = false;
        }


        private void Write_Page(int index, List<Village> villages)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦════════════════════════╤═══════════════════════╤═══════════════╤═══════════╗");
            Console.WriteLine("║  №  ║    Назвение посёлка    │       Девелопер       │ Площадь в м^2 │ Население ║");
            Console.WriteLine("╠═════╬════════════════════════╪═══════════════════════╪═══════════════╪═══════════╣");

            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < villages.Count())
                {
                    Console.WriteLine("║     ║                        │                       │               │           ║");
                    Console.Write("╟─────╫────────────────────────┼───────────────────────┼───────────────┼───────────╢");
                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(index + 1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(villages[index].name);
                    Console.SetCursorPosition(33, Console.CursorTop);
                    Console.Write(villages[index].dev);
                    Console.SetCursorPosition(57, Console.CursorTop);
                    Console.Write(villages[index].area);
                    Console.SetCursorPosition(73, Console.CursorTop);
                    Console.WriteLine(villages[index].people);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩════════════════════════╧═══════════════════════╧═══════════════╧═══════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = villages.Count() % 10 == 0 ? (villages.Count() / 10) : (villages.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private void Write_Page(int index, List<House> houses)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦════════════════════╤════════════╤═══════════════╤═══════════════╤════════════════╗");
            Console.WriteLine("║  №  ║  Назвение посёлка  │ Номер дома │ Площадь в м^2 │ Кол-во этажей │    Тип дома    ║");
            Console.WriteLine("╠═════╬════════════════════╪════════════╪═══════════════╪═══════════════╪════════════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < houses.Count())
                {
                    Console.WriteLine("║     ║                    │            │               │               │                ║");
                    Console.Write("╟─────╫────────────────────┼────────────┼───────────────┼───────────────┼────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(index + 1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(houses[index].name);
                    Console.SetCursorPosition(29, Console.CursorTop);
                    Console.Write(houses[index].num);
                    Console.SetCursorPosition(42, Console.CursorTop);
                    Console.Write(houses[index].area);
                    Console.SetCursorPosition(58, Console.CursorTop);
                    Console.Write(houses[index].floor);
                    Console.SetCursorPosition(74, Console.CursorTop);
                    Console.WriteLine(houses[index].type);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩════════════════════╧════════════╧═══════════════╧═══════════════╧════════════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = houses.Count() % 10 == 0 ? (houses.Count() / 10) : (houses.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private void Write_Page(int index, List<Developer> developers)
        {
            Console.Clear();
            Console.WriteLine("╔═════╦═══════════════════════╤═══════════════╤══════════════════════════════════╗");
            Console.WriteLine("║  №  ║     Девелопер         │ Годовой доход │         Адрес девелопера         ║");
            Console.WriteLine("╠═════╬═══════════════════════╪═══════════════╪══════════════════════════════════╣");
            for (byte i = 0; i < 10; i++, index++)
            {
                if (index < developers.Count())
                {

                    Console.WriteLine("║     ║                       │               │                                  ║");
                    Console.Write    ("╟─────╫───────────────────────┼───────────────┼──────────────────────────────────╢");

                    Console.SetCursorPosition(2, Console.CursorTop - 1);
                    Console.Write(index + 1);
                    Console.SetCursorPosition(8, Console.CursorTop);
                    Console.Write(developers[index].name);
                    Console.SetCursorPosition(32, Console.CursorTop);
                    Console.Write(developers[index].inc);
                    Console.SetCursorPosition(48, Console.CursorTop);
                    Console.WriteLine(developers[index].addr);
                    Console.SetCursorPosition(0, Console.CursorTop + 1);
                }
            }
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            Console.WriteLine("╚═════╩═══════════════════════╧═══════════════╧══════════════════════════════════╝");
            Frames frame = new Frames();
            int lastPage;
            lastPage = developers.Count() % 10 == 0 ? (developers.Count() / 10) : (developers.Count() / 10 + 1);
            int x = Console.CursorLeft, y = Console.CursorTop;
            frame.Menu(x, y, 20, "Стр. " + index / 10 + " из " + lastPage);
        }

        private string ChoiceDeveloper(int readPosY)
        {
            BinFile file = new BinFile();
            Console.SetCursorPosition(80, 0);
            List<string> developerNames = file.GetDevNames();
            Frames frame = new Frames();
            frame.Continuous(25, "Выберите девелопера", developerNames.ToArray());
            ushort x = 80;
            ushort y = 2;
            int i = 0;
            frame.Choice(x, y, ConsoleColor.Green, 25);
            Input inp = new Input();
            ConsoleKey? key = null;
            while (key != ConsoleKey.Enter)
            {
                key = inp.InputKey(ConsoleKey.Escape, ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter);
                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        if ((y / 2) < developerNames.Count - 1)
                        {
                            i++;
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y += 2;
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if (y == 4)
                            {
                                Console.SetCursorPosition(x, 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╠{line}╣");
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if ((y / 2) >= 2)
                        {
                            i--;
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 25);
                            y -= 2;
                            //Console.WriteLine(y);
                            //Console.WriteLine(developers.Count);
                            frame.Choice(x, y, ConsoleColor.Green, 25);
                            if ((y / 2) == developerNames.Count - 2)
                            {
                                Console.SetCursorPosition(x, (developerNames.Count) * 2);
                                string line = new string('═', 25);
                                Console.WriteLine($"╚{line}╝");
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        Console.SetCursorPosition(27, readPosY);
                        Console.Write(developerNames[i]);
                        break;
                }
            }
            return developerNames[i];
        }

        /// <summary>
        /// Создаёт новый файл с введённым пользователем именем
        /// </summary>
        public void Create_File()
        {
            Frames frame = new Frames();
            BinFile file = new BinFile();
            Console.SetCursorPosition(30, 9);
            frame.Continuous(34, "Введите имя файла", "");
            Input inp = new Input();
            bool canСontinue = true;
            while (canСontinue)
            {
                Console.SetCursorPosition(32, 12);
                Console.CursorVisible = true;
                if (inp.ReadValid(ref BinFile.fileName, 31))
                {
                    if (!File.Exists($"C:/C#/RunDll/XMLfiles/{BinFile.fileName}.xml"))
                    {
                        Console.CursorVisible = false;
                        try
                        {
                            file.CreateFile(BinFile.fileName);
                            canСontinue = false;
                        }
                        catch (Exception)
                        {
                            Console.CursorVisible = false;
                            string clear = new string(' ', 32);
                            Console.SetCursorPosition(32, 12);
                            Console.Write(clear);
                            Console.SetCursorPosition(32, 12);
                            Console.Write("Недопустимые знаки в имени файла");
                            Thread.Sleep(600);
                            Console.SetCursorPosition(32, 12);
                            Console.Write(clear);
                        }
                    }
                    else
                    {
                        Console.CursorVisible = false;
                        string clear = new string(' ', 28);
                        Console.SetCursorPosition(32, 12);
                        Console.Write(clear);
                        Console.SetCursorPosition(32, 12);
                        Console.Write("Файл уже существует");
                        Thread.Sleep(600);
                        Console.SetCursorPosition(32, 12);
                        Console.Write(clear);
                    }
                }
                else
                    canСontinue = false;
            }
        }

        /// <summary>
        /// Выводит список существующих файлов
        /// </summary>
        public void Write_FileList(bool del = false)
        {
            BinFile file = new BinFile();
            List<string> files = file.Load_FileList();
            Console.SetCursorPosition(30, 3);
            Frames frame = new Frames();
            Input inp = new Input();

            frame.Continuous(30, "Выберете файл", files.ToArray());
            int y = 5;
            int x = 30;
            int i = 0;
            ConsoleKey? key = null;
            frame.Choice(x, y, ConsoleColor.Green, 30);
            while ((key != ConsoleKey.Enter) && (key != ConsoleKey.Escape))
            {
                key = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                switch (key)
                {
                    case ConsoleKey.DownArrow:

                        if ((y / 2) < files.Count + 1)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 30);
                            y += 2;
                            ++i;
                            frame.Choice(x, y, ConsoleColor.Green, 30);
                            if (y == 7)
                            {
                                Console.SetCursorPosition(x, 5);
                                string line = new string('═', 30);
                                Console.WriteLine($"╠{line}╣");
                            }
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (y != 5)
                        {
                            frame.ContinuousChoice(x, y, ConsoleColor.White, 30);
                            y -= 2;
                            --i;
                            frame.Choice(x, y, ConsoleColor.Green, 30);

                            if ((y / 2) == files.Count)
                            {
                                Console.SetCursorPosition(x, (files.Count + 1) * 2 + 3);
                                string line = new string('═', 30);
                                Console.WriteLine($"╚{line}╝");
                            }
                        }
                        break;
                    case ConsoleKey.Enter:
                        if ((del) && (frame.Call_MassageBox(30, 3, "Удалить файл?")))
                        {
                            File.Delete($"C:/C#/RunDll/XMLfiles/{files[i]}.xml");
                        }
                        else
                            BinFile.fileName = files[i];
                        break;
                }
            }
        }
    }
}