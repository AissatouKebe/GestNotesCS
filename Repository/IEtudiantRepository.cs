using GestNotesCS.Entity;

namespace GestNotesCS.Repository;

public interface IEtudiantRepository
{
    void Ajouter(Etudiant etudiant);
    Etudiant? GetByMatricule(string matricule);
    IReadOnlyList<Etudiant> GetTous();
}