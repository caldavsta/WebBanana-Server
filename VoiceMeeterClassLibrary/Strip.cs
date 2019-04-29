using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace VoiceMeeterClasses
{
    public class Strip : INotifyPropertyChanged
    {
        public bool Mono { get; set; }
        private bool mute;
        public bool Mute
        {
            get
            {
                return mute;
            }
            set
            {
                if (value != mute)
                {
                    mute = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Solo { get; set; }
        public bool Mc { get; set; }
        public float Gain { get; set; }
        public float PanX { get; set; }
        public float PanY { get; set; }
        public float ColorX { get; set; }
        public float ColorY { get; set; }
        public float FxX { get; set; }
        public float FxY { get; set; }
        public float Audibility { get; set; }
        public float Comp { get; set; }
        public float Gate { get; set; }
        public float EqGain1 { get; set; }
        public float EqGain2 { get; set; }
        public float EqGain3 { get; set; }
        public string label { get; set; }
        public bool A1 { get; set; }
        public bool A2 { get; set; }
        public bool A3 { get; set; }
        public bool B1 { get; set; }
        public bool B2 { get; set; }
        public string FadeTo { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var prop in this.GetType().GetProperties())
            {
                result += string.Format("{0}={1}", prop.Name, prop.GetValue(this, null));
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() == this.GetType())
            {
                Strip compare = obj as Strip;
                return (compare.ToString().Equals(this.ToString()));
            } else
            {
                return false;
            }
        }

    }
}
