namespace partita_di_calcio
{
    internal class Program
    {
        static void forzaGiocatori(int[] stats)
        {
            Random rnd = new Random();

            for (int i = 0; i < stats.Length; i++)
            {
                int statisticaGiocatore = rnd.Next(30, 101);

                stats[i] = statisticaGiocatore;
            }

            

        }

        static int forzaSquadra(int[] stats)
        {
            int somma = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                somma = somma + stats[i];
            }

            return somma;
        }

        static void stampaSquadra(int[] stats)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                Console.Write($"[{stats[i]}]");
            }
        }

        static void Main(string[] args)
        {
            int goal1 = 0;

            int goal2 = 0;

            int somma = 0;

            int[] titolari1 = new int[11];
            int[] panchinari1 = new int[5];

            int[] titolari2 = new int[11];
            int[] panchinari2 = new int[5];

            forzaGiocatori(titolari1);
            forzaGiocatori(panchinari1);

            stampaSquadra(titolari1);
            Console.WriteLine();
            stampaSquadra(panchinari1);

            Console.WriteLine();
            Console.WriteLine();

           forzaGiocatori(titolari2);
            forzaGiocatori(panchinari2);

            stampaSquadra(titolari2);
            Console.WriteLine();
            stampaSquadra(panchinari2);

            Console.WriteLine();
            Console.WriteLine();

            int forzaSquadra1 = forzaSquadra(titolari1);

            Console.WriteLine("forza totale della squadra 1: " + forzaSquadra1);

            Console.WriteLine();
            Console.WriteLine();

            int forzaSquadra2 = forzaSquadra(titolari2);

            Console.WriteLine("forza totale della squadra 2: " + forzaSquadra2);

            somma = forzaSquadra1 + forzaSquadra2;

            for (int i = 0; i <= 90; i++)
            {
                Random rnd = new Random();

                int probabilitaGoal = rnd.Next(1, 101);

                if (probabilitaGoal > 2)
                {
                    Console.WriteLine("nessuno ha fatto Goal!");
                }

                else
                {
                    int probabilitaSquadra = rnd.Next(1, somma + 1);

                    if (probabilitaSquadra > forzaSquadra1)
                    {
                        Console.WriteLine("HA SEGNATO LA SQUADRA 2");

                        goal2++;
                    }

                    else
                    {
                        Console.WriteLine("HA SEGNATO LA SQUADRA 1");

                        goal1++;
                    }
                }

                Console.WriteLine();

            }

            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("PUNTEGGIO FINALE: " + goal1 + " - " + goal2);
        }
    }
}
