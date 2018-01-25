using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace BSS.UI {
    public class OptionBoardViewer : SerializedMonoBehaviour
    {
        public class Option {
            public string title;
            public System.Action action;
        }

        public OptionBoard optionBoard {
            get {
                if (Board.boardList.Find(x => x is OptionBoard) == null) {
                    return null;
                }
                return Board.boardList.Find(x => x is OptionBoard) as OptionBoard;
            }
        }

        public string mainTitle;
        public List<Option> options = new List<Option>();

        public void show() {
            if (optionBoard == null) {
                return;
            }
            switch (options.Count) {
                case 1:
                    optionBoard.Show(mainTitle,options[0].title, options[0].action);
                    break;
                case 2:
                    optionBoard.Show(mainTitle, options[0].title, options[0].action, options[1].title, options[1].action);
                    break;
                case 3:
                    optionBoard.Show(mainTitle, options[0].title, options[0].action, options[1].title, options[1].action, options[2].title, options[2].action);
                    break;
                default:
                    break;
            }
        }
    }
}
