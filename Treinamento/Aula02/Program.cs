using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aula02.Model;
using Aula02.Data;
using Aula02.Extensions;

namespace Aula02
{
    class Program
    {
        static void ImprimirConta(Conta conta, string titulo = null)
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------");
            if (!string.IsNullOrEmpty(titulo))
            {
                Console.WriteLine(titulo.ToUpper());
                Console.WriteLine("--------------------------------------");
            }

            string tipoConta = default(string);
            if (conta.GetType() == typeof(ContaCorrente))
            {
                tipoConta = "Corrente";
            }
            else if (conta is ContaPoupanca)
            {
                tipoConta = "Poupança";
            }

            Console.WriteLine($" Conta {tipoConta} Agencia: {conta.Agencia} \tConta: {conta.Numero}");
            Console.WriteLine("--------------------------------------");
        }

        static void ImprimirExtrato(Conta c)
        {
            ImprimirConta(c, "Imprimir Extrato");

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
            Console.WriteLine(" \t 3 - Movimentar contas");
        }

        static void ExibirMenuDaConta(Conta conta)
        {
            ImprimirConta(conta, "Selecione a opção");

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
            Console.WriteLine($"   \t NOVA CONTA CORRENTE");
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

        public static Conta ObterConta()
        {

            Console.Clear();
            Console.WriteLine("--------------------------------------");
            Console.WriteLine($"   \tSELECIONAR CONTA");
            Console.WriteLine("--------------------------------------");

            Console.Write("Informe a agencia: ");
            int agencia = int.Parse(Console.ReadLine());

            Console.Write("Informe a conta: ");
            int numero = int.Parse(Console.ReadLine());

            var conta = RepositorioDeContas.Obter(x => x.Agencia == agencia && x.Numero == numero);

            return conta;

        }

        static void Sacar(Conta conta)
        {
            ImprimirConta(conta, "Saque");

            Console.Write("Informe o valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            conta.Sacar(valor);
        }

        static void Depositar(Conta conta)
        {
            ImprimirConta(conta, "Deposito");

            Console.Write("Informe o valor: ");
            decimal valor = decimal.Parse(Console.ReadLine());

            conta.Depositar(valor);
        }

        static RepositorioBase<Conta> RepositorioDeContas = new RepositorioBase<Conta>();

        static void Main(string[] args)
        {

            //RepositorioDeContas.Salvar(new ContaCorrente(1, 1) { Saldo = 10 });
            //RepositorioDeContas.Salvar(new ContaCorrente(1, 2) { Saldo = 70 });
            //RepositorioDeContas.Salvar(new ContaCorrente(1, 3) { Saldo = 100 });
            //RepositorioDeContas.Salvar(new ContaCorrente(1, 4) { Saldo = 150 });

            //Func<Conta, bool> filtro = (c) => c.Numero == 3;

            //var conta = RepositorioDeContas.Obter((c) => c.Numero == 3 && c.Agencia == 1);

            //EXEMPLO
            //Func<Conta, bool> filtro = (conta) =>
            //{
            //    if (conta.Saldo == 0)
            //        return conta.Agencia == 1;
            //    return conta.Agencia == 2;
            //};

            //Action<Conta> movimentar = (conta) => conta.Movimentar();

            //string a = "Felipe Oriane";
            //string b = a.RemoveSpaces();

            int[] inteiros = { 9, 2, 4, 5, 6, 3, 7, 8, 1, 0 };

            IEnumerable<int> query = from i in inteiros
                                     let p = i % 2
                                     let result = i * 3
                                     orderby result
                                     where (p == 0)
                                     select result;

            foreach (var valor in query)
            {
                Console.WriteLine(valor);
            }
            

            for (int i = 1; i <= 10; i++)
            {
                var conta = new ContaCorrente(1, i);
                conta.Depositar(1000m);

                RepositorioDeContas.Salvar(conta);

                var contaPoupanca = new ContaPoupanca(2, i);
                contaPoupanca.Depositar(1000m);

                RepositorioDeContas.Salvar(contaPoupanca);
            }


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
                        var conta = ObterConta();

                        if (conta == null)
                        {
                            Console.WriteLine("Conta Inexistente!");
                            Console.ReadKey();
                        }
                        else
                        {
                            ExibirMenuDaConta(conta);
                            int opcaoConta;
                            do
                            {
                                ExibirMenuDaConta(conta);

                                Console.Write("Informe a opção: ");
                                opcaoConta = int.Parse(Console.ReadLine());

                                switch (opcaoConta)
                                {
                                    case 1:
                                        //exibir saldo

                                        ImprimirConta(conta, "Saldo");
                                        Console.WriteLine("Saldo Atual: {0}", conta.Saldo.ToString("C2"));
                                        Console.ReadKey();

                                        break;
                                    case 2:
                                        //exibir extrato

                                        ImprimirExtrato(conta);
                                        Console.ReadKey();

                                        break;
                                    case 3:
                                        // saque

                                        Sacar(conta);

                                        break;
                                    case 4:
                                        // deposito

                                        Depositar(conta);

                                        break;
                                }
                            } while (opcaoConta != 0);
                        }
                        break;

                    case 3:

                        var contas = RepositorioDeContas.Obter();

                        foreach (var c in contas)
                        {
                            c.Movimentar();
                        }
                        Console.WriteLine("Contas movimentadas com sucesso!");
                        Console.ReadKey();

                        break;
                }
            } while (op != 0);
        }
    }
}
