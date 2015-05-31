using UnityEngine;
using System.Collections;

public class SnakeBody : MonoBehaviour {

    public GameObject next;
    public Direction dir;

	// Use this for initialization
	void Start () {
		this.next = null;
		this.transform.localScale = new Vector3(SnakeHead.bodySep, SnakeHead.bodySep);
	}
	
	public void BodyUpdate () {
        Vector3 position = this.transform.position;

        switch (dir) {
            case Direction.NORTH:
                position.y += SnakeHead.speed;
                break;
            case Direction.WEST:
                position.x -= SnakeHead.speed;
                break;
            case Direction.EAST:
                position.x += SnakeHead.speed;
                break;
            case Direction.SOUTH:
                position.y -= SnakeHead.speed;
                break;
            case Direction.NONE:
                break;
            default:
                Debug.Log("Wut");
                break;
        }

        this.transform.position = position;

        if (this.next != null) {
            this.next.GetComponent<SnakeBody>().BodyUpdate();
        }
	}

	public void ResetPosition(Vector3 position) {
		if (this.next != null) {
			this.next.GetComponent<SnakeBody>().ResetPosition(position);
		}
		this.dir = Direction.NONE;
		this.transform.position = position;
	}

	public void DeleteBody(){
		if (this.next != null) {
			this.next.GetComponent<SnakeBody>().DeleteBody();
		}
		Destroy (this.gameObject);
	}

    public void ChangeDirection(Direction dir) {
        if (this.next != null) {
            this.next.GetComponent<SnakeBody>().ChangeDirection(this.dir);
        }
        this.dir = dir;
    }
}
