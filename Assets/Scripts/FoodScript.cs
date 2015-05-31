using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {

	public Sprite[] sprites;

	private static int? lastRand = null;
	void Start(){}
	public void RandomizeFood(){
		float radius = this.GetComponent<BoxCollider2D> ().size.x;

		Vector3 pos = this.GetComponentInParent<PlayArea> ().GetRandomCell ();
		while (Physics2D.OverlapCircle(new Vector2(pos.x,pos.y), radius)){			
			pos = this.GetComponentInParent<PlayArea> ().GetRandomCell (); 
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
