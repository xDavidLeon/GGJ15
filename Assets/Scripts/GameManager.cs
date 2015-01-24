using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public int levelSize = 13;
	private Cell[,] cells;
	public GameObject cellPrefab;
    private GameObject cellContainer;

    public float eventTimer = 3.0f;
    public float eventTimerNow = 0.0f;

	void Start () {
        cells = new Cell[levelSize, levelSize];
        cellContainer = new GameObject();
		cellContainer.name = "Cells";

        DrawCircle();
        //DrawFilledCircle();
	}
	
	void Update () {
	    if(eventTimerNow >= eventTimer) // Do level events!
        {
            LavaTiles();
            eventTimerNow = 0;
        }
        eventTimerNow += Time.deltaTime;
	}

    #region GAMEPLAY
    void LavaTiles()
    {
        int n = Random.Range(5, 10);
        int i = 0;
        while (i < n)
        {
            // Get random cube
            Cell c = GetFreeCell();
            c.DangerLava();
            // Apply Lava Event
            i++;
        }
    }

    #endregion

    #region CUBE_MANAGEMENT

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

    public void DrawCircle()
    {
        for(int x=0;x<levelSize;x++)
           for(int y=0;y<levelSize;y++) {
 
             float dx = x-levelSize/2.0f, dy=y-levelSize/2.0f;
             float dist = Mathf.Sqrt(dx*dx+dy*dy);

             if (dist < levelSize / 2.0f) AddCube(x, y, 0);
             // or  if(dx*dx+dy*dy<rad*rad)
           }
    }

    public void DrawFilledCircle()
    {
        int x, y, px, nx, py, ny, d;
        int r = levelSize/2;
        int cx = levelSize/2;
        int cy = levelSize/2;

        for (x = 0; x <= r; x++)
        {
            d = (int)(Mathf.Sqrt(r * r - x * x));
            for (y = 0; y <= d; y++)
            {
                px = cx + x;
                nx = cx - x;
                py = cy + y;
                ny = cy - y;

                AddCube(px, py, 0);
                AddCube(nx, py, 0);

                AddCube(px, ny, 0);
                AddCube(nx, ny, 0);

                //tex.SetPixel(px, py, col);
                //tex.SetPixel(nx, py, col);

                //tex.SetPixel(px, ny, col);
                //tex.SetPixel(nx, ny, col);

            }
        } 
    }

    public void AddCube(int col, int row, int height)
    {
        GameObject cell = GameObject.Instantiate(cellPrefab, new Vector3(col + 0.5f, height+0.5f, row+0.5f), Quaternion.identity) as GameObject;
        cell.name = "Cell_" + row + "_" + col;
        cell.transform.parent = cellContainer.transform;
        cells[row, col] = cell.GetComponent<Cell>();
    }

    public void AddCube(float col, float row, float height)
    {
        AddCube((int)col, (int)row, (int)height);
    }

    public void AddCubes(int col0, int col1, int row, int height)
    {
        for (int i = col0; i < col1; i++)
        {
            AddCube(i, row, height);
        }
    }

    public Cell GetCell(int row, int col)
    {
        return cells[row, col];
    }

    public Cell GetCell(Vector2 pos)
    {
        return cells[(int)pos.x, (int)pos.y];
    }

    Cell GetFreeCell()
    {
        Cell c = null;
        while (c == null)
        {
            int row = Random.Range(0, levelSize);
            int col = Random.Range(0, levelSize);
            c = GetCell(row, col);
        }
        return c;
    }

    #endregion
}
