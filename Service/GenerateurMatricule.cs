namespace GestNotesCS.Service;

public class GenerateurMatricule : IGenerateurMatricule
{
    private int _compteur;

    public string Suivant()
    {
        _compteur++;
        return $"ism{_compteur}";
    }
}