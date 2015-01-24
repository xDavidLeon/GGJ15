using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>{
	public Texture2D mapLayout;
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

        DrawMapFromTexture();
        //DrawCircle();
        //DrawFilledCircle();
	}
	
	void Update () {
	    if(eventTimerNow >= eventTimer) // Do level events!
        {
            LavaTiles();
            BoulderTiles();
            eventTimerNow = 0;
        }
        eventTimerNow += Time.deltaTime;
	}

    #region GAMEPLAY
    void LavaTiles()
    {
        int n = Random.Range(5, 10);
        int i = 0;
        int numTries = 0;
        while (i < n && numTries < 20)
        {
            // Get random cube
            Cell c = GetFreeCell();
            c.DangerLava();
            // Apply Lava Event
            i++;
            numTries++;
        }
    }

    void BoulderTiles()
    {
        int n = Random.Range(1, 4);
        int i = 0;
        int numTries = 0;
        while (i < n && numTries < 20)
        {
            // Get random cube
            Cell c = GetFreeCell();
            c.DangerBoulder();
            // Apply Lava Event
            i++;
            numTries++;
        }
    }

    #endregion

    #region CUBE_MANAGEMENT

    void DrawMapFromTexture()
    {
        levelSize = mapLayout.width;
        Color32[] colors = mapLayout.GetPixels32();
        
        for (int i = 0; i < mapLayout.width; i++)
        {
            for (int j = 0; j < mapLayout.height; j++)
            {
                Color c = mapLayout.GetPixel(i, j);
                if (c.r > 0.1f) AddCube(i, j, 0);
            }
        }
    }

    void DrawSquare()
    {
        for (int i = 0; i < levelSize; i++)
        {
            for (int j = 0; j < levelSize; j++)
            {
                AddCube(j, 0, i);
            }
        }
    }

    void DrawCircle()
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
        GameObject cell = GameObject.Instantiate(cellPrefab, new Vector3(col + 0.5f, height, row+0.5f), Quaternion.identity) as GameObject;
        cell.name = "Cell_" + row + "_" + col;
        cell.transform.parent = cellContainer.transform;
        cell.transform.Rotate(0,90*Random.Range(0,10),0);
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

    Cell GetFreeCell(bool stateNormal = true)
    {
        int numTries = 0;

        Cell c = null;
        while (c == null && numTries < 20)
        {
            int row = Random.Range(0, levelSize);
            int col = Random.Range(0, levelSize);
            c = GetCell(row, col);
            if (c != null && c.state != Cell.CELL_STATE.NORMAL) c = null;
            numTries++;
        }
        return c;
    }

    #endregion
}
