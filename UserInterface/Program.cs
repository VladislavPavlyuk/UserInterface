using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/*Разработать консольное приложение, состоящее из двух классов:
- класс UserInterface предоставляет пользователю возможность изменять настройки окна приложения (цвет фона и текста консоли, размер окна, заголовок и др.);
- класс ApplicationSettingsHelper хранит настройки окна приложения, а также позволяет их считывать из файла и записывать в файл.*/

namespace UserInterface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
            
            ConsoleColor currentBackground = Console.BackgroundColor;

            ConsoleColor currentForeground = Console.ForegroundColor;
               
            Console.WriteLine("All the foreground colors except {0}, the background color:", currentBackground);

            foreach (var color in colors)
            {
                if (color == currentBackground) continue;
                Console.ForegroundColor = color;
                Console.WriteLine("   The foreground color is {0}.", color);
            }
            Console.WriteLine();
            
            Console.ForegroundColor = currentForeground;

            Console.WriteLine("All the background colors except {0}, the foreground color:", currentForeground);
            foreach (var color in colors)
            {
                if (color == currentForeground) continue;
                Console.BackgroundColor = color;
                Console.WriteLine("   The background color is {0}.", color);
            }

            Console.ResetColor();
            Console.WriteLine("\nOriginal colors restored...");
            Console.ReadKey();*/


            ApplicationSettingsHelper obj = new ApplicationSettingsHelper();

            string[] items =
                {
                "Добавить настройки в список", //0
                "Удалить настройки", //1
                "Изменить настройки", //2
                "Вывести на экран", //3 
                "Сохранить настройки", //4
                "Загрузить настройки", //5
                "Выход"                //6
            };

            Menu menu = new Menu(items);

            int menuResult;
            do
            {
                Console.WriteLine("\tНастройки");

                menuResult = menu.PrintMenu();

                switch (menuResult)
                {
                    case 0:
                        Add(obj); //Добавить настройки
                        break;
                    case 1:
                        Del(obj); //Удалить настройки
                        break;
                    case 2:
                        Edit(obj); //Изменить настройки
                        break;
                    case 3:
                        Print(obj); //Вывести на экран
                        break;
                    case 4:
                        Save(obj); //Сохранить настройки в Binary файл
                        break;
                    case 5:
                        Load(obj); //Загрузить настройки из файла
                        break;
                    case 6:
                        Exit(); //выход
                        break;
                    default:
                        break;
                }

                Console.WriteLine("Для продолжения нажмите любую клавишу");
                Console.ReadKey();
            } while (menuResult != items.Length - 1);

            Console.Clear();
        }

        static void Add(ApplicationSettingsHelper obj)          // Добавить настройки
        {
            try
            {
                obj.Add();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void Del(ApplicationSettingsHelper obj)          // Удаление настроек                               
        {
            try
            {
                obj.Remove();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void Edit(ApplicationSettingsHelper obj)         // редактирование настроек
        {
            try
            {
                obj.Edit();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void Print(ApplicationSettingsHelper obj)        // Печать настроек
        {
            try
            {
                obj.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка!", e.ToString());
            }
        }
        static void Save(ApplicationSettingsHelper obj)        // сохранение в файл
        {
            try
            {
                SaveBinary(obj);

                Console.Clear();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void SaveBinary(ApplicationSettingsHelper obj)  // сохранение в Binary файл
        {
            try
            {
                obj.SaveBinary();
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void Load(ApplicationSettingsHelper obj)     // загрузка группы из Binary файла
        {
            try
            {
                obj.LoadBinary();            
            }
            catch (Exception)
            {
                Console.WriteLine("Ошибка!");
            }
        }
        static void Exit()
        {
            Console.WriteLine("Приложение заканчивает работу!");
        }
    }
}
