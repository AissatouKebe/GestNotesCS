using GestNotesCS.Dto;
using GestNotesCS.Entity;
using GestNotesCS.Service;

namespace GestNotesCS.View;

public class MenuConsole
{
    private readonly IEtudiantService _service;
    private readonly ISaisie _saisie;

    public MenuConsole(IEtudiantService service, ISaisie saisie)
    {
        _service = service;
        _saisie = saisie;
    }

    public void Demarrer()
    {
        int choix;
        do
        {
            choix = AfficherMenu();
            switch (choix)
            {
                case 1: AjouterEtudiant(); break;
                case 2: AfficherEtudiants(); break;
                case 3: AjouterNote(); break;
                case 4: AfficherNotes();break;
                case 5: AfficherMoyenne(); break;
                case 6: AfficherAdmis(); break;
                case 7: AfficherClassement(); break;
                case 0: Console.WriteLine("Au revoir"); break;
            }
        }while (choix != 0);
    }

    private int AfficherMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== Gestion des notes =====");
        Console.WriteLine("1. Ajouter un étudiant");
        Console.WriteLine("2. Afficher la liste des étudiants");
        Console.WriteLine("3. Ajouter une note à un étudiant");
        Console.WriteLine("4. Lister les notes d'un étudiant");
        Console.WriteLine("5. Afficher la moyenne d'un étudiant");
        Console.WriteLine("6. Afficher les étudiants admis");
        Console.WriteLine("7. Afficher le classement des étudiants");
        Console.WriteLine("0. Quitter");

        return _saisie.LireEntier("Votre choix : ", 0, 7);
    }

    private void AjouterEtudiant()
    {
        var etudiants = _service.ListerEtudiants();
        if (etudiants.Count == 0)
        {
            Console.WriteLine("Aucun étudiant enregistré.");
            return;
        }
        foreach(var e in etudiants)
        {
            Console.WriteLine($"{e.Matricule} | {e.Nom} {e.Prenom}");
        }
    }

    private void AjouterNote()
    {
        EtudiantDto? etudiant = DemanderEtudiant();
        if (etudiant is null)
        {
            Console.WriteLine("Étudiant non trouvé.");
            return;
        }
        double note = _saisie.LireDouble(
            $"Note ({Etudiant.NoteMin} à {Etudiant.NoteMax}) : ", Etudiant.NoteMin, Etudiant.NoteMax);
        _service.AjouterNote(etudiant.Matricule, note);
        Console.WriteLine("Note ajoutée avec succès.");
    }

    private void AfficherNotes()
    {
        EtudiantDto? etudiant = DemanderEtudiant();
        if (etudiant is null)
        {
            Console.WriteLine("Étudiant non trouvé.");
            return;
        }
        if (etudiant.Notes.Count == 0)
        {
            Console.WriteLine("Aucune note enregistrée pour cet étudiant.");
            return;
        }
        foreach (var n in etudiant.Notes)
        {
            Console.WriteLine(n.ToString("F2"));

        }
    }

    private void AfficherMoyenne()
    {
        EtudiantDto? etudiant = DemanderEtudiant();
        if (etudiant is null)
        {
            Console.WriteLine("Étudiant non trouvé.");
            return;
        }
        if (etudiant.Moyenne is double moyenne)
        {
            Console.WriteLine($"Moyenne de {etudiant.Prenom} {etudiant.Nom} : {moyenne:F2}");
        }
        else
        {
            Console.WriteLine("Cet étudiant n'a pas de notes : impossible de calculer la moyenne.");
        }
    }

    private void AfficherAdmis()
    {
        var admis = _service.ListerAdmis();
        if (admis.Count == 0)
        {
            Console.WriteLine("Aucun étudiant admis.");
            return;
        }
        foreach (var e in admis)
        {
            Console.WriteLine($"{e.Matricule} | {e.Nom} {e.Prenom} | moyenne : {e.Moyenne:F2}");
        }
    }

    private EtudiantDto? DemanderEtudiant()
    {
        if (_service.ListerEtudiants().Count == 0)
        {
            Console.WriteLine("Aucun étudiant enregistré.");
            return null;
        }
        EtudiantDto? etudiant = _service.Rechercher(_saisie.LireChaine("Matricule : "));
        if (etudiant is null)
            Console.WriteLine("Ce matricule ne correspond à aucun étudiant.");
        return etudiant;
    }

    private void AfficherClassement()
    {
        var classement = _service.Classement();
        if (classement.Count == 0)
        {
            Console.WriteLine("Aucun étudiant n'a de moyenne.");
            return;
        }
        foreach (var c in classement)
        {
            Console.WriteLine($"{c.Rang} | {c.Matricule} | {c.Nom} {c.Prenom} | moyenne : {c.Moyenne:F2}");
        }
    }

    private void AfficherEtudiants()
    {
        var etudiants = _service.ListerEtudiants();
        if (etudiants.Count == 0)
        {
            Console.WriteLine("Aucun étudiant enregistré.");
            return;
        }
        foreach (var e in etudiants)
        {
            Console.WriteLine($"{e.Matricule} | {e.Nom} {e.Prenom}");
        }
    }
}