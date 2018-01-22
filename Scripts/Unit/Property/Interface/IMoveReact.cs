using UnityEngine;
using System.Collections;

namespace BSS {
    public interface IMoveReact 
    {
        void onMove(Vector2 pos,float speed);
        void onStop();
    }
}
