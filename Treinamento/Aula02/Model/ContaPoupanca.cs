using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aula02.Model
{
    class ContaPoupanca : Conta
    {
        public ContaPoupanca(int agencia, int numero)
        {
            this.Agencia = agencia;
            this.Numero = numero;
        }
        private const decimal Juros = 0.005m;

        public override void Movimentar()
        {
            decimal juros = Saldo * Juros;

            Saldo += juros;

            _extrato.Add(new Item()
            {
                Data = DateTime.Now,
                Saldo = this.Saldo,
                Tipo = TipoItem.Juros,
                Valor = juros
            });
        }
    }
}
