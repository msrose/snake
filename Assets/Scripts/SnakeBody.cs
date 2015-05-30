using UnityEngine;
using System.Collections;

public class SnakeBody : MonoBehaviour {

    public GameObject next;
    public Direction dir;

	// Use this for initialization
	void Start () {
        this.next = null;
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
			this.next.ResetPosition(position);
		}
		this.transform.position = position;
	}

    public void ChangeDirection(Direction dir) {
        if (this.next != null) {
            this.next.GetComponent<SnakeBody>().ChangeDirection(this.dir);
        }
        this.dir = dir;
    }
}
