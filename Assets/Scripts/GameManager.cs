using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int levelSize = 13;
	private Cell[,] cells;
	public GameObject cellPrefab;
    GameObject cellContainer;

	void Start () {
        cells = new Cell[levelSize, levelSize];
        cellContainer = new GameObject();
		cellContainer.name = "Cells";

        DrawFilledCircle();
	}
	
	void Update () {
	
	}

    public void DrawSquare()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                AddCube(j, 0, i);
            }
        }
    }

    public void DrawCircle(int x0, int y0, int radius)
    {
        int x = radius;
        int y = 0;
        int radiusError = 1 - x;

        while (x >= y)
        {

            AddCubes(-x + x0, x + x0, y + y0, 0);
            AddCubes(-y + x0, y + x0, x + y0, 0);
            AddCubes(-x + x0, x + x0, -y + y0, 0);
            AddCubes(-y + x0, y + x0, -x + y0, 0);

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

    public void DrawFilledCircle()
    {
        float cx = (float)levelSize / 2.0f;
        float cy = (float)levelSize / 2.0f;
        float rad = (float)levelSize / 2.0f;

        for(int x=0;x<levelSize;x++)
            for(int y=0;y<levelSize;y++) {
 
             float dx = x-cx, dy=y-cy;
             float dist = Mathf.Sqrt(dx*dx+dy*dy);

             if (dist < rad) AddCube(y, x, 0);
            }
    }

    public void AddCube(int col, int row, int height)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab, new Vector3(col, height, row), Quaternion.identity) as GameObject;
        cell.name = "Cell_" + row + "_" + col;
        cell.transform.parent = cellContainer.transform;
        cells[row, col] = cell.GetComponent<Cell>();
    }

    public void AddCubes(int col0, int col1, int row, int height)
    {
        for (int i = col0; i < col1; i++)
        {
            AddCube(i, row, height);
        }
    }
}
