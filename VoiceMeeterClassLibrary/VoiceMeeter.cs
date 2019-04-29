using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceMeeterClasses
{
    public class VoiceMeeter
    {
        public string SampleString
        {
            get
            {
                return "Sample String!!";
            }
            set
            {

            }
        }
        public Strip[] Strips
        {
            get;
            set;
        }

        public Bus[] Busses
        {
            get;
            set;
        }
        public VoiceMeeter()
        {

        }

        public VoiceMeeter(bool whether)
        {
            Strips = new Strip[5];
            Strips[0] = new Strip();
        }

        public override string ToString()
        {
            string result = string.Empty;
            for (int i = 0; i < Strips.Length; i++)
            {
                result += "Strip " + i + ": " + Strips[i].ToString();
            }
            for (int i = 0; i < Busses.Length; i++)
            {
                result += "Bus " + i + ": " + Busses[i].ToString();
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj.GetType() == GetType()){
                VoiceMeeter compare = obj as VoiceMeeter;
                for(int i = 0; i < Strips.Length; i++)
                {
                    if (!Strips[i].Equals(compare.Strips[i]))
                    {
                        return false;
                    }
                }
                for (int i = 0; i < Busses.Length; i++)
                {
                    if (!Busses[i].Equals(compare.Busses[i]))
                    {
                        return false;
                    }
                }
            } else
            {
                return false;
            }
            return true;
        }
    }
}
