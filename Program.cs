using GestNotesCS.Repository;
using GestNotesCS.Service;
using GestNotesCS.View;

IEtudiantRepository repository = new EtudiantRepositoryMemoire();
IGenerateurMatricule generateur = new GenerateurMatricule();
IRegleAdmission regleAdmission = new RegleMoyenneMinimale(10);
IEtudiantService service = new EtudiantService(repository, generateur, regleAdmission);

var demo = service.AjouterEtudiant("Ba", "Amadou");
foreach (double note in new[] { 10.0, 12.5, 14.0 })
    service.AjouterNote(demo.Matricule, note);

try
{
    new MenuConsole(service, new SaisieConsole()).Demarrer();
}
catch (InvalidOperationException)
{
    Console.WriteLine();
    Console.WriteLine("Entrée fermée, arrêt du programme.");
}