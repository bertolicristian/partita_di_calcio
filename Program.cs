using System;

namespace partita_di_calcio
{
    internal class Program
    {
        //forza casuale ogni giocatore
        static void forzaGiocatori(int[] stats)
        {
            Random rnd = new Random();

            for (int i = 0; i < stats.Length; i++)
            {
                stats[i] = rnd.Next(30, 101);
            }
        }

        //forza totale della squadra
        static int forzaSquadra(int[] stats)
        {
            int somma = 0;

            for (int i = 0; i < stats.Length; i++)
            {
                somma = somma + stats[i];
            }

            return somma;
        }

        //forza giocatori
        static void stampaSquadra(int[] stats)
        {
            for (int i = 0; i < stats.Length; i++)
            {
                Console.Write($"[{stats[i]}]");
            }
        }

        //probabilità di ammonizione
        static int ammonizione(int[] titolari)
        {
            Random rnd = new Random();

            int probabilita = rnd.Next(100); 

            if (probabilita < 4)
            {
                int giocatore = rnd.Next(0, titolari.Length);

                
                titolari[giocatore] = titolari[giocatore] - 10;

                Console.WriteLine();
                Console.WriteLine("CARTELLINO GIALLO AL GIOCATORE " + giocatore);

                return giocatore;
            }

            return -1;
        }

        static bool sostituzioneMigliore(int giocatoreAmmonito, int[] titolari, int[] panchina, bool[] espulsi, ref int sostituzioniEffettuate)
            
        {
            // nessun ammonito
            if (giocatoreAmmonito == -1)
            {
                return false;
            }
               

            // se viene espulso non può essere sostituito
            if (espulsi[giocatoreAmmonito])
            {
                return false;
            }
                

            // limite sostituzioni raggiunto
            if (sostituzioniEffettuate >= 5)
            {
                return false;
            }
                

            int forzaTitolare = titolari[giocatoreAmmonito];

            // trova il panchinaro più forte
            int indiceMigliore = -1;
            int forzaMigliore = -1;

            for (int i = 0; i < panchina.Length; i++)
            {
                if (panchina[i] > forzaMigliore)
                {
                    forzaMigliore = panchina[i];
                    indiceMigliore = i;
                }
            }

            // se nessuno in panchina è più forte del titolare ammonito niente sostituzione
            if (indiceMigliore == -1 || forzaMigliore <= forzaTitolare)
            {
                return false;
            }
               

            Console.WriteLine($"SOSTITUZIONE: esce titolare {giocatoreAmmonito}, entra panchinaro {indiceMigliore}");

            // esegue la sostituzione
            titolari[giocatoreAmmonito] = panchina[indiceMigliore];
            panchina[indiceMigliore] = 0;

            sostituzioniEffettuate++;

            return true;
        }
        // Stampa lo stato completo della partita
        static void stampaStatoPartita(int[] titolari1, int[] panchina1, int[] titolari2, int[] panchina2)

