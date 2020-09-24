using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sentene
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text = "Экзаменационное задание по C#";
            string text = "задание задание Экзаменационное задание по C#";
            Sentence sentence = new Sentence(text);
            sentence.Print();
            Console.WriteLine(sentence[0]);
            foreach (var item in sentence)
            {
                Console.WriteLine(item);
            }
            sentence.RemoveAll("задание");
            sentence.Print();


            string sentences = "Предложение 1! Предложение 2. Предлодение 3? Предложение 4...";
            Text text1 = new Text(sentences);
            text1.Print();

            sentences = "Разработать класс FileText, который наследует от Text и добавляет следующие методы:";
            FileText fileText = new FileText(sentences);
            fileText.Save("file.txt");

            FileText fileText1= new FileText("");
            fileText1.Load("file.txt");
            fileText1.Print();
        }
    }

    class Sentence : IEnumerable
    {
        protected List<string> Words;
        public int Length => Words.Count;
        public Sentence(string sentences)
        {
            Words = new List<string>();
            Words = sentences.Split(' ').ToList<string>();
        }

        public void Add(string word)
        {
            Words.Add(word);
        }

        public void Insert(int pos, string word)
        {
            Words.Insert(pos, word);
        }

        public void RemoveAt(int pos)
        {
            Words.RemoveAt(pos);
        }

        public void RemoveAll(string word)
        {
            Words.RemoveAll(Words => Words.Contains(word));
        }

        public void Set(string sentence)
        {
            Words = sentence.Split(' ').ToList();
        }

        public string this[int i]
        {
            get
            {
                return Words[i];
            }
            private set { }
        }

        public IEnumerator GetEnumerator()
        {
            return Words.GetEnumerator();
        }

        public void Print()
        {
            foreach (var word in Words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

    }

    class Text
    {
        protected List<Sentence> Sentences;
        public int Length => Sentences.Count;
        public Text(string sentences)
        {
            Sentences = new List<Sentence>();
            ParseTextAndSetSentences(sentences);
        }

        protected void ParseTextAndSetSentences(string sentences)
        {
            Sentences.Clear();
            string[] texts = Regex.Split(sentences, @"(?<=[\.!\?])\s+");
            foreach (var text in texts)
            {
                Sentences.Add(new Sentence(text));
            }

        }

        public void Add(string sentence)
        {
            Sentences.Add(new Sentence(sentence));
        }

        public void Add(Sentence sentence)
        {
            Sentences.Add(sentence);
        }

        public void Insert(int pos, string sentence)
        {
            Sentences.Insert(pos, new Sentence(sentence));
        }

        public void RemoveAt(int pos)
        {
            Sentences.RemoveAt(pos);
        }

        public void Set(string texts)
        {
            ParseTextAndSetSentences(texts);
        }

        public IEnumerator GetEnumerator()
        {
            return Sentences.GetEnumerator();
        }

        public Sentence this[int i] => Sentences[i];

        public string this[int s, int w] => Sentences[s][w];

        public void Print()
        {
            foreach (var sentence in Sentences)
            {
                sentence.Print();
            }
        }
    }

    class FileText : Text
    {

        public FileText(string sentens) : base(sentens)
        {

        }

        public void Save(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                for (int i = 0; i < Sentences.Count; i++)
                {
                    foreach (var item in Sentences[i])
                    {
                        writer.Write(item + " ");
                    }
                    
                }
            }
        }

        public void Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                ParseTextAndSetSentences(reader.ReadToEnd());
            }
        }
    }
}
