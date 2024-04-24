using System;

namespace HeroesVsMonsterV2
{
    public class Nain : Hero
    {
        public Nain(Coordonee Coordonee) : base(Coordonee)
        {
        }

        public override int End
        {
            get
            {
                return base.End + 2;
            }
        }

        public override string Icon
        {
            get { return "N"; }
        }
    }
}