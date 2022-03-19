using System;

namespace ClassesAndObjects.Classes {

    public class Consola
    {
        public static string Tag = "Consola";
        private readonly string _serial;
        private string _name = string.Empty;
        private float _hourRate = 0f;

        public string Serial
        {
            get { return _serial; }
        }

        public Consola()
        {
            Guid guid = Guid.NewGuid();
            _serial = guid.ToString();
        }

        public Consola(string serial)
        {
            _serial = serial;
        }
        
        public Consola(string serial, string name, float rate)
        {
            _serial = serial;
            _name = name;
            _hourRate = rate;
        }

        public static void SetTag(string tag)
        {
            Tag = tag;
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetHourRate(float rate)
        {
            _hourRate = rate;
        }

        public void SetHourRate(int days, float totalCost)
        {
            _hourRate = totalCost / days;
        }

        public float GerHourRate()
        {
            return _hourRate;
        }
    }
}