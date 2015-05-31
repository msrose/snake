using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {

	public Sprite[] sprites;
	public GameObject map;

	private static int? lastRand = null;
	void Start(){
		float newSize = map.GetComponent<PlayArea> ().CellSize;
		RandomizeFood ();
		transform.localScale = new Vector3 (newSize, newSize);
	}
	public void RandomizeFood(){
		float radius = this.GetComponent<BoxCollider2D> ().size.x;

		Vector3 pos = map.GetComponent<PlayArea> ().GetRandomCell ();
		while (Physics2D.OverlapCircle(new Vector2(pos.x,pos.y), radius)){			
			pos = map.GetComponent<PlayArea> ().GetRandomCell (); 
		}
		this.transform.localPosition = pos;
		
		int rand = Random.Range (0, sprites.Length);
		
		if (lastRand != null && lastRand == rand) {
			rand = ((int)lastRand + 1) % sprites.Length;
		}
		
		this.GetComponent<SpriteRenderer>().sprite = sprites[rand];
		lastRand = rand;
	}
}
