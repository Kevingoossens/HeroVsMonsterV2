﻿using System;

namespace HeroesVsMonsterV2
{
    public class Foret
    {
        private const int Size = 10;
        private const int NbrEnemi = 8;

        private List<Personnage> _Personnages;
        private readonly De _De3;
        private int _NbrDeCombatGagne;

        private bool _FinDePartie;
        private bool _FightEnd;

        private bool FinDePartie
        {
            get { return _FinDePartie; }
            set { _FinDePartie = value; }
        }

        private readonly Hero _Hero;

        public Hero Hero
        {
            get { return _Hero; }
        }

        protected De De3
        {
            get { return _De3; }
        }

        public Foret(HeroesTypes HeroType)
        {
            _Personnages = new List<Personnage>();
            _De3 = new De(3);

            for (int i = 0; i < NbrEnemi; i++)
            {
                _Personnages.Add(GetNextMonster());
            }

            Coordonee C = GetNewValidCoordonate(2);
            this._Hero = (HeroType == HeroesTypes.Humain) ? (Hero)new Humain(C) : new Nain(C);
            _Personnages.Add(Hero);
            Hero.Meurt += UnPersonnageEstMort;

            Afficher();
        }

        private void UnPersonnageEstMort(Personnage p)
        {
            _FightEnd = true;
            p.Meurt -= UnPersonnageEstMort;

            if (p is Hero)
            {
                FinDePartie = true;

                Console.WriteLine();
                Console.WriteLine("Le {0} est mort.", Hero.GetType().Name);
                AfficherStats();
                Console.WriteLine();
                Console.WriteLine("Fin de partie, vous avez bien combattu, Héro !");
                Console.WriteLine();
                Console.WriteLine("Appuiez sur la touche 'Enter' pour relancer une partie.");

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Le monstre est mort.");
                Console.WriteLine();
                AfficherStatsInBattle(); 
                Console.WriteLine();
                Console.WriteLine("Le {0} récupère pour le prochain affrontement.", Hero.GetType().Name);
                Console.WriteLine();
                Console.WriteLine("Appuiez sur une touche pour continuer...");
                Console.ReadKey();

                _NbrDeCombatGagne++;
                Hero.SeReposer();
                Hero.Depouiller((Monster)p);
                _Personnages.Remove(p);

                if (_Personnages.Count == 1)
                {
                    FinDePartie = true;
                    Console.WriteLine();
                    Console.WriteLine("Il n'y a plus de monstres.");
                    Console.WriteLine();
                    AfficherStats();
                    Console.WriteLine();
                    Console.WriteLine("Félicitations, Héros ! Tu as vaincu tous les monstres de la forêt de Shorewood, lui rendant ainsi sa tranquillité,");
                    Console.WriteLine("le récit de ta quête restera a jamais dans l'histoire du pays de Stormwall !");
                    Console.WriteLine();
                    Console.WriteLine("Appuiez sur la touche 'Enter' pour relancer une partie.");
                }
                else
                {
                    Afficher();
                }
            }
        }

        private void AfficherStats()
        {
            Console.WriteLine("Le {0} a gagné {1} combat(s)", Hero.GetType().Name, _NbrDeCombatGagne);
            Console.WriteLine("Le {0} a accumulé {1} Or(s)", Hero.GetType().Name, Hero.Or);
            Console.WriteLine("Le {0} a accumulé {1} Cuir(s)", Hero.GetType().Name, Hero.Cuir);
        }

        private void AfficherStatsInBattle() 
        {
            Console.WriteLine("Le {0} a accumulé {1} Or(s)", Hero.GetType().Name, Hero.Or);
            Console.WriteLine("Le {0} a accumulé {1} Cuir(s)", Hero.GetType().Name, Hero.Cuir);
        }

