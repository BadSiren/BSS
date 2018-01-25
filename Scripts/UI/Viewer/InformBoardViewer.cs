using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

namespace BSS.UI {
    public class InformBoardViewer : SerializedMonoBehaviour
    {
        public InformBoard informBoard {
            get {
                if (Board.boardList.Find(x=>x is InformBoard) == null) {
                    return null;
                }
                return Board.boardList.Find(x=>x is InformBoard) as InformBoard;
            }
        }

        public string title;
        public string content;
        public System.Func<string> titleFunc;
        public System.Func<string> contentFunc;

        public void show() {
            if (informBoard==null) {
                return;
            }
            var _title="";
            if (titleFunc!=null) {
                _title=titleFunc();
            }
            if (!string.IsNullOrEmpty(title)) {
                _title=title;
            }
            var _content = "";
            if (contentFunc != null) {
                _content = contentFunc();
            }
            if (!string.IsNullOrEmpty(content)) {
                _content = content;
            }

            informBoard.Show(_title,_content);
        }

    }
}
