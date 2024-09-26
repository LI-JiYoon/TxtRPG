using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rtanRPG
{
    //public class Item
    //{
    //    public string name;
    //    public string description;
    //    public float def;
    //    public float atk;
    //    public float price;
    //    public bool alreadyHave;

    //    public Item(string name, float def, float atk, string description, float price)
    //    {
    //        this.name = name;
    //        this.description = description;
    //        this.def = def;
    //        this.atk = atk;
    //        this.price = price;
    //    }

    //    public string label()
    //    {
    //        if (def > 0)
    //        {
    //            return $"- {this.name,-15}| 방어력 +{this.def,-15}| {this.description,-15}|";
    //        }
    //        else
    //        {

    //            return $"- {this.name,-15}| 공격력 +{this.atk,-15}| {this.description,-15}|";
    //        }
    //    }
    //}


    public class AtkItem : Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public float atk;

        public float ability { get { return atk; } }
        public float price { get; set; }
        public bool alreadyHave { get; set; }

        public string type { get { return "atk"; } }
        bool _isSoltout = false;
        public string isSoldout
        {
            get
            {
                if (_isSoltout) return "구매완료";
                else return ((int)price).ToString();
            }
            set
            {
                _isSoltout = bool.Parse(value);
            }
        }

        public AtkItem(string name, float atk, string description, float price)
        {
            this.name = name;
            this.description = description;
           
            this.atk = atk;
            this.price = price;
        }


        public string Label()
        { 
            return $" {name,-15}| 공격력 +{(int)ability,-15}| {description,-15}|";
        }
    }

    public class DefItem : Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public float def;
        public float ability { get { return def; } }
        public float price { get; set; }
        public bool alreadyHave { get; set; }

        public string type { get { return "def"; } }

        public DefItem(string name, float def, string description, float price)
        {
            this.name = name;
            this.description = description;
            this.def = def;
            this.price = price;
        }

        public string Label()
        {
            return $" {name,-15}| 방어력 +{(int) ability,-15}| {description,-15}|";
        }


        bool _isSoldout = false;
        public bool IsSoldout
        {
            get { return _isSoldout; }
            set { _isSoldout = value; }
        }
        public string isSoldout 
        { 
            get 
            {
                if (_isSoldout) return "구매완료";
                else return ((int)price).ToString();
            }
            set
            {
                _isSoldout = bool.Parse(value);
            }
        }
    }

    public interface Item
    {
        public string name { get; set; }
        public string description { get; set; }
        public float ability { get;}
        public float price { get; set; }
        public bool alreadyHave { get; set; }
        public string type { get; }

    
        public string isSoldout { get; set; }

        public string Label();
        
    }
}
