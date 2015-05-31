using UnityEngine;
using System.Collections;

public class PlayArea : MonoBehaviour {
	public int GridLength;
	public int GridWidth;
	public float CellSize;
	public GameObject WestWall;
	public GameObject EastWall;
	public GameObject NorthWall;
	public GameObject SouthWall;
	public GameObject Snake;
	public GameObject Food;

	// Use this for initialization
	void Start () {
		int lengthWithWalls = GridLength + 2;
		int widthWithWalls = GridWidth + 2;
		float MidX = lengthWithWalls * (CellSize) * 0.5f;
		float MidY = widthWithWalls*(CellSize)*0.5f;
		WestWall.transform.localScale = new Vector3(CellSize, widthWithWalls*CellSize);
		EastWall.transform.localScale = new Vector3(CellSize,widthWithWalls*CellSize);
		NorthWall.transform.localScale = new Vector3(lengthWithWalls*CellSize,CellSize);
		SouthWall.transform.localScale = new Vector3(lengthWithWalls*CellSize,CellSize);
		
		WestWall.transform.localPosition = new Vector3(CellSize*0.5f,MidY);
		EastWall.transform.localPosition = new Vector3 ((lengthWithWalls - 0.5f)*CellSize, MidY);
		NorthWall.transform.localPosition = new Vector3(MidX, (widthWithWalls - 0.5f)*CellSize);
		SouthWall.transform.localPosition = new Vector3(MidX, CellSize*0.5f);

		Snake.GetComponent<SnakeHead> ().SetSize (CellSize);
		Snake.GetComponent<SnakeHead> ().SetLocalPosition (getLocalPosition(GridLength / 2, GridWidth / 2));
		FoodScript food = Food.GetComponent<FoodScript> ();
		food.RandomizeFood ();
		food.transform.localScale = new Vector3 (CellSize, CellSize);
	}

	public Vector3 getLocalPosition(int cellX, int cellY){
		return new Vector3 ((cellX+0.5f)*CellSize, (cellY+0.5f)*CellSize);
	}

	public Vector3 GetRandomCell(){
		int cellX = Random.Range(1, GridLength);
		int cellY = Random.Range(1, GridWidth);
		return new Vector3 ((cellX+0.5f)*CellSize, (cellY+0.5f)*CellSize);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
