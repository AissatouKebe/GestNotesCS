namespace GestNotesCS.Dto;

public record EtudiantDto(string Matricule, string Nom, string Prenom, IReadOnlyList<double> Notes, double? Moyenne);