using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static rtanRPG.Potion;

namespace rtanRPG
{
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
                else return ((int)price).ToString() + "G";
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
            //return $" {name,-15}| 공격력 +{(int)ability,-15}| {description,-15}|";
            return string.Format(" {0, -" + MyLog.GetKoreanLength(20, name) + "}| 공격력 +{1, -5}| {2, -" + MyLog.GetKoreanLength(50, description) + "}| ", name, ability, description);
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
            return string.Format(" {0, -" + MyLog.GetKoreanLength(20, name) + "}| 방어력 +{1, -5}| {2, -" + MyLog.GetKoreanLength(50, description) + "}| ", name, ability, description);
            //return $" {name,-15}| 방어력 +{(int)ability,-15}| {description,-15}|";
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
                else return ((int)price).ToString() + "G";
            }
            set
            {
                _isSoldout = bool.Parse(value);
            }
        }
    }

    public class HealthPotion : Potion
    {
        public string name { get; set; }
        public string description { get; set; }
        public float ability { get { return effect; } }
        public float price { get; set; }

        //하급체력포션, 중급체력포션등으로 나눌수도?
        public HealthPotion(string name, float effect, string description, float price) : base(name, effect, description, price, 0)
        {

        }

    }

    public class ManaPotion : Potion
    {
        public string name { get; set; }
        public string description { get; set; }
        public float ability { get { return effect; } }
        public float price { get; set; }

        //하급체력포션, 중급체력포션등으로 나눌수도?
        public ManaPotion(string name, float effect, string description, float price) : base(name, effect, description, price, 1)
        {

        }

    }

    public class Potion : Item
    {
        List<string> potionEffect = new List<string>()
            {
                "체력",
                "마나"
            };

        public string name { get; set; }
        public string description { get; set; }
        public float effect;
        public float ability { get { return effect; } }
        public float price { get; set; }
        public bool alreadyHave { get; set; }
        public int potionType;       //포션타입0은 체력포션, 1 ~는 다른타입의 포션
        public string type { get { return potionEffect[potionType]; } }


        public Potion(string name, float effect, string description, float price, int potiontype)
        {
            this.name = name;
            this.effect = effect;
            this.description = description;
            this.price = price;
            potionType = potiontype;
        }

        public string Label()
        {
            return string.Format(" {0, -" + MyLog.GetKoreanLength(20, name) + "}| {1, -" + MyLog.GetKoreanLength(6, type) + "} +{2, -5}| {3, -" + MyLog.GetKoreanLength(50, description) + "}| ", name, type, ability, description);
            //return $" {name,-15}| 방어력 +{(int)ability,-15}| {description,-15}|";
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
                else return ((int)price).ToString() + "G";
            }
            set
            {
                
            }
        }

    }

    public interface Item
        {
            public string name { get; set; }
            public string description { get; set; }
            public float ability { get; }
            public float price { get; set; }
            public bool alreadyHave { get; set; }
            public string type { get; }


            public string isSoldout { get; set; }

            public string Label();

        }
}
