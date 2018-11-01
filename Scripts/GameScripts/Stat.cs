namespace StatSystem
{
    public class Stat
    {
        public string Name;
        public string Units;
        public int Value{get;set;}
        public int Capacity;
        bool IsFull;

        //Increase w/ amount
        public void Increase(int amount)
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
        private void Decrease(int amount)
        {
            Value -= amount;
        }

        //Request decrease, and if there enough, decrease it
        public bool RequestDecrease(int amount){
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

        //Request, but dont decrease in case if requested amount is enough
        public bool RequestDecrInfo(int amount)
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
}