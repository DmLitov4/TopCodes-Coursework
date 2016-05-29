using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCore.Codes
{
    //класс - код Гилберта-Мура
    public class GuilbertMoore : Code
    {
        protected List<KeyValuePair<char, double>> qProbability; 
        protected List<KeyValuePair<char, double>> sigmaProbability;
        protected List<KeyValuePair<char, int>> len;        

        //конструктор (пустой)
        public GuilbertMoore() : base()
        {
            qProbability = new List<KeyValuePair<char, double>>();
            sigmaProbability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();
        }

        //конструктор
        public GuilbertMoore(List<Tuple<char, double>> list) : base()
        {
            qProbability = new List<KeyValuePair<char, double>>();
            sigmaProbability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();

            for (int i = 0; i < list.Count; i++)
            {
                this.setSymAndProb(list[i].Item1, list[i].Item2);
                this.setRes(list[i].Item1, "");
            }
        }

        //функция устанавливает кумулятивные вероятности
        public void setQumProb()
        {
            qProbability.Add(new KeyValuePair<char, double>(this.getAllSym()[0], 0));
            for (int i = 1; i < this.getAllSym().Count; i++)
                qProbability.Add(new KeyValuePair<char, double>(getAllSym()[i], getSymProb(this.getAllSym()[i - 1]) + qProbability[i - 1].Value));
        }

        //установить сигма вероятности
        public void setSigmaProb()
        {
            for (int i = 0; i < this.getAllSym().Count; i++)
                sigmaProbability.Add(new KeyValuePair<char, double>(getAllSym()[i], qProbability[i].Value + getSymProb(this.getAllSym()[i]) / 2));
        }

        //получить массив кумулятивных вероятностей
        public List<KeyValuePair<char, double>> getQumProb()
        {
            return qProbability;
        }

        //получить массив вероятностей сигма
        public List<KeyValuePair<char, double>> getSigmaProb()
        {
            return sigmaProbability;
        }

        //функция определения длины кодового слова 
        public List<KeyValuePair<char, int>> setLength()
        {
            List<KeyValuePair<char, int>> list = new List<KeyValuePair<char, int>>();
            int N = 0;

            var iter = getAllSym();
            for (int i = 0; i < iter.Count; ++i)
            {
                N = 0;
                double deg = Math.Pow(2, N);
                while (getProb(iter[i]) < deg)
                {
                    --N;
                    deg = Math.Pow(2, N);
                }
                N--;
                char ch = iter[i];
                list.Add(new KeyValuePair<char, int>(ch, N));
            }
            return list;
        }

        //собственная реализация перевод вещественного числа в двоичное представление
        public string doubleToBinary(double num)
        {
            string res = "";
            for (int i = 0; i < 15; ++i)
            {
                num = num * 2;
                int integ = Convert.ToInt32(Math.Truncate(num));
                double fract = num - Math.Truncate(num);
                res += integ.ToString();
                num = fract;
            }
            return res;
        }

        //очистить все данные в коде
        public override void clearData()
        {
            base.clearData();
            this.qProbability.Clear();
            this.len.Clear();
        }

        //функция построения кода Гилберта-Мура
        public override void buildCode()
        {
            qProbability.Clear();
            this.setQumProb();
            this.setSigmaProb();
            List<KeyValuePair<char, int>> len = setLength();
            for (int i = 0; i < qProbability.Count; ++i)
            {
                var bin = doubleToBinary(sigmaProbability[i].Value);
                var arr = from val in len where val.Key == sigmaProbability[i].Key select val.Value;
                int n = Math.Abs(arr.First());
                var res = bin.Substring(0, n);
                setRes(sigmaProbability[i].Key, res);
            }


        }
    }
}
