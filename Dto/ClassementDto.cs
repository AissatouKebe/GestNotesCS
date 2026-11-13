namespace GestNotesCS.Dto;

public record ClassementDto(
    int Rang,
    string Matricule,
    string Nom,
    string Prenom,
    double Moyenne);