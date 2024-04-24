using System;

namespace HeroesVsMonsterV2
{
    public class Dragonnet : Monster, Or, Cuir
    {
        private int _Or;

        public int Or
        {
            get { return _Or; }
            private set { _Or = value; }
        }

        private int _Cuir;

        public int Cuir
        {
            get { return _Cuir; }
            private set { _Cuir = value; }
        }

        public override int End
        {
            get
            {
                return base.End + 1;
            }
        }

        public Dragonnet(Coordonee Coordonee) : base(Coordonee)
        {
            Cuir = De4.Lance();
            Or = De6.Lance();
        }

        public override string Icon
        {
            get { return "D"; }
        }
    }
}

