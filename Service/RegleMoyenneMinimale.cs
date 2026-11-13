using GestNotesCS.Entity;

namespace GestNotesCS.Service;

public class RegleMoyenneMinimale : IRegleAdmission
{
    private readonly double _seuil;

    public RegleMoyenneMinimale(double seuil = 10) => _seuil = seuil;

    public bool EstAdmis(Etudiant etudiant) => etudiant.Moyenne is double m && m >= _seuil;
}