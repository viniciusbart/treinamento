using System;

namespace Aula01.Model
{
    class Local
    {
        public int Size { get; set; } = 100;
        public string Code { get; set; }
        public Local()
        {
        }
        public Local(string code, int size)
        {
            Code = code;
            Size = size;
        }

        public virtual void Harvest()
        {
            Console.WriteLine("-------");
        }
    }

    class Sector : Local
    {
        public Farm[] Farms { get; set; }

        public Sector(string code, int size) : base (code,size)
        {
        }

        public override void Harvest()
        {
            Console.WriteLine("Harvest Sector {0}", Code);
        }
    }

    class Farm : Local
    {
        public Block[] Blocks { get; set; }

        public override void Harvest()
        {
            Console.WriteLine("Harvest Farm {0}", Code);
        }
    }

    class Block : Local
    {
        public Field[] Fields { get; set; }

        public override void Harvest()
        {
            Console.WriteLine("Harvest Block {0}", Code);
        }
    }

    class Field : Local
    {
        private string _culture;
        public string Culture
        {
            get
            {
                return _culture;
            }
            set
            {
                _culture = value;
            }
        }

        public void Plant(string culture)
        {
            Culture = culture;
        }

        public override void Harvest()
        {
            Console.WriteLine("Harvest Field {0}",Code);
        }

    }
}
