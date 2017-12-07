﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula01.Model;

namespace Aula01
{
    class Program
    {
        public static void Print(object o)
        {
            Console.WriteLine(o.ToString());
        }

        public static void Show(Local local)
        {
            Console.WriteLine("{0} - {1}", local.Code, local.Size);

        }

        static void Main(string[] args)
        {
            int i = 10;
            bool b = true;
            //string s = "vini";
            /*
            Print(i);
            Print(b);
            Print(s);
            */
            Sector sector1 = new Sector("S1", 100);

            Farm farm1 = new Farm();
            farm1.Code = "FM1";

            Block block1 = new Block();
            block1.Code = "B1";
            block1.Size = 50;

            Field field1 = new Field();
            field1.Code = "F5";
            field1.Size = 25;

            Show(sector1);
            Show(block1);



            Console.ReadKey();
        }
    }
}