using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeCore
{
    //класс - код Хаффмана
    public class Huffman: Code
    {
        //конструктор
        public Huffman(): base(){}

        public Huffman(List<Tuple<char, double>> list): base()
        {
            for (int i = 0; i < list.Count; i++)
            {
                this.setSymAndProb(list[i].Item1, list[i].Item2);
                this.setRes(list[i].Item1, "");
            }
        }

        //функция построения кода
        public override void buildCode()
        {
            while (this.numOfKeys() != 1)
            {
                double min1 = Double.MaxValue;
                double min2 = Double.MaxValue;
                var key1 = symbolProbability.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                min1 = symbolProbability[key1];
                symbolProbability.Remove(key1);
                var key2 = symbolProbability.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
                min2 = symbolProbability[key2];
                symbolProbability.Remove(key2);
                List<char> L = new List<char>();
                L.AddRange(key1);
                L.AddRange(key2);
                symbolProbability.Add(L, min1 + min2);
                for (int i = 0; i < key1.Count; i++)
                    setRes(key1[i], "0");
                for (int i = 0; i < key2.Count; i++)
                    setRes(key2[i], "1");
            }



        }

    }
}
