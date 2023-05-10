using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;

namespace UserInterface
{
    [Serializable]

    public class ApplicationSettingsHelper : ICloneable, IEnumerator, IEnumerable
    {
        ArrayList ag;

        int position = -1;

        FileStream stream = null;

        BinaryFormatter formatter = null;

        object[] temp = null;

        public ApplicationSettingsHelper()          // конструктор по умолчанию
        {
            ag = new ArrayList();
        }

        public void Add()
        {
            try
            {
                Console.WriteLine("\nПожалуйста введите цвет фона (0 - 15 : ");
                Console.Write("\nФон :\t\t");
                UInt16 backGroundColor = UInt16.Parse(Console.ReadLine());

                Console.Write("\nПожалуйста введите цвет текста (0 - 15) : \t");
                UInt16 foreGroundColor = UInt16.Parse(Console.ReadLine());

                Console.Write("\nШирина окна :\t");
                UInt16 width = UInt16.Parse(Console.ReadLine());

                Console.Write("\nВысота окна :\t");
                UInt16 height = UInt16.Parse(Console.ReadLine());

                Console.Write("\nЗаголовок :\t");
                String title = Console.ReadLine();
                
                Settings st = new Settings(backGroundColor, foreGroundColor, width, height, title);
                ag.Add(st);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }

        public void Remove()
        {
            try
            {
                Console.WriteLine("\nПожалуйста введите название настроек для удаления : ");
                Console.WriteLine("\nНазвание :");
                String title = Console.ReadLine();
                int index = -1;
                for (int i = 0; i < ag.Count; i++)
                {
                    if ((ag[i] as Settings).Title == title)
                        index = i;
                }
                if (index == -1)
                    throw new Exception("Настройки c таким названием отсутствуют в списке");
                else
                    ag.RemoveAt(index);
                Console.WriteLine("Настройки {0} удалены из списка", title);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }


        public void Edit()
        {
            try
            {
                Console.WriteLine("\nПожалуйста введите название настроек для редактирования : ");
                Console.WriteLine("\nНазвание :");
                System.String _title = Console.ReadLine();
                bool flag = false;

                for (int i = 0; i < ag.Count; i++)
                {
                    if ((ag[i] as Settings).Title == _title)
                    {
                        flag = true;

                        (ag[i] as Settings).Print();

                        Console.WriteLine("Какие настройки окна Вы хотели бы изменить?\nНажмите любую кнопку для продолжения...");
                        Console.ReadKey();

                        string[] items =
                        {
                            "Цвет фона",
                            "Цвет текста",
                            "Ширина окна",
                            "Высота окна",
                            "Заголовок",
                            "Выход"
                        };

                        Menu menu = new Menu(items);
                        int menuResult;
                        do
                        {
                            menuResult = menu.PrintMenu();

                            //Console.Clear();
                            switch (menuResult)
                            {
                                case 0:
                                    {
                                        Console.WriteLine("Введите новый цвет фона (0 - 15) : ");
                                        UInt16 backGroundColor = UInt16.Parse(Console.ReadLine());
                                        (ag[i] as Settings).BackgroundColor = backGroundColor;
                                        break;
                                    }
                                case 1:
                                    {
                                        Console.WriteLine("Введите новый цвет текста (0 - 15) : ");
                                        UInt16 foreGroundColor = UInt16.Parse(Console.ReadLine());
                                        (ag[i] as Settings).ForegroundColor = foreGroundColor;
                                        break;
                                    }
                                case 2:
                                    {
                                        Console.WriteLine("Введите ширину окна : ");
                                        UInt16 width = UInt16.Parse(Console.ReadLine());
                                        (ag[i] as Settings).Width = width;
                                        break;
                                    }
                                case 3:
                                    {
                                        Console.WriteLine("Введите высоту окна : ");
                                        UInt16 heigth = UInt16.Parse(Console.ReadLine());
                                        (ag[i] as Settings).Height = heigth;
                                        break;
                                    }
                                case 4:
                                    {
                                        Console.WriteLine("Введите новый заголовок : ");
                                        System.String title = Console.ReadLine();
                                        (ag[i] as Settings).Title = title;
                                        break;
                                    }
                                default:
                                    break;
                            }
                                (ag[i] as Settings).Print();
                            Console.WriteLine("Данные о настройках окна были успешно изменены");

                        } while (menuResult != items.Length - 1);
                    }
                }

                if (!flag)
                    throw new Exception("Настройки с таким названием отсутствуют в файле!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }

        public void Print() 
        {
            try
            {
                Console.WriteLine("\n\t\t\t\t\tСписок настроек окна :\n\nЦвет фона: \tЦвет текста: \tШирина окна: \tВысота окна: \tЗаголовок: ");

                foreach (Settings temp in ag)
                {
                    temp.Print();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }

        public void SaveBinary()
        {
            try
            {
                const string fileName = "Settings.bin";

                using (stream = new FileStream(fileName, FileMode.Create))
                {
                    formatter = new BinaryFormatter();
                    formatter.Serialize(stream, ag);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }
        
        public void LoadBinary()
        {
            try
            {
                const string fileName = "Settings.bin";

                if (File.Exists(fileName))
                {
                    using (stream = new FileStream(fileName, FileMode.Open))
                    {
                        formatter = new BinaryFormatter();
                        ag = (ArrayList)formatter.Deserialize(stream);
                        stream.Close();
                        Console.WriteLine("Десериализация успешно выполнена!");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return;
            }
        }

        #region foreach
        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public object Current
        {
            get
            {
                return ag[position];
            }
        }
        public void Reset()
        {
            position = -1;
        }

        public bool MoveNext()
        {
            if (position < ag.Count - 1)
            {
                position++;

                return true;
            }
            else
            {
                this.Reset();

                return false;
            }
        }

        public object Clone()
        {
            return new Settings();
        }

        #endregion
    }
}
