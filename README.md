# Kajens Cykeluthyrning 🚲

Programmet är skapat för att hjälpa personalen på Kajens Cykeluthyrning i Göteborg att hålla ordning på fordon, kunder och uthyrningar.

## Funktioner i programmet

Programmet har en meny med sex val som styrs via en loop:
1. **Visa fordon:** Visar en lista över alla cyklar och elcyklar, deras status (ledig/uthyrd) och batterinivå.
2. **Registrera kund:** Lägger till en ny kund med namn, ålder och medlemskap.
3. **Hyr ut:** Låter en kund hyra ett fordon. Programmet kollar automatiskt att:
   - Kunden har rätt ålder (minst 15 år för vanlig cykel, minst 18 år för elcykel).
   - Fordonet är ledigt.
   - Elcykeln har minst 30 % batteri.
4. **Lämna tillbaka:** Räknar ut totalpriset baserat på antal timmar och timpriset. Om kunden är medlem får de 10 % rabatt. Batteriet på elcyklar minskar med 10 % per hyrd timme.
5. **Ladda:** Laddar upp batteriet på en elcykel till 100 %.
6. **Avsluta:** Stänger av programmet.

## Hur koden är uppdelad (Klasser och Arv)
Koden är uppdelad i flera filer för att hålla struktur:
* **Fordon.cs:** Basklass med gemensamma egenskaper för alla fordon.
* **Cykel.cs:** Klass för vanliga cyklar (ärver från `Fordon`).
* **Elcykel.cs:** Klass för elcyklar som även har batterinivå (ärver från `Fordon`).
* **Kund.cs:** Klass som sparar kundens uppgifter.
* **Program.cs:** Huvudprogrammet som hanterar menyn och logiken.
