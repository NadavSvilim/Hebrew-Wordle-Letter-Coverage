using ExcelDataReader;

namespace HebrewWordleThingy
{
    public static class Globals
    {
        public static readonly int WORD_LENGTH = 5;
        public static readonly int THRESHOLD = 5;
    }
    class Program
    {
        static void Main(string[] args)
        {
            string OriginalDbFilePath = "C:\\Users\\Nadav\\Projects\\HebrewWordleThingy\\OriginalWordsDb.xlsx";
            string FilteredDbFilePath = "C:\\Users\\Nadav\\Projects\\HebrewWordleThingy\\FilteredWordsDb.text";
            FilterOriginalDB(OriginalDbFilePath, FilteredDbFilePath);
            Console.WriteLine("Done filtering the original DB for hebrew words of size " + Globals.WORD_LENGTH);
            NonDirectedGraph<Word> graph = CreateGraph(FilteredDbFilePath);
            Console.WriteLine("Done creating the graph");
            List<ForeignWordGroup> groups = FindGroups(graph);
            if (groups.Count == 0)
            {
                Console.WriteLine("No groups found :(");
                return;
            }
            string output = "";
            foreach (ForeignWordGroup group in groups)
            {
                output += group;
            }
            File.WriteAllText("C:\\Users\\Nadav\\Projects\\HebrewWordleThingy\\output.txt", output);
            Console.WriteLine("Done - output is in output.txt");
        }
        public static bool IsForeign(Word w1, Word w2)
        {
            return (w1.BinaryRepresentation & w2.BinaryRepresentation) == 0;
        }

        public static void FilterOriginalDB(string OriginalFilePath, string FilteredDbFilePath)
        {
            string Output = "";
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(OriginalFilePath, FileMode.Open, FileAccess.Read))
            using (var reader = ExcelReaderFactory.CreateReader(stream))
            {
                do
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0) == null)
                        {
                            continue;
                        }
                        string currWord = reader.GetValue(0).ToString();
                        int score = int.Parse(reader.GetValue(1).ToString());
                        if (currWord != null && score >= Globals.THRESHOLD && IsLegalWord(currWord, Globals.WORD_LENGTH))
                        {
                            Output += currWord + "|" + score + "\n";
                        }
                    }
                } while (reader.NextResult());
            }
            File.WriteAllText(FilteredDbFilePath, Output);
        }

        public static bool IsLegalWord(string word, int numOfChars)
        {
            return IsXChars(word, numOfChars) && UniqueChars(word) && OnlyHebrewChars(word);
        }
        public static bool UniqueChars(string word)
        {
            return word.Length == new HashSet<char>(word).Count;
        }

        public static bool OnlyHebrewChars(string word)
        {
            return word.All(c => c >= 0x5D0 && c <= 0x5EA);
        }

        public static bool IsXChars(string word, int numOfChars)
        {
            return word.Length == numOfChars;
        }

        public static NonDirectedGraph<Word> CreateGraph(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            NonDirectedGraph<Word> graph = new MatrixNDG<Word>(lines.Length - 1);
            foreach (string line in lines)
            {
                string[] words = line.Split('|');
                Word w = new Word(words[0], int.Parse(words[1]));
                // adding the word as a vertex
                graph.AddVertex(w);
                // adding an edge between the word and all other words that are foreign to it
            }
            List<Word> V = graph.Vertices();
            for (int i = 0; i < lines.Length - 1; i++)
            {
                Word word1 = V[i];
                for (int j = i + 1; j < lines.Length - 1; j++)
                {
                    Word word2 = V[j];
                    if (IsForeign(word1, word2))
                        graph.AddEdge(word1, word2);
                }
            }
            return graph;
        }

        public static List<ForeignWordGroup> FindGroups(NonDirectedGraph<Word> graph)
        {
            List<ForeignWordGroup> outputOf4 = new List<ForeignWordGroup>();
            List<ForeignWordGroup> outputOf3 = new List<ForeignWordGroup>();
            List<Word> V = graph.Vertices();
            int size = graph.VertexCount();
            for (int i = 0; i < size; i++)
            {
                Word word1 = V[i];
                for (int j = i + 1; j < size && graph.HasEdge(word1, V[j]); j++)
                {
                    Word word2 = V[j];
                    for (int t = j + 1; t < size && graph.HasEdge(word1, V[t]) && graph.HasEdge(word2, V[t]); t++)
                    {
                        Word word3 = V[t];
                        ForeignWordGroup groupOf3 = new ForeignWordGroup(3);
                        groupOf3.AddWord(word1);
                        groupOf3.AddWord(word2);
                        groupOf3.AddWord(word3);
                        for (int s = t + 1; s < size && graph.HasEdge(word1, V[s]) && graph.HasEdge(word2, V[s]) && graph.HasEdge(word3, V[s]); s++)
                        {
                            ForeignWordGroup groupOf4 = new ForeignWordGroup(4);
                            Word word4 = V[s];
                            groupOf4.AddWord(word1);
                            groupOf4.AddWord(word2);
                            groupOf4.AddWord(word3);
                            groupOf4.AddWord(word4);
                            outputOf4.Add(groupOf4);
                        }
                        outputOf3.Add(groupOf3);
                    }
                }
            }
            if (outputOf4.Count > 0)
                return outputOf4;
            return outputOf3;
        }
    }
}