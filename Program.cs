using System.Globalization;

try
{
    new Application().Demarrer();
}
catch (InvalidOperationException)
{
    Console.WriteLine();
    Console.WriteLine("Entrée fermée, arrêt du programme.");
}

class Etudiant
{
    public string Matricule { get; }
    public string Nom { get; }
    public string Prenom { get; }
    public List<double> Notes { get; } = new();

    public Etudiant(string matricule, string nom, string prenom)
    {
        Matricule = matricule;
        Nom = nom;
        Prenom = prenom;
    }
        // null quand l'étudiant n'a encore aucune note
    public double? Moyenne => Notes.Count == 0 ? null : Notes.Average();

    public override string ToString() => $"{Matricule} | {Nom} {Prenom}";
}

class Application
{
    private const double NoteMin = 0;
    private const double NoteMax = 20;
    private readonly List<Etudiant> etudiants = new();

    private int compteur = 0;

    public void Demarrer()
    {
        // Un étudiant de démonstration, comme en cours
        var demo = new Etudiant(GenererMatricule(), "Ba", "Amadou");
        demo.Notes.AddRange(new[] { 10.0, 12.5, 14.0 });
        etudiants.Add(demo);

        int choix;
        do
        {
            choix = Menu();
            switch (choix)
            {
                case 1: AjouterEtudiant(); break;
                case 2: AfficherEtudiants(); break;
                case 3: AjouterNote(); break;
                case 4: AfficherNotes(); break;
                case 5: AfficherMoyenne(); break;
                case 0: Console.WriteLine("Au revoir"); break;
            }
        } while (choix != 0);
    }

    // ---------- Menu ----------

    private int Menu()
    {
        Console.WriteLine();
        Console.WriteLine("===== Gestion des notes =====");
        Console.WriteLine("1. Ajouter un étudiant");
        Console.WriteLine("2. Afficher la liste des étudiants");
        Console.WriteLine("3. Ajouter une note à un étudiant");
        Console.WriteLine("4. Lister les notes d'un étudiant");
        Console.WriteLine("5. Afficher la moyenne d'un étudiant");
        Console.WriteLine("0. Quitter");
        return LireEntier("Votre choix : ", 0, 5);


    }

    // ---------- Fonctionnalités ----------

    private void AjouterEtudiant()
    {
        string nom = LireChaine("Nom : ");
        string prenom = LireChaine("Prénom : ");
        var etudiant = new Etudiant(GenererMatricule(), nom, prenom);
        etudiants.Add(etudiant);
        Console.WriteLine($"Étudiant ajouté avec le matricule {etudiant.Matricule}.");
    }

    private void AfficherEtudiants()
    {
        if (etudiants.Count == 0)
        {
            Console.WriteLine("Aucun étudiant enregistré.");
            return;
        }
        etudiants.ForEach(e => Console.WriteLine(e));
    }

        private void AjouterNote()
    {
        Etudiant? etudiant = DemanderEtudiant();
        if (etudiant is null) return;

        double note = LireDouble($"Note ({NoteMin} à {NoteMax}) : ", NoteMin, NoteMax);
        etudiant.Notes.Add(note);
        Console.WriteLine("Note ajoutée.");
    }

    private void AfficherNotes()
    {
        Etudiant? etudiant = DemanderEtudiant();
        if (etudiant is null) return;

        if (etudiant.Notes.Count == 0)
        {
            Console.WriteLine("Cet étudiant n'a aucune note.");
            return;
        }
        etudiant.Notes.ForEach(n => Console.WriteLine(n.ToString("F2")));
    }

        private void AfficherMoyenne()
    {
        Etudiant? etudiant = DemanderEtudiant();
        if (etudiant is null) return;

        if (etudiant.Moyenne is double moyenne)
            Console.WriteLine($"Moyenne de {etudiant.Prenom} {etudiant.Nom} : {moyenne:F2}");
        else
            Console.WriteLine("Aucune note : moyenne impossible à calculer.");
    }

    // ---------- Outils ----------

    private string GenererMatricule()
    {
        compteur++;
        return $"ism{compteur}";
    }

        private Etudiant? GetEtudiantByMatricule(string matricule)
    {
        return etudiants.FirstOrDefault(e =>
            e.Matricule.Equals(matricule, StringComparison.OrdinalIgnoreCase));
    }

    private Etudiant? DemanderEtudiant()
    {
        if (etudiants.Count == 0)
        {
            Console.WriteLine("Aucun étudiant enregistré.");
            return null;
        }
        string matricule = LireChaine("Matricule : ");
        Etudiant? etudiant = GetEtudiantByMatricule(matricule);
        if (etudiant is null)
            Console.WriteLine("Ce matricule ne correspond à aucun étudiant.");
        return etudiant;
    }

    private static string? LireLigne(string message)

    {
        Console.Write(message);
        string? ligne = Console.ReadLine();
        if (ligne is null)
            throw new InvalidOperationException("L'entrée a été fermée.");
        return ligne;
    }

    private static string LireChaine(string message)
    {
        while (true)
        {
            string saisie = LireLigne(message)!.Trim();
            if (!string.IsNullOrWhiteSpace(saisie)) return saisie;
            Console.WriteLine("La saisie ne peut pas être vide.");
        }
    }

    private static int LireEntier(string message, int min, int max)
    {
        while (true)
        {
            string saisie = LireLigne(message)!;
            if (int.TryParse(saisie, out int valeur) && valeur >= min && valeur <= max)
                return valeur;
            Console.WriteLine($"Saisir un entier entre {min} et {max}.");
        }
    }

        private static double LireDouble(string message, double min, double max)
    {
        while (true)
        {
            // Accepte "12,5" comme "12.5"
            string saisie = LireLigne(message)!.Replace(',', '.');
            if (double.TryParse(saisie, NumberStyles.Float, CultureInfo.InvariantCulture, out double valeur)
                && valeur >= min && valeur <= max)
                return valeur;
            Console.WriteLine($"Saisir un nombre entre {min} et {max}.");
        }
    }
}