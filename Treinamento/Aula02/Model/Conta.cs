using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Model
{
    public abstract class Conta
    {
        public int Agencia { get; set; } = 1616;

        public int Numero { get; set; } = 1234;

        public decimal Saldo { get; set; }

        protected List<Item> _extrato = new List<Item>();

        public virtual void Depositar(decimal valor)
        {
            Saldo += valor;

            _extrato.Add(new Item()
            {
                Tipo = TipoItem.Deposito,
                Data = DateTime.Now,
                Valor = valor,
                Saldo = this.Saldo
            });
        }

        public virtual void Sacar(decimal valor)
        {
            Saldo -= valor;

            _extrato.Add(new Item()
            {
                Tipo = TipoItem.Saque,
                Data = DateTime.Now,
                Valor = valor * -1,
                Saldo = this.Saldo
            });

        }

        public abstract void Movimentar();

        public Item[] ObterExtrato()
        {
            return _extrato.ToArray();
        }
    }
}
