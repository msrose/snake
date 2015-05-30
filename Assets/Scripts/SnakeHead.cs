using UnityEngine;
using System.Collections;

public class SnakeHead : MonoBehaviour {

    public GameObject SnakeBodyPrefab;
    public GameObject bodyBegin;
    public GameObject bodyEnd;
    private Direction dir;
    private Direction nextDir;

	bool isDead;

    private int curFrame;

    public static int gridSize = 5;
    public static float bodySep = 1f;
    public static float speed = bodySep/gridSize;

    private Vector3 startPosition;

	// Use this for initialization
	void Start () {
	    /* Start with multiple heads*/
        this.curFrame = 0;
        this.startPosition = this.transform.position;
		dir = Direction.NONE;
		nextDir = Direction.NONE;
		isDead = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			ResetPosition();
			isDead = false;
		}

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

            if (bodyBegin != null) {
                bodyBegin.GetComponent<SnakeBody>().ChangeDirection(this.dir);
            }
            
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
		
        if (bodyBegin != null) {
            bodyBegin.GetComponent<SnakeBody>().BodyUpdate();
        }
	}

    void ResetPosition() {
		this.dir = Direction.NONE;
		this.nextDir = Direction.NONE;
		this.transform.position = this.startPosition;

		bodyBegin.GetComponent<SnakeBody> ().ResetPosition (this.transform.position);
    }
    
    void ExtendBody() {
        Vector3 position;
        GameObject obj;
        Direction d;

        if (bodyEnd == null) {
            position = this.transform.position;
            d = this.dir;
        } else {
            position = this.bodyEnd.transform.position;
            d = this.bodyEnd.GetComponent<SnakeBody>().dir;
        }

        switch (d) {
            case Direction.NORTH:
                position.y -= bodySep;
                break;
            case Direction.WEST:
                position.x += bodySep;
                break;
            case Direction.EAST:
                position.x -= bodySep;
                break;
            case Direction.SOUTH:
                position.y += bodySep;
                break;
            case Direction.NONE:
                break;
            default:
                Debug.Log("Wut");
                break;
        }

        obj = Instantiate(SnakeBodyPrefab, position, Quaternion.identity) as GameObject;
        SnakeBody back = obj.GetComponent<SnakeBody>();
        back.next = null;
        back.dir = d;
        
        if (this.bodyBegin == null) {
            this.bodyBegin = obj;
        } else {
            this.bodyEnd.GetComponent<SnakeBody>().next = obj; 
        }

        this.bodyEnd = obj;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Obstacle")) {
			isDead = true;
		} else if(col.CompareTag("Food")) {
			ExtendBody();
		}
    }
}


