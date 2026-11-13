using GestNotesCS.Entity;
using GestNotesCS.Repository;
using GestNotesCS.Dto;

namespace GestNotesCS.Service;

public class EtudiantService : IEtudiantService
{
    private readonly IEtudiantRepository _repository;
    private readonly IGenerateurMatricule _generateur;
    private readonly IRegleAdmission _regleAdmission;

    public EtudiantService(IEtudiantRepository repository, IGenerateurMatricule generateur, IRegleAdmission regleAdmission)
    {
        _repository = repository;
        _generateur = generateur;
        _regleAdmission = regleAdmission;
    }

    public EtudiantDto AjouterEtudiant(string nom, string prenom)
    {
        var etudiant = new Etudiant(_generateur.Suivant(), nom, prenom);
        _repository.Ajouter(etudiant);
        return ToDto(etudiant);
    }

    public IReadOnlyList<EtudiantDto> ListerEtudiants() =>
        _repository.GetTous().Select(ToDto).ToList();

    public EtudiantDto? Rechercher(string matricule)
    {
        Etudiant? etudiant = _repository.GetByMatricule(matricule);
        return etudiant is null ? null : ToDto(etudiant);
    }

    public bool AjouterNote(string matricule, double note)
    {
        if (note < Etudiant.NoteMin || note > Etudiant.NoteMax)
            throw new ArgumentOutOfRangeException(nameof(note), $"La note doit être comprise entre {Etudiant.NoteMin} et {Etudiant.NoteMax}.");
        
        Etudiant? etudiant = _repository.GetByMatricule(matricule);
        if (etudiant is null)
            return false;

        etudiant.Notes.Add(note);
        return true;
        
    }

    public IReadOnlyList<EtudiantDto> ListerAdmis() =>
        _repository.GetTous()
            .Where(e => _regleAdmission.EstAdmis(e))
            .Select(ToDto)
            .ToList();

    public IReadOnlyList<ClassementDto> Classement() =>
        _repository.GetTous()
            .Where(e => e.Moyenne is not null)
            .OrderByDescending(e => e.Moyenne)
            .Select((e, index) => new ClassementDto(index + 1, e.Matricule, e.Nom, e.Prenom, e.Moyenne!.Value))
            .ToList();

    private static EtudiantDto ToDto(Etudiant e) =>
        new EtudiantDto(e.Matricule, e.Nom, e.Prenom, e.Notes.ToList(), e.Moyenne);
}