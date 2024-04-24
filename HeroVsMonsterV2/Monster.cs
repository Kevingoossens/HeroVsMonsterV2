using System;

namespace HeroesVsMonsterV2
{
    public abstract class Monster : Personnage
    {
        public Monster(Coordonee Coordonee) : base(Coordonee)
        {
            Type TMonster = this.GetType();

            if (this is Cuir)
            {
                TMonster.GetProperty("Cuir").SetMethod.Invoke(this, new object[] { De4.Lance() }); ;
            }

            if (this is Or)
            {
                TMonster.GetProperty("Or").SetMethod.Invoke(this, new object[] { De6.Lance() }); ;
            }
        }
    }
}