using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aiv.Fast2D;


namespace AIV_Engine
{
    enum KeyName { Up, Down, Right, Left, Fire, Jump, LAST }

    internal struct KeysList
    {
        private KeyCode[] keyCodes;

        public KeysList(List<KeyCode> keys)
        {
            keyCodes = keys.ToArray();
        }

        public void SetKey(KeyName name, KeyCode code)
        {
            keyCodes[(int)name] = code;
        }

        public KeyCode GetKey(KeyName name)
        {
            return keyCodes[(int)name];
        }
    }
}
