using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {
	
	void Start(){
		float radius = this.GetComponent<BoxCollider2D> ().size.x;
		Vector3 pos = new Vector3 (Random.Range(-9, 9), Random.Range(-9, 9), 0);
		while (Physics2D.OverlapCircle(new Vector2(pos.x,pos.y), radius)){
			pos.x = Random.Range(-9, 9);
			pos.y = Random.Range(-9, 9);
		}
		this.transform.position = pos;
	}
}
