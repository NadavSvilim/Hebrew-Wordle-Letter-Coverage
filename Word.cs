class Word{
    // כל אות מקבלת ספרה בינארית ע"פ הסדר שלהם בא"ב
    private static readonly Dictionary<char, int> LetterToBit = new Dictionary<char, int>
        {
            { 'א', 1 << 0 },
            { 'ב', 1 << 1 },
            { 'ג', 1 << 2 },
            { 'ד', 1 << 3 },
            { 'ה', 1 << 4 },
            { 'ו', 1 << 5 },
            { 'ז', 1 << 6 },
            { 'ח', 1 << 7 },
            { 'ט', 1 << 8 },
            { 'י', 1 << 9 },
            { 'כ', 1 << 10 },
            { 'ך', 1 << 10 },
            { 'ל', 1 << 11 },
            { 'מ', 1 << 12 },
            { 'ם', 1 << 12 },
            { 'נ', 1 << 13 },
            { 'ן', 1 << 13 },
            { 'ס', 1 << 14 },
            { 'ע', 1 << 15 },
            { 'פ', 1 << 16 },
            { 'ף', 1 << 16 },
            { 'צ', 1 << 17 },
            { 'ץ', 1 << 17 },
            { 'ק', 1 << 18 },
            { 'ר', 1 << 19 },
            { 'ש', 1 << 20 },
            { 'ת', 1 << 21 }
        };
    public string Text { get; private set; }
    public int Score { get; private set; }
    public int BinaryRepresentation { get; private set; }

    public Word(string text, int score){
        Text = text;
        Score = score;
        BinaryRepresentation = 0;
        foreach (char letter in text){
            BinaryRepresentation |= LetterToBit[letter];
        }
    }

    public override bool Equals(object? obj){
        if (obj == null || GetType() != obj.GetType())
            return false;
        Word other = (Word)obj;
        return Text == other.Text;
    }

    public override int GetHashCode(){
        return Text.GetHashCode();
    }

    public override string ToString()
    {
        return Text;
    }
}
