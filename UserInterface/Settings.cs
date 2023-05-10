using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.ComponentModel;

namespace UserInterface
{
    [Serializable]

    public class Settings : Object
    {

        public System.UInt16 backGroundColor;

        public System.UInt16 foreGroundColor;

        public System.UInt16 width;

        public System.UInt16 height;

        public System.String title;

        public System.UInt16 BackgroundColor
        {
            get
            {
                return backGroundColor;
            }
            set => backGroundColor = value;
        }    
        public System.UInt16 ForegroundColor
        {
            get
            {
                return foreGroundColor;
            }
            set => foreGroundColor = value;
        }
        public System.UInt16 Width
        {
            get
            {
                return width;
            }
            set => width = value;
        }
        public System.UInt16 Height
        {
            get
            {
                return height;
            }
            set => height = value;
        }
        public System.String Title
        {
            get
            {
                return title;
            }
            set => title = value;
        }

        //  Конструктор по умолчанию
        public Settings() :
            this(11, 0, 10,10, "Заголовок")
        {
        }

        //  Конструктор с параметрами
        public Settings(System.UInt16 backGroundColor,
                        System.UInt16 foreGroundColor,
                        System.UInt16 width,
                        System.UInt16 height,
                        System.String title)
        {
            this.backGroundColor = backGroundColor;
            this.foreGroundColor = foreGroundColor;
            this.width = width;
            this.height = height;
            this.title = title;
        }

        public  ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
        public void Print()
        {
            Console.Title = title;
            Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4}", backGroundColor, foreGroundColor, width, height, title);
            Console.BackgroundColor = colors[backGroundColor];
            Console.ForegroundColor = colors[foreGroundColor];
            Console.SetWindowSize(width, height);
        }
    }
}
