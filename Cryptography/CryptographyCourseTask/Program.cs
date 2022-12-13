string[] key = new string[]
{
    "Три деня младите дружини",
    "как прохода бранят. Горските долини",
    "трепетно повтарят на боя ревът.",
    "Пристъпи ужасни! Дванайсетий път",
    "гъсти орди лазят по урвата дива",
    "и тела я стелят, и кръв я залива.",
    "Бури подир бури! Рояк след рояк!",
    "Сюлейман безумний сочи върха пак",
    "и вика: \"Търчете! Тамо са раите!\"",
    "И ордите тръгват с викове сърдити,",
    "и \"Аллах!\" гръмовно въздуха разпра.",
    "Върхът отговаря с други вик: ура!",
    "И с нов дъжд куршуми, камъни и дървье;",
    "дружините наши, оплискани с кърви,",
    "пушкат и отблъскват, без сигнал, без ред,",
    "всякой гледа само да бъде напред",
    "и гърди геройски на смърт да изложи,",
    "и един враг повеч мъртъв да положи.",
    "Пушкалата екнат. Турците ревът,",
    "насипи налитат и падат, и мрът; -",
    "Идат като тигри, бягат като овци",
    "и пак се завръщат; българи, орловци",
    "кат лъвове тичат по страшний редут,",
    "не сещат ни жега, ни жажда, ни труд.",
    "Щурмът е отчаян, отпорът е лют.",
};

int idxDigitCount = 2;

Console.WriteLine("Enter a message to encrypt(in Cyrillic): ");
string message = Console.ReadLine().ToLower();

try
{
    string encrypted = Encrypt(message);
    Console.WriteLine();
    Console.WriteLine($"C = {encrypted}");
    Console.WriteLine();
    Console.WriteLine($"P = {Decrypt(encrypted)}");
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}


string Encrypt(string message)
{
    string result = "";
    var r = new Random();

    foreach(var c in message)
    {
        var possibilities = FindAllPossibilities(c);
        if (char.IsLetter(c) && possibilities.Count == 0)
            throw new Exception($"Missing character {c} in key!");

        if (possibilities.Count == 0)
            continue;

        Index selectedIdx = possibilities[r.Next(possibilities.Count)];
        result += $"{selectedIdx.i}{selectedIdx.j} ";
    }

    if (result.Equals(""))
        throw new Exception("Failed to encrypt!");

    return result;
}

string Decrypt(string cryptogram)
{
    string trimmed = cryptogram.Split(' ').Aggregate((a, b) => a + b);
    string result = "";
    int step = 2 * idxDigitCount;

    for(int i = 0; i < trimmed.Length-1; i += step)
    {
        string charSubstr = trimmed.Substring(i, step);
        int idxI = int.Parse(charSubstr.Substring(0, idxDigitCount));
        int idxJ = int.Parse(charSubstr.Substring(idxDigitCount, idxDigitCount));
        
        
        char decrypted = key[idxI][idxJ];
        result += decrypted;
    }

    if (result == "")
        throw new Exception("Failed to decrypt!");

    return result;
}

List<Index> FindAllPossibilities(char c)
{
    List<Index> result = new List<Index>();
    for(int i = 0; i < key.Length; i++)
        for(int j = 0; j < key[i].Length; j++)
        {
            if (key[i][j] == c && GetIdxDigitCount(i) == idxDigitCount && GetIdxDigitCount(j) == idxDigitCount)
                result.Add(new Index(i, j));
        }

    return result;
}

int GetIdxDigitCount(int i)
{
    return (int)Math.Floor(Math.Log10(i) + 1);
}

public class Index
{
    public int i { get; set; }
    public int j { get; set; }

    public Index(int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}