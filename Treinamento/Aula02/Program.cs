using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula02.Model;
using Aula02.Data;

namespace Aula02
{
    class Program
    {
        static void ImprimirExtrato(Conta c)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------IMPRIMINDO EXTRATO----------");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Agencia: {c.Agencia} \tConta: {c.Numero}");
            Console.WriteLine("--------------------------------------");

            Item[] extrato = c.ObterExtrato();

            foreach(Item item in extrato)
            {
                //Console.ForegroundColor = item.Valor < 0 ? ConsoleColor.Red : ConsoleColor.Gray;
                
                if (item.Valor < 0)
                { 
                    Console.ForegroundColor = ConsoleColor.Red;
                    if (item.Tipo == TipoItem.Imposto)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                
                Console.WriteLine(item);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("--------------------------------------");
        }

        static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("===============================================");
            Console.WriteLine("   \tGERENCIADOR DE CONTAS ");
            Console.WriteLine("===============================================");

            Console.WriteLine(" \t 1 - Criar nova conta");
            Console.WriteLine(" \t 2 - Selecionar conta");
        }

        static void ExibirMenuDaConta(Conta conta)
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"Agencia: {conta.Agencia} \tConta: {conta.Numero}");
            Console.WriteLine("--------------------------------------");

            Console.WriteLine(" \t 0 - Sair");
            Console.WriteLine(" \t 1 - Exibir Saldo");
            Console.WriteLine(" \t 2 - Exibir Extrato");
            Console.WriteLine(" \t 3 - Saque");
            Console.WriteLine(" \t 4 - Depósito");

        }

        static void CriarNovaConta()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"\t NOVA CONTA CORRENTE");
            Console.WriteLine("--------------------------------------");

            int agencia, conta;
            decimal saldoInicial;

            Console.Write("Informe a Agencia: ");
            agencia = int.Parse(Console.ReadLine());

            Console.Write("Informe o nr da Conta: ");
            conta = int.Parse(Console.ReadLine());

            Console.Write("Informe o Saldo Inicial: ");
            saldoInicial = decimal.Parse(Console.ReadLine());

            var contaCorrente = new ContaCorrente(agencia, conta);
            contaCorrente.Depositar(saldoInicial);

            RepositorioDeContas.Salvar(contaCorrente);

        }

        static RepositorioBase<Conta> RepositorioDeContas = new RepositorioBase<Conta>();

        static void Main(string[] args)
        {

            RepositorioDeContas.Salvar(new ContaCorrente(1, 1) { Saldo = 10 });
            RepositorioDeContas.Salvar(new ContaCorrente(1, 2) { Saldo = 70 });
            RepositorioDeContas.Salvar(new ContaCorrente(1, 3) { Saldo = 100 });
            RepositorioDeContas.Salvar(new ContaCorrente(1, 4) { Saldo = 150 });

            //Func<Conta, bool> filtro = (c) => c.Numero == 3;

            var conta = RepositorioDeContas.Obter((c) => c.Numero == 3 && c.Agencia == 1);

            //EXEMPLO
            //Func<Conta, bool> filtro = (conta) =>
            //{
            //    if (conta.Saldo == 0)
            //        return conta.Agencia == 1;
            //    return conta.Agencia == 2;
            //};

            //Action<Conta> movimentar = (conta) => conta.Movimentar();



            int op;
            do
            {
                ExibirMenu();
                Console.Write("Informe a opcao: ");
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        CriarNovaConta();
                        break;

                    case 2:
                        break;
                }


            } while (op != 0);


            
            
            
            
            
            
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
        }
    }
}
