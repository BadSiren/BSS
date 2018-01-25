using UnityEngine;
using System.Collections;

namespace BSS {
    public interface ISelectReact  {
        void onSelect(GameObject selectObj);
        void onDeselect(GameObject selectObj);
    }
}

