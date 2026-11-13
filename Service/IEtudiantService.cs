using GestNotesCS.Dto;

namespace GestNotesCS.Service;

public interface IEtudiantService
{
    EtudiantDto AjouterEtudiant(string nom, string prenom);
    IReadOnlyList<EtudiantDto> ListerEtudiants();
    EtudiantDto? Rechercher(string matricule);
    bool AjouterNote(string matricule, double note);
    IReadOnlyList<EtudiantDto> ListerAdmis();
    IReadOnlyList<ClassementDto> Classement();
    
}