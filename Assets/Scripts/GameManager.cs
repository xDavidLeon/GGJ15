using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int levelRows = 10;
	public int levelCols = 10;
	private Cell[,] cells;
	public GameObject cellPrefab;
	// Use this for initialization
	void Start () {
		cells = new Cell[levelRows,levelCols];
		GameObject cellContainer = new GameObject();
		cellContainer.name = "Cells";
		for (int i = 0; i < levelRows; i++)
		{
			for (int j = 0; j < levelCols; j++)
			{
				GameObject c = GameObject.Instantiate(cellPrefab, new Vector3(j + 0.5f,-0.5f,i+0.5f),Quaternion.identity) as GameObject;
				c.name = "Cell " + i + "," + j;
				c.transform.parent = cellContainer.transform;
				cells[i,j] = c.GetComponent<Cell>();
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
