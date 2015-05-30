using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnakeHead : MonoBehaviour {

    public GameObject SnakeBodyPrefab;
	public GameObject FoodPrefab;
    public GameObject bodyBegin;
    public GameObject bodyEnd;
	public Text scoreText;
	public Text highScoreText;
	public Text lastScoreText;

    private Direction dir;
    private Direction nextDir;
	private int foodCount;
	private int highScore;
	private int lastScore;

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
		lastScoreText.gameObject.SetActive (false);
		foodCount = 0;
		highScore = 0;
		lastScore = 0;
		UpdateScores ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			ResetPosition();
			lastScore = foodCount;
			foodCount = 0;
			UpdateScores();
			isDead = false;
			lastScoreText.gameObject.SetActive(true);
		}

        curFrame++;

		if (Input.anyKeyDown) {
			lastScoreText.gameObject.SetActive (false);
		}

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && this.dir != Direction.SOUTH) {
            this.nextDir = Direction.NORTH;
        }
        
		else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && this.dir != Direction.EAST) {
            this.nextDir = Direction.WEST;
        }
        
		else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && this.dir != Direction.WEST) {
            this.nextDir = Direction.EAST;
        }
        
		else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && this.dir != Direction.NORTH) {
            this.nextDir = Direction.SOUTH;
        }
        
//        else if (Input.GetKeyDown(KeyCode.Space)) {
//            this.nextDir = Direction.NONE;
//        }

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
                this.transform.rotation = Quaternion.identity;
                break;
            case Direction.WEST:
                position.x -= speed;
                this.transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.EAST:
                position.x += speed;
                this.transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case Direction.SOUTH:
                position.y -= speed;
                this.transform.rotation = Quaternion.Euler(0, 0, 180);
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

		//bodyBegin.GetComponent<SnakeBody> ().ResetPosition (this.transform.position);
		if (bodyBegin != null && bodyBegin.GetComponent<SnakeBody> () != null) {
			bodyBegin.GetComponent<SnakeBody> ().DeleteBody ();
		}
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

	void GenerateFood(){
		Vector3 position = new Vector3 (Random.Range(-9, 9), Random.Range(-9, 9), 0);
		Instantiate(FoodPrefab, position, Quaternion.identity);
	}

    void OnTriggerEnter2D(Collider2D col) {
        if(col.CompareTag("Obstacle")) {
			isDead = true;
		} else if(col.CompareTag("Food")) {
			Destroy(col.gameObject);
			GenerateFood();
			ExtendBody();
			foodCount++;
			if(foodCount > highScore) {
				highScore = foodCount;
			}
			UpdateScores();
		}
    }

	void UpdateScores() {
		scoreText.text = "Score: " + foodCount.ToString();
		lastScoreText.text = "Your Score: " + lastScore.ToString ();
		highScoreText.text = "Hight Score: " + highScore.ToString();
	}
}


