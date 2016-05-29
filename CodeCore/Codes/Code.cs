using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCore
{
    public class Code
    {
        //здесь хранится словарь пар вида "символы - их вероятность (p_i)"
        protected Dictionary<List<char>, double> symbolProbability;
        //здесь хранится итоговый результат: "символ - его код (c_i)"
        protected Dictionary<char, string> result;

        //конструктор класса
        public Code()
        {
            symbolProbability = new Dictionary<List<char>, double>();
            result = new Dictionary<char, string>();
        }

        // общее количество символов
        public int numOfKeys()
        {
            return symbolProbability.Keys.Count;
        }

        public virtual void clearData()
        {
            this.symbolProbability.Clear();
            this.result.Clear();
        }

        //меняем вероятность p_i по символу-ключу или добавляем новую пару "символ - вероятность"
        public void setSymAndProb(char sym, double prob)
        {
            bool is_find = false;
            foreach (var key in symbolProbability.Keys.ToList())
            {
                if (key.Contains(sym))
                {
                    symbolProbability[key] = prob;
                    is_find = true;
                    break;
                }
            }
            if (!is_find)
                symbolProbability.Add(new List<char>() { sym }, prob);
        }

        public double getSymProb(char sym)
        {
            foreach (var key in symbolProbability.Keys.ToList())
            {
                if (key.Contains(sym))
                    return symbolProbability[key];
            }
            return -1;
        }

        //получить список всех заданных символов
        public List<char> getAllSym()
        {
            List<char> res = new List<char>();
            var symlist = symbolProbability.Keys.ToList();
            for (int i = 0; i < symlist.Count; i++)
                for (int j = 0; j < symlist[i].Count; j++)
                    res.Add(symlist[i][j]);
            res.Sort();
            return res;
        }

        //получаем вероятность p_i заданного символа
        public double getProb(char sym)
        {
            foreach (var key in symbolProbability.Keys.ToList())
            {
                if (key.Contains(sym))
                    return symbolProbability[key];
            }
            throw new Exception("There's no such symbol in list");
        }

        //устанавливаем результат для символа
        public void setRes(char sym, string res)
        {
            if (result.ContainsKey(sym))
                result[sym] = res + result[sym];
            else
                result.Add(sym, res);
        }

        //получить результат для символа
        public string getRes(char sym)
        {
            return result[sym];
        }

        //функция построения кода
         public virtual void buildCode(){}
    }
}
