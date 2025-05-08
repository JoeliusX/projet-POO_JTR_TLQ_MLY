Console.WriteLine("Salut!");

static string CalculerOperation()
{
    Console.WriteLine("Donnez moi un nombre svp.");
    string nombre1 = Console.ReadLine();
    Console.WriteLine("Donnez moi un  deuxième nombre.");
    string nombre2 = Console.ReadLine();
    Console.WriteLine("Quel operation voulez vous faire? (+ pour somme, - pour soustraction, * pour multiplication, / pour division)");
    string operation = Console.ReadLine();

    int numero1 = Int32.Parse(nombre1);
    int numero2 = Int32.Parse(nombre2);

    if (operation == "+")
    {
        int calcul = numero1 + numero2;

    }
    else if (operation == "-")
    {
        int calcul = numero1 - numero2;
    }
    else if (operation == "*")
    {
        int calcul = numero1 * numero2;
    }
    else if (operation == "/")
    {
        float calcul = numero1 / numero2;
    }
    else
    {
        Console.WriteLine("Veillez bien noter les nombres et le symbole svp!");
        string calcul = "introuvable";
    }
    string resultat = calcul;
    return resultat;
}

while (true)
{
    CalculerOperation();
    Console.WriteLine("Le resultat de l'operation de ces nombres est " + resultat + ".");
}