using System;

namespace HeroesVsMonsterV2
{
    public abstract class Personnage
    {
        public event Action<Personnage> Meurt;

        private int _End, _For, _PV, _X, _Y;
        private readonly De _De4, _De6;
        private Coordonee _Coordonee;
        protected De De4
        {
            get { return _De4; }
        }

        protected De De6
        {
            get { return _De6; }
        }

        public int X
        {
            get { return _X; }
            private set { _X = value; }
        }

        public int Y
        {
            get { return _Y; }
            private set { _Y = value; }
        }

        public abstract string Icon
        {
            get;
        }

        public virtual int End
        {
            get { return _End; }
            private set { _End = value; }
        }

        public virtual int For
        {
            get { return _For; }
            private set { _For = value; }
        }

        private int PV
        {
            get { return _PV; }
            set
            {
                _PV = value;
                if (_PV <= 0 && Meurt != null)
                {
                    Meurt(this);
                }
            }
        }

        public Personnage(Coordonee Coordonee)
        {
            _De4 = new De(4);
            _De6 = new De(6);

            X = Coordonee.X;
            Y = Coordonee.Y;

            For = De.GetNouvelleCaracteristique();
            End = De.GetNouvelleCaracteristique();
            ResetPV();
        }

        public void Frappe(Personnage Personnage)
        {
            //Calcule des Dégâts
            int Degat = De4.Lance() + GetModificateur(For);

            Console.WriteLine("{0} Frappe {1} et lui inflige {2} Point(s) de dégat", GetType().Name, Personnage.GetType().Name, Degat);
            //Retrait des dégâts des points de vie de la cible
            Personnage.PV -= Degat;
        }

        internal void SeDeplace(Directions Direction)
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(".");
            switch (Direction)
            {
                case Directions.North:
                    Y -= 1;
                    break;
                case Directions.East:
                    X += 1;
                    break;
                case Directions.South:
                    Y += 1;
                    break;
                case Directions.West:
                    X -= 1;
                    break;
            }

            Console.SetCursorPosition(X, Y);
            Console.Write(Icon);
            Console.SetCursorPosition(0, 0);

        }

        private int GetModificateur(int Caracteristique)
        {
            return (Caracteristique < 5) ? -1 :
                (Caracteristique < 10) ? 0 :
                (Caracteristique < 15) ? 1 : 2;
        }

        public void ResetPV()
        {
            PV = End + GetModificateur(End);
        }
    }
}