        public void Lance()
        {
            Console.WriteLine("Vous pénétrez la forêt de Shorewood !");
            Console.SetCursorPosition(0, Size + 2);
            Console.WriteLine("Utilisez les flèches pour déplacer le Héro...");
            
            bool IsPlayerTurn = true;

            while (!FinDePartie)
            {
                ConsoleKeyInfo ki = Console.ReadKey();

                switch (ki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (CanGoThere(Hero, Directions.North))
                            Hero.SeDeplace(Directions.North);
                        break;
                    case ConsoleKey.RightArrow:
                        if (CanGoThere(Hero, Directions.East))
                            Hero.SeDeplace(Directions.East);
                        break;
                    case ConsoleKey.DownArrow:
                        if (CanGoThere(Hero, Directions.South))
                            Hero.SeDeplace(Directions.South);
                        break;
                    case ConsoleKey.LeftArrow:
                        if (CanGoThere(Hero, Directions.West))
                            Hero.SeDeplace(Directions.West);
                        break;
                    case ConsoleKey.Escape:
                        FinDePartie = true;
                        break;
                }

                Monster M = GetNearestMonster();
                if (M != null)
                {
                    Console.Clear();
                    Console.WriteLine("Stats - {0} :", Hero.GetType().Name);
                    Console.WriteLine("Points de vie - {0} : {1}", Hero.GetType().Name, Hero.PV);
                    Console.WriteLine("Force - {0} : {1}", Hero.GetType().Name, Hero.For);
                    Console.WriteLine("Endurance - {0}: {1}", Hero.GetType().Name, Hero.End);
                    Console.WriteLine();
                    Console.WriteLine("Vous rencontrez un {0}, le combat commence !", M.GetType().Name);
                    Console.WriteLine();
                    Console.WriteLine("Stats - {0} :", M.GetType().Name);
                    Console.WriteLine("Points de vie - {0} : {1}", M.GetType().Name, M.PV);
                    Console.WriteLine("Force - {0} : {1}", M.GetType().Name, M.For);
                    Console.WriteLine("Endurance - {0}: {1}", M.GetType().Name, M.End);
                    Console.WriteLine();

                    _FightEnd = false;
                    while (!_FightEnd)
                    {
                        if (IsPlayerTurn)
                            Hero.Frappe(M);
                        else
                            M.Frappe(Hero);

                        IsPlayerTurn = !IsPlayerTurn;
                    }
                }
            }
        }

        private bool CanGoThere(Personnage p, Directions Direction)
        {
            switch (Direction)
            {
                case Directions.North:
                    return p.Y - 1 > 0;
                case Directions.East:
                    return p.X + 1 < Size + 1;
                case Directions.South:
                    return p.Y + 1 < Size + 1;
                case Directions.West:
                    return p.X - 1 > 0;
                default:
                    return false;
            }
        }

        private Monster GetNearestMonster()
        {
            Monster? M = null;

            foreach (Personnage p in _Personnages)
            {
                //Récupère le monstre ayant les même Coordonees que le héro
                if (M == null && p is Monster && p.X == Hero.X && p.Y == Hero.Y)
                {
                    M = (Monster)p;
                }

                //Récupère le monstre dans les cases adjacentes
                //if (M == null && p is Monster && 
                //    (Math.Abs(p.X - Hero.X) == 1 || Math.Abs(p.X - Hero.X) == 0) &&
                //    (Math.Abs(p.Y - Hero.Y) == 1 || Math.Abs(p.Y - Hero.Y) == 0))
                //{
                //    M = (Monster)p;
                //}
            }

            return M;
        }

        private Monster GetNextMonster()
        {
            Monster? M = null;

            Coordonee C = GetNewValidCoordonate(3);

            switch (De3.Lance())
            {
                case 1:
                    M = new Loup(C);
                    break;
                case 2:
                    M = new Orque(C);
                    break;
                case 3:
                    M = new Dragonnet(C);
                    break;
            }

            M.Meurt += UnPersonnageEstMort;
            return M;
        }

        private Coordonee GetNewValidCoordonate(int Limit)
        {
            De Dice = new De(Size);

            int X, Y;
            bool IsPlaced;

            do
            {
                X = Dice.Lance();
                Y = Dice.Lance();
                IsPlaced = true;

                foreach (Personnage p in _Personnages)
                {
                    if (Math.Abs(p.X - X) < Limit && Math.Abs(p.Y - Y) < Limit)
                    {
                        IsPlaced = false;
                    }
                }

            } while (!IsPlaced);

            return new Coordonee(X, Y);
        }

        private void Afficher()
        {
            Console.Clear();
            foreach (Personnage p in _Personnages)
            {
                Console.SetCursorPosition(p.X, p.Y);
                Console.Write(p.Icon);
                Console.SetCursorPosition(0, 0);
            }
        }
    }
}