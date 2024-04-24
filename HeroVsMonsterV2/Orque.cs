using System;

namespace HeroesVsMonsterV2
{
    public class Orque : Monster, Or
    {
        private int _Or;

        public int Or
        {
            get { return _Or; }
            private set { _Or = value; }
        }

        public override int For
        {
            get
            {
                return base.For + 1;
            }
        }

        public Orque(Coordonee Coordonee) : base(Coordonee)
        {
            Or = De6.Lance();
        }

        public override string Icon
        {
            get { return "O"; }
        }
    }
}

