using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCore.Codes
{
    //класс - арифметическое кодирование
    public class ArithmetCoding: Code
    {
        protected List<KeyValuePair<char, double>> qProbability;
        protected List<KeyValuePair<char, int>> len;
        protected string input;
        protected double P;
        protected double Q;
        
        //конструктор (пустой)
        public ArithmetCoding()
        {
            qProbability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();
        }

        public ArithmetCoding(List<Tuple<char, double>> list, string inp): base()
        {
            qProbability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();

            for (int i = 0; i < list.Count; i++)
            {
                this.setSymAndProb(list[i].Item1, list[i].Item2);
                this.setRes(list[i].Item1, "");
            }

            this.input = inp;
        }

        //функция получения входной строки
        public string getInput()
        {
            return input;
        }

        //функция устанавливает кумулятивные вероятности
        public void setQumProb()
        {
            qProbability.Add(new KeyValuePair<char, double>(this.getAllSym()[0], 0));
            for (int i = 1; i < this.getAllSym().Count; i++)
                qProbability.Add(new KeyValuePair<char, double>(getAllSym()[i], getSymProb(this.getAllSym()[i - 1]) + qProbability[i - 1].Value));
        }

        //получить массив кумулятивных вероятностей
        public List<KeyValuePair<char, double>> getQumProb()
        {
            return qProbability;
        }

        //получение кумулятивной вероятности по конкретному символу
        public double getSymQumProb(char ch)
        {
            for (int i = 0; i < qProbability.Count(); ++i)
                if (qProbability[i].Key == ch)
                    return qProbability[i].Value;
            return -1;
        }

        //функция нахождения P и Q
        public void findQandP()
        {
            double P = 1;
            double Q = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                Q += P * getSymQumProb(input[i]);
                P *= getSymProb(input[i]);       
            }
            this.P = P;
            this.Q = Q + P / 2;
        }

        //функция устанавливает длину кода
        public int setLength(double P)
        {
            int N = 0;
            double deg = Math.Pow(2, N);
            while (P < deg)
           {
             --N;
             deg = Math.Pow(2, N);
           }
            --N;
            return N;
        }

        //собственная функция перевода вещественного в двоичное представление
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

        public override void buildCode()
        {
            setQumProb();
            findQandP();
            int len = Math.Abs(setLength(this.P));
            var bin = doubleToBinary(Q);
            var res = bin.Substring(0, len);
            setRes(input[0], res);
        }

    }
}
