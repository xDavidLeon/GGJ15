using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {
    public enum CELL_STATE
    {
        NORMAL,
        LAVA,
        BOULDER,
        DANGER_LAVA,
        DANGER_BOULDER,
        RESTORING
    };

    public CELL_STATE state = CELL_STATE.NORMAL;
    public float timerObjective = 3.0f;
    public float timerNow = 0;
    public GameObject triggerLava;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
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
            case CELL_STATE.DANGER_LAVA:
                if (timerNow >= timerObjective)
                    Lava();
                break;
        }

        timerNow += Time.deltaTime;
	}

	void OnCollisionEnter(Collision c)
	{
	}

//	public void Step(Level.TEAM team)
//	{
//		if (isActivated) return;
//		cellTeam = team;
//		Color c = Color.white;
//		switch (team)
//		{
//		case Level.TEAM.BLUE:
//			c = new Color(0.0f/255.0f,76.0f/255.0f,133.0f/255.0f);
//			renderer.material.SetColor("_DetailColor", c);
//			particleSystem.startColor = c;
//			break;
//		case Level.TEAM.GREEN:
//			c = new Color(39.0f/255.0f,105.0f/255.0f,22.0f/255.0f);
//			renderer.material.SetColor("_DetailColor", c);
//			particleSystem.startColor = c;
//			break;
//		case Level.TEAM.RED:
//			c = new Color(98.0f/255.0f,14.0f/255.0f,10.0f/255.0f);
//			renderer.material.SetColor("_DetailColor", c);
//			particleSystem.startColor = c;
//			break;
//		case Level.TEAM.YELLOW:
//			c = new Color(128.0f/255.0f,115.0f/255.0f,44.0f/255.0f);
//			renderer.material.SetColor("_DetailColor", c);
//			particleSystem.startColor = c;
//			break;
//		}
//	}

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
        Vector3 targetPos = transform.position + new Vector3(0, 1, 0);
        iTween.MoveTo(this.gameObject, targetPos, 2.0f);
        timerNow = 0;
        triggerLava.collider.enabled = false;
    }

    public void DangerLava()
    {
        if (state == CELL_STATE.DANGER_LAVA) return;
        state = CELL_STATE.DANGER_LAVA;
        renderer.material.SetColor("_DetailColor", Color.red);
        timerNow = 0;
        triggerLava.collider.enabled = true;
    }

    public void Lava()
    {
        if (state == CELL_STATE.LAVA) return;
        state = CELL_STATE.LAVA;

        Vector3 targetPos = transform.position - new Vector3(0,1,0);
        iTween.MoveTo(this.gameObject, targetPos, 2.0f);
        timerNow = 0;
    }

    //public bool ActivateCell()
    //{
    //    if (isActivated) return false;
    //    particleSystem.Play();
    //    life = maxLife;
    //    if (HasPlayerOnTop()) return false;
    //    isActivated = true;
    //    Vector3 targetPos = transform.position;
    //    targetPos.y = 0.5f;
    //    iTween.MoveTo (this.gameObject, targetPos , 2.0f);
    //    return true;
    //}

    //public void GetHit()
    //{
    //    life -= 1;
    //    if (life <= 0)
    //    {
    //        Restart(false);
    //    }
    //}

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

//    public void Restart(bool restartColor)
//    {
//        if (isActivated) 
//        {
//            particleSystem.Stop();
//            Vector3 targetPos = transform.position;
//            targetPos.y = -0.5f;
//            iTween.MoveTo(this.gameObject,targetPos,0.25f);
//        }
//        if (restartColor)
//        {
////			cellTeam = Level.TEAM.NONE;
//            renderer.material.SetColor("_DetailColor", Color.white);
//        }
//        isActivated = false;
//    }
}
