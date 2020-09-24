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
            string text = "Экзаменационное задание по C#";
            Sentence sentence = new Sentence(text);
            sentence.Print();
            Console.WriteLine(sentence[0]);
            foreach (var item in sentence)
            {
                Console.WriteLine(item);
            }

            string sentences = "Предложение 1! Предложение 2. Предлодение 3? Предложение 4...";
            Text text1 = new Text(sentences);
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

        public virtual void Add(string word)
        {
            Words.Add(word);
        }

        public virtual void Insert(int pos, string word)
        {
            Words.Insert(pos, word);
        }

        public virtual void RemoveAt(int pos)
        {
            Words.RemoveAt(pos);
        }

        public void RemoveAll(string word)
        {
            
        }

        public virtual void Set(string sentence)
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

        public virtual IEnumerator GetEnumerator()
        {
            return Words.GetEnumerator();
        }

        public virtual void Print()
        {
            foreach (var word in Words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
        }

    }

    class Text : Sentence
    {
        protected List<Sentence> Sentences;
        public int Length => Sentences.Count;
        public Text(string sentens) : base(sentens)
        {
            Sentences = new List<Sentence>();
        }

        public override void Add(string sentence)
        {
            Sentences.Add(new Sentence(sentence));
        }

        public void Add(Sentence sentence)
        {
            Sentences.Add(sentence);
        }

        public override void Insert(int pos, string sentence)
        {
            Sentences.Insert(pos, new Sentence(sentence));
        }

        public override void RemoveAt(int pos)
        {
            Sentences.RemoveAt(pos);
        }

        public override void Set(string text)
        {
            //Sentences = new List<Sentence>();
        }

        public override IEnumerator GetEnumerator()
        {
            return Sentences.GetEnumerator();
        }

        public Sentence this[int i] => Sentences[i];

        public string this[int s, int w] => Sentences[s][w];

        public override void Print()
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
                foreach (var sentece in Sentences)
                {
                    writer.Write(sentece);
                }
            }
        }

        public Text Load(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                return new Text(reader.ReadToEnd());
            }
            throw new Exception("Ошибка с путем к файлу.");
        }
    }
}
