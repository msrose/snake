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
		
		WestWall.transform.localPosition = new Vector3(CellSize*0.5f-MidX,0);
		EastWall.transform.localPosition = new Vector3 ((lengthWithWalls - 0.5f)*CellSize-MidX, 0);
		NorthWall.transform.localPosition = new Vector3(0, (widthWithWalls - 0.5f)*CellSize-MidY);
		SouthWall.transform.localPosition = new Vector3(0, CellSize*0.5f-MidY);

		/*Snake.GetComponent<SnakeHead> ().SetSize (CellSize);
		Snake.GetComponent<SnakeHead> ().SetLocalPosition (getLocalPosition(GridLength / 2, GridWidth / 2));
		FoodScript food = Food.GetComponent<FoodScript> ();
		food.RandomizeFood ();
		food.transform.localScale = new Vector3 (CellSize, CellSize);*/
	}

	public Vector3 getLocalPosition(int cellX, int cellY){
		int lengthWithWalls = GridLength + 2;
		int widthWithWalls = GridWidth + 2;
		float MidX = lengthWithWalls * (CellSize) * 0.5f;
		float MidY = widthWithWalls*(CellSize)*0.5f;
		return new Vector3 ((cellX+0.5f)*CellSize - MidX, (cellY+0.5f)*CellSize -MidY);
	}

	public Vector3 GetRandomCell(){
		int cellX = Random.Range(1, GridLength);
		int cellY = Random.Range(1, GridWidth);
		return getLocalPosition(cellX,cellY);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
