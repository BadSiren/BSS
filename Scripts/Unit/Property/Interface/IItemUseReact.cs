using UnityEngine;
using System.Collections;

namespace BSS {
    public interface IItemUseReact 
    {
        void onItemUse(string ID, float _value);
    }
}
