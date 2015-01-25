using UnityEngine;
using System.Collections;

public class GameManager : MonoSingleton<GameManager>{
    [System.Serializable]
    public struct Score
    {
        public int scoreP1, scoreP2, scoreP3, scoreP4;

        public void Restart()
        {
            scoreP1 = scoreP2 = scoreP3 = scoreP4 = 0;
        }
    };

    public enum PLAY_STATE
    {
        PLAYER_SELECTION,
        PLAY,
        END
    };

    public PLAY_STATE playState = PLAY_STATE.PLAYER_SELECTION;

    [Header("Map")]
	public Texture2D mapLayout;
	public int levelSize = 13;
	private Cell[,] cells;
	public GameObject cellPrefab;
    private GameObject cellContainer;

    [Header("Game")]
    public Score scores;
    public GameObject playerSelection;
    public GameObject canvasPlay;
    public GameObject playerPrefab;
    public GameObject[] players;
    [HideInInspector]
    public GameObject levelContainer;

    [Header("Times")]
    public float timer = 0;
    public float startCountdownTime = 10.0f;
    public float roundTime = 60.0f;
    public float eventTimer = 3.0f;
    public float eventTimerNow = 0.0f;

    [Header("UI")]
    public GameObject[] playerCameras;
    public GameObject[] pressStart;

    [HideInInspector]
    public Vector3 center;

	void Start () {
        cells = new Cell[levelSize, levelSize];
        cellContainer = new GameObject("Cells");
        levelContainer = new GameObject("LevelContainer");
        center = new Vector3(6f, 0.5f, 6f);

        RestartLevel();
	}

    void RestartLevel()
    {
        foreach (Transform t in levelContainer.transform) GameObject.Destroy(t.gameObject);
        foreach (GameObject g in players) GameObject.Destroy(g);
        for (int i = 0; i < levelSize; i++)
            for (int j = 0; j < levelSize; j++ )
            {
                if (cells[i, j] == null) continue;
                GameObject.Destroy(cells[i,j].gameObject);
                cells[i,j] = null;
            }

        scores.Restart();

        DrawMapFromTexture();

        SetState(PLAY_STATE.PLAYER_SELECTION);
    }

    void SetState(PLAY_STATE state)
    {
        playState = state;
        timer = 0;
        switch (state)
        {
            case PLAY_STATE.PLAYER_SELECTION:
                canvasPlay.SetActive(false);
                playerSelection.SetActive(true);
                pressStart[0].SetActive(true);
                playerCameras[0].SetActive(true);
                playerCameras[0].camera.cullingMask = 0;
                pressStart[1].SetActive(true);
                playerCameras[1].SetActive(true);
                playerCameras[1].camera.cullingMask = 0;
                pressStart[2].SetActive(true);
                playerCameras[2].SetActive(true);
                playerCameras[2].camera.cullingMask = 0;
                pressStart[3].SetActive(true);
                playerCameras[3].SetActive(true);
                playerCameras[3].camera.cullingMask = 0;

                break;
            case PLAY_STATE.PLAY:
                canvasPlay.SetActive(true);
                playerSelection.SetActive(false);
                foreach (GameObject g in players) g.GetComponent<PlayerController>().enabled = true;
                break;
            case PLAY_STATE.END:
                canvasPlay.SetActive(false);
                playerSelection.SetActive(true);
                break;
        }
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.R)) RestartLevel();
        players = GameObject.FindGameObjectsWithTag("Player");
        switch(playState)
        {
            case PLAY_STATE.PLAYER_SELECTION:
                if (Input.GetButtonDown("Start01") && pressStart[0].activeInHierarchy )
                {
                    pressStart[0].SetActive(false);
                    playerCameras[0].camera.cullingMask = Camera.main.cullingMask;
                    GameObject cat = GameObject.Instantiate(playerPrefab, new Vector3(3.39f, 0.325f, 6), Quaternion.Euler(0, 180, 0)) as GameObject;
                    cat.name = "Player01";
                    cat.GetComponent<PlayerController>().playerNumber = PlayerController.PLAYER_NUMBER.PLAYER_1;
                    cat.GetComponent<PlayerController>().Init();
                    cat.GetComponent<PlayerController>().enabled = false;
                    cat.transform.parent = levelContainer.transform;
                }
                else if (Input.GetButtonDown("Start02") && pressStart[1].activeInHierarchy)
                {
                    pressStart[1].SetActive(false);
                    playerCameras[1].SetActive(true);
                    playerCameras[1].camera.cullingMask = Camera.main.cullingMask;
                    GameObject cat = GameObject.Instantiate(playerPrefab, new Vector3(5.39f, 0.325f, 6), Quaternion.Euler(0, 180, 0)) as GameObject;
                    cat.GetComponent<PlayerController>().playerNumber = PlayerController.PLAYER_NUMBER.PLAYER_2;
                    cat.GetComponent<PlayerController>().Init();
                    cat.name = "Player02";

                    cat.GetComponent<PlayerController>().enabled = false;
                    cat.transform.parent = levelContainer.transform;
                }
                else if (Input.GetButtonDown("Start03") && pressStart[2].activeInHierarchy)
                {
                    pressStart[2].SetActive(false);
                    playerCameras[2].camera.cullingMask = Camera.main.cullingMask;
                    GameObject cat = GameObject.Instantiate(playerPrefab, new Vector3(7.39f, 0.325f, 6), Quaternion.Euler(0, 180, 0)) as GameObject;
                    cat.GetComponent<PlayerController>().playerNumber = PlayerController.PLAYER_NUMBER.PLAYER_3;
                    cat.GetComponent<PlayerController>().Init();
                    cat.name = "Player03";

                    cat.GetComponent<PlayerController>().enabled = false;
                    cat.transform.parent = levelContainer.transform;
                }
                else if (Input.GetButtonDown("Start04") && pressStart[3].activeInHierarchy)
                {
                    pressStart[3].SetActive(false);
                    playerCameras[3].camera.cullingMask = Camera.main.cullingMask;
                    GameObject cat = GameObject.Instantiate(playerPrefab, new Vector3(9.39f, 0.325f, 6), Quaternion.Euler(0, 180, 0)) as GameObject;
                    cat.GetComponent<PlayerController>().playerNumber = PlayerController.PLAYER_NUMBER.PLAYER_4;
                    cat.GetComponent<PlayerController>().Init();
                    cat.name = "Player04";

                    cat.GetComponent<PlayerController>().enabled = false;
                    cat.transform.parent = levelContainer.transform;
                }

                if (players.Length > 1)
                {
                    timer += Time.deltaTime;
                    if (timer >= startCountdownTime) SetState(PLAY_STATE.PLAY);
                }
                else timer = 0;

                break;
            case PLAY_STATE.PLAY:

                if(eventTimerNow >= eventTimer) // Do level events!
                {
                    LavaTiles();
                    BoulderTiles();
                    eventTimerNow = 0;
                }
                eventTimerNow += Time.deltaTime;
                timer += Time.deltaTime;
                if (timer >= roundTime) SetState(PLAY_STATE.END);
                break;
            case PLAY_STATE.END:
                if (Input.GetButtonDown("Start01") || Input.GetButtonDown("Start02") || Input.GetButtonDown("Start03") || Input.GetButtonDown("Start04"))
                {
                    SetState(PLAY_STATE.PLAYER_SELECTION);
                }
                break;
        }
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
        //Color32[] colors = mapLayout.GetPixels32();
        
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