        {
            Console.WriteLine();

            Console.WriteLine("STATO ATTUALE SQUADRA 1");

            Console.Write("titolari 1: ");

            stampaSquadra(titolari1);

            Console.WriteLine();

            Console.Write("panchina 1: ");

            stampaSquadra(panchina1);

            Console.WriteLine();

            Console.WriteLine();

            Console.WriteLine("STATO ATTUALE SQUADRA 2");

            Console.Write("titolari 2: ");

            stampaSquadra(titolari2);

            Console.WriteLine();

            Console.Write("panchina 2: ");

            stampaSquadra(panchina2);

            Console.WriteLine();

            int forza1 = forzaSquadra(titolari1);

            int forza2 = forzaSquadra(titolari2);

            int somma = forza1 + forza2;

            Console.WriteLine();

            Console.WriteLine("forza totale squadra 1: " + forza1);

            Console.WriteLine("forza totale squadra 2: " + forza2);

            if (somma > 0)
            {
                float prob1 = forza1 / somma * 100;
                float prob2 = forza2 / somma * 100;
                Console.WriteLine($"probabilità goal squadra 1: {prob1}%");
                Console.WriteLine($"probabilità goal squadra 2: {prob2}%");
            }

            Console.WriteLine();

            Console.WriteLine("====================================");

            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int goal1 = 0;
            int goal2 = 0;

            int[] titolari1 = new int[11];
            int[] panchinari1 = new int[5];

            int[] titolari2 = new int[11];
            int[] panchinari2 = new int[5];

            int[] amm1 = new int[11];
            int[] amm2 = new int[11];

            bool[] espulsi1 = new bool[11];
            bool[] espulsi2 = new bool[11];

            int sostituzioni1 = 0;
            int sostituzioni2 = 0;

            forzaGiocatori(titolari1);
            forzaGiocatori(panchinari1);
            forzaGiocatori(titolari2);
            forzaGiocatori(panchinari2);

            Console.WriteLine("FORMAZIONI INIZIALI");
            stampaStatoPartita(titolari1, panchinari1, titolari2, panchinari2);

            for (int minuto = 1; minuto <= 90; minuto++)
            {
                Console.WriteLine($"-MINUTO {minuto}-");

                int forza1 = forzaSquadra(titolari1);
                int forza2 = forzaSquadra(titolari2);
                int somma = forza1 + forza2;

                Random rnd = new Random();
                int probabilitaGoal = rnd.Next(1, 101);

                if (probabilitaGoal <= 2 && somma > 0)
                {
                    int probabilitaSquadra = rnd.Next(1, somma + 1);

                    if (probabilitaSquadra <= forza1)
                    {
                        Console.WriteLine("HA SEGNATO LA SQUADRA 1");
                        goal1++;
                    }
                    else
                    {
                        Console.WriteLine("HA SEGNATO LA SQUADRA 2");
                        goal2++;
                    }
                }
                else
                {
                    Console.WriteLine("Nessuno ha fatto goal.");
                }

                int giallo1 = ammonizione(titolari1);
                if (giallo1 != -1)
                {
                    Console.WriteLine($"Squadra 1: ammonito giocatore {giallo1}");
                    amm1[giallo1]++;

                    if (amm1[giallo1] >= 2 && !espulsi1[giallo1])
                    {
                        Console.WriteLine($"CARTELLINO ROSSO PER GIOCATORE {giallo1} SQUADRA 1");
                        titolari1[giallo1] = 0;
                        espulsi1[giallo1] = true;
                    }
                    else
                    {
                        bool sost = sostituzioneMigliore(giallo1, titolari1, panchinari1, espulsi1, ref sostituzioni1);
                        if (sost == true)
                        {
                            Console.WriteLine("Sostituzione effettuata per la squadra 1 dopo ammonizione.");
                        }
                            
                    }

                    stampaStatoPartita(titolari1, panchinari1, titolari2, panchinari2);
                }

                int giallo2 = ammonizione(titolari2);
                if (giallo2 != -1)
                {
                    Console.WriteLine($"Squadra 2: ammonito giocatore {giallo2}");
                    amm2[giallo2]++;

                    if (amm2[giallo2] >= 2 && !espulsi2[giallo2])
                    {
                        Console.WriteLine($"CARTELLINO ROSSO PER GIOCATORE {giallo2} SQUADRA 2");
                        titolari2[giallo2] = 0;
                        espulsi2[giallo2] = true;
                    }
                    else
                    {
                        bool sost = sostituzioneMigliore(giallo2, titolari2, panchinari2, espulsi2, ref sostituzioni2);
                        if (sost==true)
                        {
                            Console.WriteLine("Sostituzione effettuata per la squadra 2 dopo ammonizione.");
                        }
                            
                    }

                    stampaStatoPartita(titolari1, panchinari1, titolari2, panchinari2);
                }

                Console.WriteLine();
            }

            Console.WriteLine("=RISULTATO FINALE=");
            Console.WriteLine($"PUNTEGGIO FINALE: {goal1} - {goal2}");
        }
    }
}

