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
	public InputField nameInputField;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public KeyCode goFast;
	public KeyCode goSlow;
	public int cellX = 0;
	public int cellY = 0;


	public Sprite[] sprites;

	public GameObject map;

    private Direction dir;
    private Direction nextDir;
	private int foodCount;
	private int highScore;
	private int lastScore;

	public bool isDead;
	bool speedUp;
	bool slowUp;
	bool enterName;

    private int curFrame;

    public int gridSize;
    public static float bodySep = 1f;
    public float speed;

    private Vector3 startPosition;
	
	
	public void SetSize(float NewSize){
		this.transform.localScale = new Vector3 (NewSize, NewSize);
		SnakeHead.bodySep = NewSize;
		this.speed = bodySep/gridSize;
		
	}

	public void SetLocalPosition(Vector3 pos){
		this.transform.localPosition = pos;
	}

	// Use this for initialization
	void Start () {
	    /* Start with multiple heads*/
        this.curFrame = 0;
		this.gridSize = 5;
		this.speed = bodySep / this.gridSize;
		dir = Direction.NONE;
		nextDir = Direction.NONE;
		isDead = false;
		lastScoreText.gameObject.SetActive (false);
        
        if (nameInputField!= null){
            nameInputField.gameObject.SetActive (false);
        }
		foodCount = 0;
		highScore = 0;
		lastScore = 0;
		UpdateScores ();
		PlayArea pa = (PlayArea)map.GetComponent<PlayArea> ();
		float newSize = pa.CellSize;
		SetSize (newSize);
		if (cellX <= 0) {
			cellX = pa.GridLength / 2;
		}else if(cellX > pa.GridLength){
			cellX = pa.GridLength;
		}
		if(cellY <=0 || cellY > pa.GridWidth){
			cellY = pa.GridWidth / 2;
		}else if(cellY > pa.GridWidth){
			cellY = pa.GridWidth;
		}

		SetLocalPosition (pa.getLocalPosition(cellX, cellY));
		this.startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			ResetPosition ();
			lastScore = foodCount;
			foodCount = 0;
			UpdateScores ();
			isDead = false;
			lastScoreText.gameObject.SetActive (true);
            
            if (nameInputField!= null){
                enterName = true;
                nameInputField.gameObject.SetActive(true);
                nameInputField.Select();
            }
		}
        if (enterName && nameInputField != null) {
			if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)){
				if(nameInputField.text != ""){
					//Send to website
					enterName = false;
					nameInputField.text = "";
					nameInputField.gameObject.SetActive(false);
				}
			}
		} else {
            curFrame++;

            if (Input.GetKey (goFast)) {
                speedUp = true;
                slowUp = false;
            } else if (Input.GetKey (goSlow)) {
                speedUp = false;
                slowUp = true;
            } else {
                speedUp = false;
                slowUp = false;
            }

            if (Input.anyKeyDown) {
				lastScoreText.gameObject.SetActive (false);
			}
            if (Input.GetKeyDown (up) && this.dir != Direction.SOUTH) {
                this.nextDir = Direction.NORTH;
            } else if ((Input.GetKeyDown (left)) && this.dir != Direction.EAST) {
                this.nextDir = Direction.WEST;
            } else if ((Input.GetKeyDown (right)) && this.dir != Direction.WEST) {
                this.nextDir = Direction.EAST;
            } else if ((Input.GetKeyDown (down)) && this.dir != Direction.NORTH) {
                this.nextDir = Direction.SOUTH;
            }

            if (curFrame == gridSize) {
                curFrame = 0;

                if(speedUp){
                    gridSize = 3;
                    speed = bodySep/gridSize;
                } else if (slowUp){
                    gridSize = 7;
                    speed = bodySep/gridSize;
                }else{
                    gridSize = 5;
                    speed  = bodySep/gridSize;
                }

                if (bodyBegin != null) {
                    bodyBegin.GetComponent<SnakeBody> ().UpdateSpeed (speed);
                    bodyBegin.GetComponent<SnakeBody> ().ChangeDirection (this.dir);
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
				this.transform.rotation = Quaternion.Euler (0, 0, 90);
				break;
			case Direction.EAST:
				position.x += speed;
				this.transform.rotation = Quaternion.Euler (0, 0, 270);
				break;
			case Direction.SOUTH:
				position.y -= speed;
				this.transform.rotation = Quaternion.Euler (0, 0, 180);
				break;
			case Direction.NONE:
				break;
			default:
				Debug.Log ("Wut");
				break;
			}

			this.transform.position = position;
		
			if (bodyBegin != null) {
				bodyBegin.GetComponent<SnakeBody> ().BodyUpdate ();
			}
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
		back.UpdateSpeed (this.speed);
        
        if (this.bodyBegin == null) {
            this.bodyBegin = obj;
        } else {
            this.bodyEnd.GetComponent<SnakeBody>().next = obj; 
        }
        this.bodyEnd = obj;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag ("Obstacle")) {
			isDead = true;
		} else if (col.CompareTag ("Food")) {
			col.GetComponent<FoodScript> ().RandomizeFood ();
			ExtendBody ();
			foodCount++;
			if (foodCount > highScore) {
				highScore = foodCount;
			}
			UpdateScores ();
		} else if (col.CompareTag ("Player")) {
			int len = col.GetComponent<SnakeHead>().getLength();
			if(this.getLength() >= len){
				isDead = true;
			}
		}
    }

	void UpdateScores() {
		scoreText.text = "Score: " + foodCount.ToString();
		lastScoreText.text = "Your Score: " + lastScore.ToString ();
		highScoreText.text = "Hight Score: " + highScore.ToString();
	}

	public int getLength(){
		return foodCount;
	}
}


