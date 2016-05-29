using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCore
{
    //класс - код Шеннона
    public class Shannon : Code
    {
        protected List<KeyValuePair<char, double>> q_probability;
        protected List<KeyValuePair<char, int>> len;

        //конструктор класса (пустой)
        public Shannon() : base()
        {
            q_probability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();    
        }

        //конструктор класса
        public Shannon(List<Tuple<char, double>> list) : base()
        {
            q_probability = new List<KeyValuePair<char, double>>();
            len = new List<KeyValuePair<char, int>>();

            for (int i = 0; i < list.Count; i++)
            {
                this.setSymAndProb(list[i].Item1, list[i].Item2);
                this.setRes(list[i].Item1, "");
            }
                   
        }

        //функция задает кумулятивные вероятности
        public void setQumProb()
        {
            q_probability.Add(new KeyValuePair<char, double>(this.getAllSym()[0], 0));
            for (int i = 1; i < this.getAllSym().Count; i++)
                q_probability.Add(new KeyValuePair<char, double>(getAllSym()[i], getSymProb(this.getAllSym()[i - 1]) + q_probability[i - 1].Value));
        }

        //функция возвращает кумулятивные вероятности
        public List<KeyValuePair<char, double>> getQumProb()
        {
            return q_probability;
        }

        public List<KeyValuePair<char, int>> setLength()
        {
            List<KeyValuePair<char,int>> list = new List<KeyValuePair<char,int>>();
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
                char ch = iter[i];
                list.Add(new KeyValuePair<char, int>(ch, N));
            }
            return list;
        }

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

        public override void clearData()
        {
            base.clearData();
            this.q_probability.Clear();
            this.len.Clear();
        }

        public override void buildCode()
        {
            var list = symbolProbability.ToList();
            list.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));
            list.Reverse();
            q_probability.Clear();
            setQumProb();
            //q_probability.Add(new KeyValuePair<char, double>(list[0].Key[0], 0));
            //for (int i = 1; i < list.Count; ++i)
            //    q_probability.Add(new KeyValuePair<char, double>(list[i].Key[0], list[i - 1].Value + q_probability[i-1].Value));
            List<KeyValuePair<char,int>> len = setLength();
            for (int i = 0; i < q_probability.Count; ++i)
            {
                var bin = doubleToBinary(q_probability[i].Value);
                var arr = from val in len where val.Key == q_probability[i].Key select val.Value;
                int n = Math.Abs(arr.First());
                var res = bin.Substring(0, n);
                setRes(q_probability[i].Key, res);
            }
            
        }
    }
}
