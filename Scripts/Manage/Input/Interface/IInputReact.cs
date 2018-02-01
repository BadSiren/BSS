using UnityEngine;
using System.Collections;

namespace BSS.Input {
    public interface IInputReact 
    {
        void onClick(string clickName, GameObject target);
    }
}
