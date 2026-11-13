using System.Globalization;

namespace GestNotesCS.View;

public class SaisieConsole : ISaisie
{
    public string LireChaine(string message)
    {
        while (true)
        {
            string saisie = LireLigne(message).Trim();
            if (!string.IsNullOrWhiteSpace(saisie))
            {
                return saisie;
            }
            Console.WriteLine("La saisie ne peut pas être vide. Veuillez réessayer.");
        }
    }

    public double LireDouble(string message, double min, double max)
    {
        while (true)
        {
            string saisie = LireLigne(message).Replace(',', '.');
            if (double.TryParse(saisie, NumberStyles.Float, CultureInfo.InvariantCulture, out double valeur)
                && valeur >= min && valeur <= max)
            {
                return valeur;
            }
            Console.WriteLine($"La saisie n'est pas un nombre valide ou est en dehors de la plage [{min}, {max}]. Veuillez réessayer.");
        }
    }

    private static string LireLigne(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? throw new InvalidOperationException("L'entrée a été fermée.");
    }

    public int LireEntier(string message, int min, int max)
    {
        while (true)
        {
            string saisie = LireLigne(message);
            if (int.TryParse(saisie, out int valeur) && valeur >= min && valeur <= max)
            {
                return valeur;
            }
            Console.WriteLine($"La saisie n'est pas un entier valide ou est en dehors de la plage [{min}, {max}]. Veuillez réessayer.");
        }
    }
}