﻿// Программа для работы с базой данных посёлков
//               Вариант №20
//     Выполнил Сергеев Кирилл Дмитриевич 
//                Группа 206

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Shell;
using Correct_Input;
using TUI;

namespace DataBase
{
    public class IntMenu
    {
           // Вывести назначение программы

           ConsoleColor col = ConsoleColor.Green;
        public IntMenu()
        {
            Console.CursorVisible = false;
        }
        /// <summary>
        /// Выводит экранное меню
        /// </summary>
        public void MainMenu()
        {
            Console.CursorVisible = false;
            Frames frame = new Frames();
            Input inp = new Input();
            Tables table = new Tables();

            ConsoleKey? mKey = null;
            int main_x = 30, main_y = 3;
            bool canСontinue = true;

            frame.Menu(30, 3, 30, "Ввод базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
            frame.Choice(main_x, main_y, col, 30);
            while (canСontinue)
            {
                mKey = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter);

                if ((mKey == ConsoleKey.DownArrow) & (main_y != 12))
                {
                    frame.Choice(main_x, main_y, ConsoleColor.White, 30);
                    main_y += 3;
                    frame.Choice(main_x, main_y, col, 30);
                }
                else if ((mKey == ConsoleKey.UpArrow) & (main_y > 3))
                {
                    frame.Choice(main_x, main_y, ConsoleColor.White, 30);
                    main_y -= 3;
                    frame.Choice(main_x, main_y, col, 30);
                }
                else if (mKey == ConsoleKey.Enter)
                {
                    ConsoleKey? sKey = null;
                    int x = 30, y = 3;
                    switch (main_y)
                    {
                        // Выбор файла для хранения данных
                        case 9: 
                            Console.Clear();
                            frame.Menu(30, 3, 30, "Выброр существующего файла", "Создание нового файла", "Удаление файла");
                            ConsoleKey? key = null;
                            y = 3;
                            frame.Choice(30, y, ConsoleColor.Green, 30);
                            while (key != ConsoleKey.Escape)
                            {
                                key = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);
                                switch (key)
                                {
                                    case ConsoleKey.DownArrow:
                                        if(y != 9)
                                        {
                                            frame.Choice(30, y, ConsoleColor.White, 30);
                                            y += 3;
                                            frame.Choice(30, y, ConsoleColor.Green, 30);
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        if (y != 3)
                                        {
                                            frame.Choice(30, y, ConsoleColor.White, 30);
                                            y -= 3;
                                            frame.Choice(30, y, ConsoleColor.Green, 30);
                                        } 
                                        break;
                                    case ConsoleKey.Enter:
                                        switch (y)
                                        {
                                            case 3:
                                                Console.Clear();
                                                table.Write_FileList();
                                                break;
                                            case 6:
                                                Console.Clear();
                                                table.Create_File();
                                                break;
                                            case 9:
                                                Console.Clear();
                                                table.Write_FileList(true);
                                                break;
                                        }
                                        Console.Clear();
                                        frame.Menu(30, 3, 30, "Выброр существующего файла", "Создание нового файла","Удаление файла");
                                        frame.Choice(30, y, ConsoleColor.Green, 30);
                                        break;
                                }
                            }
                            
                            Console.Clear();
                            frame.Menu(30, 3, 30, "Ввод базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
                            frame.Choice(main_x, main_y, col, 30);
                            break;

                        // Выход из программы
                        case 12: 
                            canСontinue = false;
                            break;

                        default:
                            Console.Clear();
                            frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                            frame.Choice(x, y, col, 18);
                            while (sKey != ConsoleKey.Escape)
                            {
                                sKey = inp.InputKey(ConsoleKey.DownArrow, ConsoleKey.UpArrow, ConsoleKey.Enter, ConsoleKey.Escape);

                                switch (sKey)
                                {
                                    case ConsoleKey.DownArrow:
                                        if (y != 9)
                                        {
                                            frame.Choice(x, y, ConsoleColor.White, 18);
                                            y += 3;
                                            frame.Choice(x, y, col, 18);
                                        }
                                        break;
                                    case ConsoleKey.UpArrow:
                                        if (y > 3)
                                        {
                                            frame.Choice(x, y, ConsoleColor.White, 18);
                                            y -= 3;
                                            frame.Choice(x, y, col, 18);
                                        }
                                        break;
                                    case ConsoleKey.Enter:
                                        try
                                        {
                                            switch (y)
                                            {
                                                case 3:
                                                    if (main_y == 3)
                                                        table.ReadVellage();
                                                    else if (main_y == 6)
                                                        table.WriteVillage();
                                                    break;
                                                case 6:
                                                    if (main_y == 3)
                                                        table.ReadHouse();
                                                    else if (main_y == 6)
                                                        table.WriteHouse();
                                                    break;
                                                case 9:
                                                    if (main_y == 3)
                                                        table.ReadDeveloper();
                                                    else if (main_y == 6)
                                                        table.WriteDeveloper();
                                                    break;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            ConsoleKey back;
                                            //if(table.fileName == "")
                                            Console.Clear();
                                            //frame.Menu(30, 5, 31, e);
                                            throw;
                                            back = inp.InputKey(ConsoleKey.Escape);
                                            if (back == ConsoleKey.Escape)
                                            {
                                                Console.Clear();
                                                frame.Menu(30, 3, 18, "Таблица посёлков", "Таблица домов", "Таблица девелоперов");
                                            }
                                            //else

                                        }
                                        //y = 0;
                                        frame.Choice(x, y, col, 18);

                                        break;
                                    case ConsoleKey.Escape:
                                        Console.Clear();
                                        frame.Menu(30, 3, 30, "Ввод базы данных", "Вывод базы данных", "Выбор файла", "Выход из программы");
                                        //main_y = 3;
                                        frame.Choice(main_x, main_y, col, 30);
                                        break;
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}