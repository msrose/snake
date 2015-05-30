using UnityEngine;
using System.Collections;

public class SnakeHead : MonoBehaviour {

    public GameObject bodyBegin;
    public GameObject bodyEnd;
    private Direction dir = Direction.NORTH;
    private Direction nextDir;


    private int curFrame;

    public int gridSize = 5;
    public static float speed = 0.25F;
    public static float bodySep = 1.25f;

    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
	    /* Start with multiple heads*/
        this.curFrame = 0;
        this.startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        curFrame++;

        if (Input.GetKeyDown(KeyCode.W) && this.dir != Direction.SOUTH) {
            this.nextDir = Direction.NORTH;
        }
        
        else if (Input.GetKeyDown(KeyCode.A) && this.dir != Direction.EAST) {
            this.nextDir = Direction.WEST;
        }
        
        else if (Input.GetKeyDown(KeyCode.D) && this.dir != Direction.WEST) {
            this.nextDir = Direction.EAST;
        }
        
        else if (Input.GetKeyDown(KeyCode.S) && this.dir != Direction.NORTH) {
            this.nextDir = Direction.SOUTH;
        }
        
        else if (Input.GetKeyDown(KeyCode.Space)) {
            this.nextDir = Direction.NONE;
        }

        else if (Input.GetKeyDown(KeyCode.R)) {
            ResetPosition();
        }

        if (curFrame == gridSize) {
            curFrame = 0;
            bodyBegin.GetComponent<SnakeBody>().ChangeDirection(this.dir);
            this.dir = this.nextDir;
        }

        Vector3 position = this.transform.position;

        switch (this.dir) {
            case Direction.NORTH:
                position.y += speed;
                break;
            case Direction.WEST:
                position.x -= speed;
                break;
            case Direction.EAST:
                position.x += speed;
                break;
            case Direction.SOUTH:
                position.y -= speed;
                break;
            case Direction.NONE:
                break;
            default:
                Debug.Log("Wut");
                break;
        }

        this.transform.position = position;


        bodyBegin.GetComponent<SnakeBody>().BodyUpdate();
	}

    void ResetPosition() {
        this.transform.position = this.startPosition;
    }
    /*
    void extendBody() {
        Instantiate()
        SnakeBody newPart = new SnakeBody();
        bodyEnd.next = blah;
        bodyEnd.End = blah;
    }
    *

    void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("Collision!");
        this.nextDir = Direction.NONE;
    }
        */

    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Collision Detected");
    }
}


