using System.Dynamic;
using System.Text.RegularExpressions;

class ForeignWordGroup{
    public HashSet<Word> Words { get; private set; }
    public int TotalScore { get; private set; }
    public int BinaryRepresentation { get; private set; }
    private int MaxSize;
    public ForeignWordGroup(int maxSize){
        Words = new HashSet<Word>();
        TotalScore = 0;
        BinaryRepresentation = 0;
        MaxSize = maxSize;
    }

    public void AddWord(Word word){
        if (Words.Count < MaxSize){
            Words.Add(word);
            TotalScore += word.Score;
            BinaryRepresentation |= word.BinaryRepresentation;
        }
        else
            throw new Exception("Group can only contain up to " + MaxSize +" words");
    }

    public override bool Equals(object? obj){
        if (obj == null || GetType() != obj.GetType())
            return false;
        ForeignWordGroup other = (ForeignWordGroup)obj;
        return Words.SetEquals(other.Words);
    }

    public override int GetHashCode(){
        return Words.GetHashCode();
    }

    public override string ToString(){
        return string.Join("-", Words) + " | Total Score:" + TotalScore + " | Smallest Score: " + MinScoreInGroup() + "\n";  
    }

    public int MinScoreInGroup(){
        return Words.Min(w => w.Score);
    }
}