using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
	[SerializeField]
	private bool _isSelect;	
	public bool isSelect {
		get {
			return _isSelect;
		}
		set {
			_isSelect = value;
			if (_isSelect) {
				image.sprite = selectSpr;
			} else {
				image.sprite = noSelectSpr;
			}
		}
	}
	public Sprite noSelectSpr;
	public Sprite selectSpr;

	private Image image;
	// Use this for initialization
	void Awake ()
	{
		image = GetComponent<Image> ();
		if (_isSelect) {
			image.sprite = selectSpr;
		} else {
			image.sprite = noSelectSpr;
		}
	}
	public void toggleSelect() {
		isSelect = !isSelect;
	}

}

