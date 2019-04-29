using System;
using System.Collections.Generic;
using System.Text;

namespace VoiceMeeterClasses
{
    public class Bus
    {
        public bool Mono { get; set; }
        public bool Mute { get; set; }
        public bool Eq { get; set; }

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
                Bus compare = obj as Bus;
                return (compare.ToString().Equals(this.ToString()));
            }
            else
            {
                return false;
            }
        }
    }
}
