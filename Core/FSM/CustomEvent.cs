using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlayer.Core.FSM
{
    public class CustomEvent<T> : EventArgs
    {
        public T Value;
    }
}
