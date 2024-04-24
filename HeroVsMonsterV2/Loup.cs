using System;

namespace HeroesVsMonsterV2
{
    public class Loup : Monster, Cuir
    {
        private int _Cuir;

        public int Cuir
        {
            get { return _Cuir; }
            private set { _Cuir = value; }
        }

        public Loup(Coordonee Coordonee) : base(Coordonee)

        {
            Cuir = De4.Lance();
        }

        public override string Icon
        {
            get { return "L"; }
        }

    }
}

