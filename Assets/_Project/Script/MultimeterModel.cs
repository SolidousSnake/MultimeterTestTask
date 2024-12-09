using UnityEngine;

namespace _Project.Script
{
    public class MultimeterModel
    {
        public float Resistance { get; set; }  
        public float Power { get; set; }       

        public enum MultimeterMode
        {
            Neutral,
            Resistance,
            Current,
            VoltageDC,
            VoltageAC
        }

        public string GetDisplayValue(MultimeterMode mode)
        {
            switch (mode)
            {
                case MultimeterMode.Neutral:
                    return "0.00";
                case MultimeterMode.Resistance:
                    return Resistance.ToString("F2");
                case MultimeterMode.Current:
                    float current = Mathf.Sqrt(Power / Resistance);
                    return current.ToString("F2"); 
                case MultimeterMode.VoltageDC:
                    float voltage = Mathf.Sqrt(Power * Resistance);
                    return voltage.ToString("F2");
                case MultimeterMode.VoltageAC:
                    return "0.01"; 
                default:
                    return "0.00";
            }
        }
    }
}