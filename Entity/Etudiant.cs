namespace GestNotesCS.Entity;

public class Etudiant
{
    public const double NoteMin = 0;
    public const double NoteMax = 20;

    public string Matricule {get; }
    public string Nom {get; }
    public string Prenom {get; }
    public List<double> Notes {get;} = new();

    public Etudiant(string matricule, string nom, string prenom)
    {
        Matricule = matricule;
        Nom = nom;
        Prenom = prenom;
    }

    //null si l'étudiant n'a pas de notes
    public double? Moyenne => Notes.Count == 0 ? null : Notes.Average();

    public override string ToString() => $"{Matricule} | {Nom} {Prenom}";
    
}