using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CELL_STATE
    {
        NORMAL,
        LAVA,
        BOULDER,
        FALL,
        DANGER_LAVA,
        DANGER_FALL,
        DANGER_BOULDER,
        RESTORING
    };

    public CELL_STATE state = CELL_STATE.NORMAL;
    public float timerObjective = 3.0f;
    public float timerNow = 0;
    public GameObject particlesDust;
    public GameObject triggerDeath;
    public GameObject prefabBoulder;

	void Start () 
	{
	
	}
	
	void Update () 
	{
        switch(state)
        {
            case CELL_STATE.NORMAL:
                break;
            case CELL_STATE.RESTORING:
                if (timerNow >= timerObjective)
                    Clear();
                break;
            case CELL_STATE.LAVA:
                if (timerNow >= timerObjective)
                    UndoLava();
                break;
            case CELL_STATE.FALL:
                break;
            case CELL_STATE.DANGER_LAVA:
                if (timerNow >= timerObjective)
                    Lava();
                break;
            case CELL_STATE.DANGER_FALL:
                if (timerNow >= timerObjective)
                    Fall();
                break;
            case CELL_STATE.DANGER_BOULDER:
                if (timerNow >= timerObjective)
                    Boulder();
                break;
            case CELL_STATE.BOULDER:
                if (timerNow >= timerObjective)
                    Clear();
                break;
        }

        timerNow += Time.deltaTime;
	}

	public void Clear()
	{
		if (state == CELL_STATE.NORMAL) return;
        state = CELL_STATE.NORMAL;
		renderer.material.SetColor("_DetailColor", Color.white);
        timerNow = 0;
	}

    public void UndoLava()
    {
        Clear();
        state = CELL_STATE.RESTORING;
        Vector3 targetPos = transform.position + new Vector3(0, 1.1f, 0);
        iTween.MoveTo(this.gameObject, targetPos, 2.0f);
        timerNow = 0;
        triggerDeath.collider.enabled = false;
    }

    public void DangerLava()
    {
        if (state == CELL_STATE.DANGER_LAVA) return;
        state = CELL_STATE.DANGER_LAVA;
        timerNow = 0;

        //GameObject.Instantiate(particlesDust, transform.position + new Vector3(0, 0.5f, 0), particlesDust.transform.rotation);
        Vector3 targetPos = transform.position - new Vector3(0, 0.1f, 0);
        iTween.MoveTo(this.gameObject, targetPos, 1.0f);

        iTween.ShakeRotation(this.gameObject, new Vector3(4,8,4), timerObjective*2);
        //iTween.ShakePosition(this.gameObject, new Vector3(0.015f, 0, 0.015f), timerObjective);
    }

    public void DangerBoulder()
    {
        if (state == CELL_STATE.DANGER_BOULDER) return;
        state = CELL_STATE.DANGER_BOULDER;
        timerNow = 0;

        GameObject boulder = GameObject.Instantiate(prefabBoulder, transform.position + new Vector3(0, 25, 0), prefabBoulder.transform.rotation) as GameObject;
        boulder.transform.parent = GameManager.instance.levelContainer.transform;
        boulder.transform.Rotate(0, 90 * Random.Range(0, 10), 0);
    }

    public void DangerFall()
    {
        if (state == CELL_STATE.DANGER_FALL) return;
        state = CELL_STATE.DANGER_FALL;
        timerNow = 0;

        //GameObject.Instantiate(particlesDust, transform.position + new Vector3(0, 0.5f, 0), particlesDust.transform.rotation);
        Vector3 targetPos = transform.position - new Vector3(0, 0.1f, 0);
        iTween.MoveTo(this.gameObject, targetPos, 1.0f);

        iTween.ShakeRotation(this.gameObject, new Vector3(4, 8, 4), timerObjective * 2);
        //iTween.ShakePosition(this.gameObject, new Vector3(0.015f, 0, 0.015f), timerObjective);
    }

    public void Fall()
    {
        if (state == CELL_STATE.FALL) return;
        state = CELL_STATE.FALL;
        timerNow = 0;

        Vector3 targetPos = transform.position - new Vector3(0, 100, 0);
        iTween.MoveTo(this.gameObject, targetPos, 2.0f);
    }

    public void Lava()
    {
        if (state == CELL_STATE.LAVA) return;
        state = CELL_STATE.LAVA;
        timerNow = 0;

        Vector3 targetPos = transform.position - new Vector3(0,1,0);
        iTween.MoveTo(this.gameObject, targetPos, timerObjective);
        triggerDeath.collider.enabled = true;
    }

    public void Boulder()
    {
        if (state == CELL_STATE.BOULDER) return;
        state = CELL_STATE.BOULDER;
        timerNow = 0;
    }

	public bool HasPlayerOnTop()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		Vector2 pos = GetCellPos();
		foreach (GameObject g in players)
		{
            Vector2 pos2 = GetCellPos(g.transform.position);
			//Vector2 pos2 = g.GetComponent<Player>().GetCellPos();
            if (pos.x == pos2.x && pos.y == pos2.y)
            {
                return true;
            }
		}
		return false;
	}

    public static Vector2 GetCellPos(Vector3 worldPos)
    {
        return new Vector2((int)worldPos.x, (int)worldPos.z);
    }

	public Vector2 GetCellPos()
	{
		return new Vector2((int)transform.position.z,(int)transform.position.x);
	}
}
