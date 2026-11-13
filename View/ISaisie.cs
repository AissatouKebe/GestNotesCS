namespace GestNotesCS.View;

public interface ISaisie
{
    string LireChaine(string message);
    int LireEntier(string message, int min, int max);
    double LireDouble(string message, double min, double max);
}