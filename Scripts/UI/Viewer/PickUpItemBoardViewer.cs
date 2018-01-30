using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.UI {
    public class PickUpItemBoardViewer : SerializedMonoBehaviour {
        public PickUpItemBoard pickUpItemBoard {
            get {
                if (Board.boardList.Find(x => x is PickUpItemBoard) == null) {
                    return null;
                }
                return Board.boardList.Find(x => x is PickUpItemBoard) as PickUpItemBoard;
            }
        }

        public string ID;
        public System.Func<string> IDFunc;
        public PickUpItem pickUpItem;

        public void show() {
            if (pickUpItemBoard == null) {
                return;
            }
            var _ID = "";
            if (IDFunc != null) {
                _ID = IDFunc();
            }
            if (!string.IsNullOrEmpty(ID)) {
                _ID = ID;
            }

            pickUpItemBoard.Show(_ID,pickUpItem);
        }
        public void close() {
            if (pickUpItemBoard == null) {
                return;
            }
            pickUpItemBoard.Close();
        }

    }
}