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
        //for (int i = 0; i < levelRows; i++)
        //{
        //    for (int j = 0; j < levelCols; j++)
        //    {
        //        GameObject c = GameObject.Instantiate(cellPrefab, new Vector3(j + 0.5f,-0.5f,i+0.5f),Quaternion.identity) as GameObject;
        //        c.name = "Cell " + i + "," + j;
        //        c.transform.parent = cellContainer.transform;
        //        cells[i,j] = c.GetComponent<Cell>();
        //    }
        //}

        DrawCircle(0, 0, 13);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DrawCircle(int x0, int y0, int radius)
    {
        int x = radius;
        int y = 0;
        int radiusError = 1 - x;

        while (x >= y)
        {
            AddCube(x + x0, 0, y + y0);
            AddCube(y + x0, 0, x + y0);
            AddCube(-x + x0, 0, y + y0);
            AddCube(-y + x0, 0, x + y0);
            AddCube(-x + x0, 0, -y + y0);
            AddCube(-y + x0, 0, -x + y0);
            AddCube(x + x0, 0, -y + y0);
            AddCube(y + x0, 0, -x + y0);
            y++;
            if (radiusError < 0)
            {
                radiusError += 2 * y + 1;
            }
            else
            {
                x--;
                radiusError += 2 * (y - x + 1);
            }
        }
    }

    public void AddCube(int x, int y, int z)
    {
        GameObject.Instantiate(cellPrefab, new Vector3(x, y, z), Quaternion.identity);
    }
}
