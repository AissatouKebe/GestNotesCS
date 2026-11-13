using GestNotesCS.Entity;

namespace GestNotesCS.Repository;

public class EtudiantRepositoryMemoire : IEtudiantRepository
{
    private readonly List<Etudiant> _etudiants = new();

    public void Ajouter (Etudiant etudiant) => _etudiants.Add(etudiant);

    public Etudiant? GetByMatricule(string matricule) => 
        _etudiants.FirstOrDefault(e => e.Matricule.Equals(matricule, StringComparison.OrdinalIgnoreCase));

    public IReadOnlyList<Etudiant> GetTous() => _etudiants;
} 