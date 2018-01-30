using UnityEngine;
using System.Collections;

namespace BSS.Input {
    public interface IInputListener
    {
        void onListen();
        void onListenVector(Vector2 vec);
        void onListenGameObject(GameObject obj);
    }
}
