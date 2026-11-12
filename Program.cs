// Version 1 : tout dans un seul fichier, sans découpage SOLID.
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

    public override string ToString() => $"{Matricule} | {Nom} {Prenom}";
}

class Application
{

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
        Console.WriteLine("0. Quitter");
        return LireEntier("Votre choix : ", 0, 2);

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

    // ---------- Outils ----------

    private string GenererMatricule()
    {
        compteur++;
        return $"ism{compteur}";
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
}