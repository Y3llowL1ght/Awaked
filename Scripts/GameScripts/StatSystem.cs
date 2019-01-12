using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Godot;
namespace StatSystem
{
    public class Stat
    {
        public string Name;
        public string Units;
        public double Value{get; private set;}
        public double Capacity;
        private bool IsFull;
        public bool IsStatic;

        //Конструктор со значениями
        public Stat(string _name = "default", string _units = "units", int _capacity = 100, bool _isStatic = false)
        {
            Name = _name;
            Units = _units;
            Capacity = _capacity;
            IsStatic = _isStatic;
        }

        //Increase w/ amount
        public void Increase(double amount)
        {
            if (IsFull)
            {
                return;
            }
            if (Value + amount >= Capacity)
            {
                Value = Capacity;
            }else
            {
                Value += amount;
            }
        }

        //Just decrease the amount
        private void Decrease(double amount)
        {
            Value -= amount;
        }

        //Request decrease, and if there enough, decrease it
        public bool RequestDecrease(double amount){
            if (Value - amount < 0)
            {
                return false;
            }
            else
            {
                Decrease(amount);
                return true;
            }
        }

        //Request, but dont decrease in case if requested amount is enough (get info)
        public bool RequestDecrInfo(double amount)
        {
            if (Value - amount < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }

    //StatHolder class
    public class StatHolder
    {
        public System.Collections.Generic.Dictionary<string,Stat> Stats;
        bool IsPassive;
        
        public StatHolder(bool _isPassive = false)
        {
            IsPassive = _isPassive;
            Stats = new System.Collections.Generic.Dictionary<string, Stat>();
        }

        //
        public virtual void Setup(){
            AddStat(StatLoader.GetStatType("default"));
        }

        //Add stat to holder dictionary
        public void AddStat(Stat stat){
                Stats.Add(stat.Name, stat);
        }

        public Stat GetStat(string name){
            return Stats[name];
        }
        


    }

    //Static class for loading stats
    public static class StatLoader
    {

        //Very Uneffective solution? Should be reworked into not loading file each time, or is it?
        public static Stat GetStatType(string Name){

            //Getting absolute path instead of user://
            string path = "res://" + "config/stat_config" + ".json";
            File statsfile = new File();
            statsfile.Open(path, (int)File.ModeFlags.Read);
            string statspath = statsfile.GetPathAbsolute();
            statsfile.Close();

            System.Collections.Generic.Dictionary<string,Stat> stats = JsonConvert.DeserializeObject<System.Collections.Generic.Dictionary<string, Stat> > (System.IO.File.ReadAllText(statspath));
            if (stats[Name] == null)
            {

                return stats["default"];
            }

            return stats[Name];
        }
        
    }
}