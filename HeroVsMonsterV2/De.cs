using System;

namespace HeroesVsMonsterV2
{
    public class De
    {
        public static int GetNouvelleCaracteristique()
        {
            const int NbreDes = 4;
            int[] Des = new int[NbreDes];
            De d = new De(6);

            for (int i = 0; i < NbreDes; i++)
            {
                Des[i] = d.Lance();
            }

            for (int i = 0; i < NbreDes - 1; i++)
            {
                for (int j = i + 1; j < NbreDes; j++)
                {
                    if (Des[j] > Des[i])
                    {
                        int temp = Des[i];
                        Des[i] = Des[j];
                        Des[j] = temp;
                    }
                }
            }

            return Des[0] + Des[1] + Des[2];
        }

        private int _Maximum;
        private Random _Random;

        public int Minimum
        {
            get { return 1; }
        }

        public int Maximum
        {
            get { return _Maximum; }
            private set { _Maximum = value; }
        }

        public De(int Maximum)
        {
            _Random = new Random(GetSeed());
            this.Maximum = Maximum;
        }

        public int Lance()
        {
            return _Random.Next(Maximum) + 1;
        }

        private int GetSeed()
        {
            //return (int)(DateTime.Now.ToFileTime() % int.MaxValue);

            Guid id = Guid.NewGuid();

            int Seed = 0;
            foreach (byte b in id.ToByteArray())
            {
                Seed += b;
            }

            return Seed;
        }
    }
}


