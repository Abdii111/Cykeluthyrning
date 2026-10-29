using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        List<Fordon> fordonLista = new List<Fordon>();
        List<Kund> kundLista = new List<Kund>();

        fordonLista.Add(new Cykel(1));
        fordonLista.Add(new Cykel(2));
        fordonLista.Add(new Elcykel(3, 50));
        fordonLista.Add(new Elcykel(4, 20));

        bool korProgrammet = true;

        while (korProgrammet)
        {
            Console.WriteLine("\n=== Kajens Cykeluthyrning ===");
            Console.WriteLine("1. Visa fordon");
            Console.WriteLine("2. Registrera kund");
            Console.WriteLine("3. Hyr ut");
            Console.WriteLine("4. Lämna tillbaka");
            Console.WriteLine("5. Ladda");
            Console.WriteLine("6. Avsluta");
            Console.Write("Val: ");

            string val = Console.ReadLine();

            switch (val)
            {
                case "1":
                    VisaFordon(fordonLista);
                    break;
                case "2":
                    RegistreraKund(kundLista);
                    break;
                case "3":
                    HyrUtFordon(fordonLista, kundLista);
                    break;
                case "4":
                    LamnaTillbakaFordon(fordonLista);
                    break;
                case "5":
                    LaddaFordon(fordonLista);
                    break;
                case "6":
                    korProgrammet = false;
                    break;
                default:
                    Console.WriteLine("Felaktigt val, försök igen.");
                    break;
            }
        }
    }

    static void VisaFordon(List<Fordon> fordonLista)
    {
        Console.WriteLine("\nNr\tTyp\tStatus\t\tBatteri");
        foreach (var f in fordonLista)
        {
            string status = f.ArUthyrt ? "uthyrd" : "ledig";
            string typ = f is Elcykel ? "Elcykel" : "Cykel";
            string batteriInfo = "";

            if (f is Elcykel elcykel)
            {
                batteriInfo = $"{elcykel.BatteriNiva} %";
            }

            Console.WriteLine($"{f.Nummer}\t{typ}\t{status}\t\t{batteriInfo}");
        }
    }

    static void RegistreraKund(List<Kund> kundLista)
    {
        Console.Write("Namn: ");
        string namn = Console.ReadLine();

        Console.Write("Ålder: ");
        int alder = int.Parse(Console.ReadLine());

        Console.Write("Medlem (j/n): ");
        string medlemSvar = Console.ReadLine();
        bool arMedlem = (medlemSvar.ToLower() == "j");

        kundLista.Add(new Kund(namn, alder, arMedlem));
        Console.WriteLine($"{namn} är registrerad.");
    }

    static void HyrUtFordon(List<Fordon> fordonLista, List<Kund> kundLista)
    {
        if (kundLista.Count == 0)
        {
            Console.WriteLine("Inga kunder finns registrerade. Registrera kund först!");
            return;
        }

        Console.Write("Kundens namn: ");
        string kundNamn = Console.ReadLine();
        Kund aktivKund = kundLista.Find(k => k.Namn.ToLower() == kundNamn.ToLower());

        if (aktivKund == null)
        {
            Console.WriteLine("Hittade ingen kund med det namnet.");
            return;
        }

        Console.Write("Fordonsnummer: ");
        int fordonsNummer = int.Parse(Console.ReadLine());
        Fordon valtFordon = fordonLista.Find(f => f.Nummer == fordonsNummer);

        if (valtFordon == null)
        {
            Console.WriteLine("Fordonet finns inte.");
            return;
        }

        if (valtFordon.ArUthyrt)
        {
            Console.WriteLine("Fordonet är redan uthyrt!");
            return;
        }

        if (valtFordon is Cykel && aktivKund.Alder < 15)
        {
            Console.WriteLine("Kunden är för unga (minst 15 år krävs för cykel).");
            return;
        }

        if (valtFordon is Elcykel elcykel)
        {
            if (aktivKund.Alder < 18)
            {
                Console.WriteLine("Kunden är för ung (minst 18 år krävs för elcykel).");
                return;
            }
            if (elcykel.BatteriNiva < 30)
            {
                Console.WriteLine($"Elcykel {elcykel.Nummer} har för lite batteri ({elcykel.BatteriNiva} %). Ladda den först.");
                return;
            }
        }

        valtFordon.ArUthyrt = true;
        valtFordon.UthyrdTill = aktivKund;
        Console.WriteLine($"{aktivKund.Namn} hyr fordon {valtFordon.Nummer}.");
    }

    static void LamnaTillbakaFordon(List<Fordon> fordonLista)
    {
        Console.Write("Fordonsnummer: ");
        int fordonsNummer = int.Parse(Console.ReadLine());
        Fordon valtFordon = fordonLista.Find(f => f.Nummer == fordonsNummer);

        if (valtFordon == null || !valtFordon.ArUthyrt)
        {
            Console.WriteLine("Detta fordon är inte uthyrt.");
            return;
        }

        Console.Write("Antal timmar: ");
        int timmar = int.Parse(Console.ReadLine());

        double totalPris = valtFordon.Timpris * timmar;
        double rabattSumma = 0;
        string typText = valtFordon is Elcykel ? "Elcykel" : "Cykel";

        if (valtFordon.UthyrdTill != null && valtFordon.UthyrdTill.ArMedlem)
        {
            rabattSumma = totalPris * 0.10;
        }

        double attBetala = totalPris - rabattSumma;

        Console.WriteLine("\n--- KVITTO ---");
        Console.WriteLine($"{typText}, {timmar} timmar à {valtFordon.Timpris} kr \t {totalPris:F2} kr");

        if (rabattSumma > 0)
        {
            Console.WriteLine($"Medlemsrabatt (10 %) \t\t -{rabattSumma:F2} kr");
        }

        Console.WriteLine($"Att betala \t\t\t {attBetala:F2} kr");

        if (valtFordon is Elcykel elcykel)
        {
            elcykel.BatteriNiva -= (timmar * 10);
            if (elcykel.BatteriNiva < 0) elcykel.BatteriNiva = 0;
        }

        valtFordon.ArUthyrt = false;
        valtFordon.UthyrdTill = null;
    }

    static void LaddaFordon(List<Fordon> fordonLista)
    {
    Cout: Console.Write("Ange nummer på elcykel som ska laddas: ");
        int fordonsNummer = int.Parse(Console.ReadLine());
        Fordon valtFordon = fordonLista.Find(f => f.Nummer == fordonsNummer);

        if (valtFordon is Elcykel elcykel)
        {
            elcykel.BatteriNiva = 100;
            Console.WriteLine($"Elcykel {elcykel.Nummer} är nu fulladdad (100 %).");
        }
        else
        {
            Console.WriteLine("Detta är inte en elcykel.");
        }
    }
}