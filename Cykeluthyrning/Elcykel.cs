public class Elcykel : Fordon
{
    public int BatteriNiva { get; set; }

    public Elcykel(int nummer, int batteriNiva) : base(nummer, 70)
    {
        BatteriNiva = batteriNiva;
    }
}