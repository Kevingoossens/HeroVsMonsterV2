using System;

namespace HeroesVsMonsterV2
{

    public class Humain : Hero
    {
        public Humain(Coordonee Coordonee) : base(Coordonee)
        {
        }

        public override int End
        {
            get
            {
                return base.End + 1;
            }
        }

        public override int For
        {
            get
            {
                return base.For + 1;
            }
        }

        public override string Icon
        {
            get { return "H"; }
        }
    }
}