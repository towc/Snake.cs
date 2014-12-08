using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameTest.cs
{
    class Control
    {

        public bool[] keys = {false, false, false, false};
                            //right, down, left, up

        public void setKey(int index, bool value)
        {
            keys[index] = value;
        }
        public bool getKey(int index)
        {
            return keys[index];
        }
    }
}
