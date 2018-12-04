// file: CursorScript.cs
using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour
{
	public Texture2D iconArrow;
	public Vector2 arrowRegPoint;
	public Texture2D iconZoom;
	public Vector2 zoomRegPoint;
	public Texture2D iconTarget;
	public Vector2 targetRegPoint;
	private Vector2 mouseReg;
	private GUITexture pointer;
	
	void Start()
	{
		pointer = GetComponent<GUITexture>();
		pointer.enabled = true;
		if (iconArrow)
		{
			pointer.texture = iconArrow;
			mouseReg = arrowRegPoint;
			Cursor.visible = false;
		}
	}
	void Update()
	{
		Vector2 mouseCoord = Input.mousePosition;
		Texture mouseTex = GetComponent<GUITexture>().texture;

		pointer.pixelInset = new Rect(mouseCoord.x - (mouseReg.x), mouseCoord.y - (mouseReg.y), mouseTex.width, mouseTex.height);
	
		if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
		{
			if (iconTarget)
			{
				pointer.texture = iconTarget;
				mouseReg = targetRegPoint;
			}
		}
		else if (Input.GetMouseButton(1))
		{
			if (iconZoom)
			{
				pointer.texture = iconZoom;
				mouseReg = zoomRegPoint;
			}
		}
		else
		{
			if (iconArrow)
			{
				pointer.texture = iconArrow;
				mouseReg = arrowRegPoint;
			}
		}
	}
}