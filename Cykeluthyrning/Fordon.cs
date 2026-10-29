public class Fordon
{
    public int Nummer { get; set; }
    public double Timpris { get; set; }
    public bool ArUthyrt { get; set; }
    public Kund UthyrdTill { get; set; }

    public Fordon(int nummer, double timpris)
    {
        Nummer = nummer;
        Timpris = timpris;
        ArUthyrt = false;
        UthyrdTill = null;
    }
}