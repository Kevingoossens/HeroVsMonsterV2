using System;

namespace HeroesVsMonsterV2
{
    public abstract class Hero : Personnage, Or, Cuir
    {
        //God Mode
        public override int End
        {
           get { return base.End + 150; }

        }

        private int _Or, _Cuir;

        public int Or
        {
            get { return _Or; }
            private set { _Or = value; }
        }

        public int Cuir
        {
            get { return _Cuir; }
            private set { _Cuir = value; }
        }

        public Hero(Coordonee Coordonee) : base(Coordonee)
        {
        }

        public void SeReposer()
        {
            ResetPV();
        }

        public void Depouiller(Monster Monster)
        {
            if (Monster is Or)
            {
                Or += ((Or)Monster).Or;
            }

            if (Monster is Cuir)
            {
                Cuir += ((Cuir)Monster).Cuir;
            }
        }
    }
}

