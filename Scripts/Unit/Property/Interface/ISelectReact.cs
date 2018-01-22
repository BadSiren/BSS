using UnityEngine;
using System.Collections;

namespace BSS {
    public interface ISelectReact  {
        void onMainSelect(GameObject selectObj);
        void onSelect(GameObject selectObj);
        void onDeselect(GameObject selectObj);
    }
}

