using HeroesVsMonsterV2;

class Program
{

    static void Main(string[] args)
    {
        int choix;

        Console.WriteLine();
        Console.WriteLine("Bienvenus dans le pays de Stormwall !");
        Console.WriteLine();
        Console.WriteLine("Il est de bon aloi de choisir tes origines héroïques avant de penetrez dans la foret de Shorewood, ");
        Console.WriteLine("dans cette forêt, se livre un combat acharné entre les héros set les monstres.");
        Console.WriteLine();
        Console.WriteLine("faites votre choix Héros");
        Console.WriteLine();
        Console.Write("Choix (1 = Humain, 2 = Nain) : ");
        while (!int.TryParse(Console.ReadLine(), out choix) || !(choix == 1 || choix == 2))
        {
            Console.Write("T'es con l'elfe?? (1 = Humain, 2 = Nain) : ");
        }

        Foret f = new Foret((choix == 1) ? HeroesTypes.Humain : HeroesTypes.Nain);
        f.Lance();

        Console.ReadLine();
    }

}