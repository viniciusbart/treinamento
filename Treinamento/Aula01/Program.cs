using System;
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
            //int i = 10;
            //bool b = true;
            //string s = "vini";
            /*
            Print(i);
            Print(b);
            Print(s);
            */
            Sector sector1 = new Sector("S1", 1000);

            Farm farm1 = new Farm();
            farm1.Code = "FM1";

            Block block1 = new Block();
            block1.Code = "B1";
            block1.Size = 50;

            Field field = new Field();
            field.Code = "F1";
            field.Size = 10;
            field.Culture = "Sugar Cane";

            Show(sector1);
            Show(farm1);
            Show(block1);
            Show(field);


            /// EXEMPLOS ANTERIORES
            /*                       
            Conta c = new ContaCorrente(1010,2020);
            c.Depositar(100m);
            c.Sacar(30m);
            c.Depositar(150m);
            c.Sacar(10m);

            RepositorioDeContas.Salvar(c);

            ImprimirExtrato(c);*/

            //int i = 10;
            //Console.WriteLine("Input a number: ");
            //int n = int.Parse(Console.ReadLine());

            /*
            int[] numbers = { 1, 2, 3, 4, 5 };

            string a = "Letra A";
            string b = "Letra B";
            string c = a + b;

            StringBuilder sb = new StringBuilder();
            sb.Append("Vinicius");
            sb.Append(" ");
            sb.Append("Bazanelli");
            sb.Insert(0, "Sr. ");

            Console.WriteLine(sb.ToString());

            var name = "Felipe Oriani";
            var x = name.Split(' ');
            var y = string.Join(";", x);
            var z = name.Replace(" ", "\"");

            Console.WriteLine(y);
            Console.WriteLine(z);

            try
            {
                Bomba(120);
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine("Indice fora do array.");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Parâmetro {e.ParamName}: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;
            }

            Console.ReadKey();
            
            */

            Console.ReadKey();
        }
    }
}
