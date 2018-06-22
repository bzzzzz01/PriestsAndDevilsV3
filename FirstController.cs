using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, SceneController, UserAction {

    public float speed = 10;
    GameObject left, right, river, boat;
    int boatSeat;
    int boatPos;//1:Left 2:Right
    bool boatMove;//0:stop 1:move
    GameObject[] priest = new GameObject[3];
    GameObject[] devil = new GameObject[3];
    int[] priestPos = new int[3];//1:Left 2:Right 3:BoatLeft 4:BoatRight
    int[] devilPos = new int[3];//1:Left 2:Right 3:BoatLeft 4:BoatRight
    int priestLeft, priestRight, devilLeft, devilRight;

    void Awake () {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadResources();
    }
    void Update()
    {
        //move the boat
        if(boatMove)
        {
            if(boatPos == 1)
            {
                boat.transform.position = Vector3.MoveTowards(boat.transform.position, new Vector3(1, 0.2f, 0), speed * Time.deltaTime);
                if (boat.transform.position == new Vector3(1, 0.2f, 0))
                {
                    boatMove = false;
                    boatPos = 2;
                }
            }
            else if (boatPos == 2)
            {
                boat.transform.position = Vector3.MoveTowards(boat.transform.position, new Vector3(-1, 0.2f, 0), speed * Time.deltaTime);
                if (boat.transform.position == new Vector3(-1, 0.2f, 0))
                {
                    boatMove = false;
                    boatPos = 1;
                }
            }
        }
     }
        

    public void LoadResources()
    {
        left = Instantiate(Resources.Load("Coast") as GameObject);
        right = Instantiate(Resources.Load("Coast") as GameObject);
        river = Instantiate(Resources.Load("River") as GameObject);
        boat = Instantiate(Resources.Load("Boat") as GameObject);
        left.transform.name = "Left";
        right.transform.name = "Right";
        river.transform.name = "River";
        boat.transform.name = "Boat";
        left.transform.position = new Vector3(-4, 0.4f, 0);
        right.transform.position = new Vector3(4, 0.4f, 0);
        river.transform.position = new Vector3(0, 0, 0);
        boat.transform.position = new Vector3(-1, 0.2f, 0);
        for (int i = 0; i < 3; i++)
        {
            priest[i] = Instantiate(Resources.Load("Priest") as GameObject);
            devil[i] = Instantiate(Resources.Load("devil") as GameObject);
            priest[i].transform.name = "Priest";
            devil[i].transform.name = "Devil";
            priestPos[i] = 1;
            devilPos[i] = 1;
        }
        boatSeat = 0;
        boatPos = 1;
        setPos();
    }

    //set gameObjects' position
    void setPos()
    {
        if (boatSeat == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (priestPos[i] == 4)
                    priestPos[i] = 3;
                if (devilPos[i] == 4)
                    devilPos[i] = 3;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (priestPos[i] == 1)
            {
                priest[i].transform.parent = null;
                priest[i].transform.position = new Vector3(-(i * 0.5f + 4.5f), 1.1f, 0);
            }
            if (priestPos[i] == 2)
            {
                priest[i].transform.parent = null;
                priest[i].transform.position = new Vector3((i * 0.5f + 4.5f), 1.1f, 0);
            }
            if (priestPos[i] == 3)
            {
                priest[i].transform.parent = boat.transform;
                priest[i].transform.localPosition = new Vector3(-0.2f, 1.4f, 0);
            }
            if (priestPos[i] == 4)
            {
                priest[i].transform.parent = boat.transform;
                priest[i].transform.localPosition = new Vector3(0.2f, 1.4f, 0);
            }

            if (devilPos[i] == 1)
            {
                devil[i].transform.parent = null;
                devil[i].transform.position = new Vector3(-(i * 0.5f + 2.5f), 1.1f, 0);
            }
            if (devilPos[i] == 2)
            {
                devil[i].transform.parent = null;
                devil[i].transform.position = new Vector3((i * 0.5f + 2.5f), 1.1f, 0);
            }
            if (devilPos[i] == 3)
            {
                devil[i].transform.parent = boat.transform;
                devil[i].transform.localPosition = new Vector3(-0.2f, 1.4f, 0);
            }
            if (devilPos[i] == 4)
            {
                devil[i].transform.parent = boat.transform;
                devil[i].transform.localPosition = new Vector3(0.2f, 1.4f, 0);
            }
        }
    }

    //count the number of each coast
    public void count()
    {
        priestLeft = priestRight = devilLeft = devilRight = 0;
        for (int i = 0; i < 3; i++)
        {
            if (priestPos[i] == 1 ||
                priestPos[i] == 3 && boatPos == 1 ||
                priestPos[i] == 4 && boatPos == 1)
                priestLeft++;
            if (priestPos[i] == 2 ||
                priestPos[i] == 3 && boatPos == 2 ||
                priestPos[i] == 4 && boatPos == 2)
                priestRight++;
            if (devilPos[i] == 1 ||
                devilPos[i] == 3 && boatPos == 1 ||
                devilPos[i] == 4 && boatPos == 1)
                devilLeft++;
            if (devilPos[i] == 2 ||
                devilPos[i] == 3 && boatPos == 2 ||
                devilPos[i] == 4 && boatPos == 2)
                devilRight++;
        }
    }

    //move gameObject
    public void moveObj(GameObject gameObject)
    {
        if (boatMove) return;
        if (gameObject.name == "Boat")
        {
            if (boatSeat > 0)
                boatMove = true;
        }
        if (boatSeat < 2)
        {
            for(int i = 0; i < 3; i++)
            {
                if (gameObject == priest[i] && priestPos[i] == boatPos)
                {
                    priestPos[i] = boatSeat + 3;
                    boatSeat++;
                }
                if (gameObject == devil[i] && devilPos[i] == boatPos)
                {
                    devilPos[i] = boatSeat + 3;
                    boatSeat++;
                }
            }
        }
        if(gameObject.transform.parent == boat.transform)
        {
            for (int i = 0; i < 3; i++)
            {
                if (gameObject == priest[i])
                {
                    priestPos[i] = boatPos;
                    boatSeat--;
                }
                if (gameObject == devil[i])
                {
                    devilPos[i] = boatPos;
                    boatSeat--;
                }
            }
        }
        setPos();
    }
    public bool isWin()
    {
        count();
        return priestRight == 3 && devilRight == 3;
    }
    public bool isLose()
    {
        count();
        return priestLeft > 0 && priestLeft < devilLeft ||
               priestRight > 0 && priestRight < devilRight;
    }
    public void reset()
    {
        if (boatMove) return;
        boat.transform.position = new Vector3(-1, 0.2f, 0);
        for (int i = 0; i < 3; i++)
        {
            priestPos[i] = 1;
            devilPos[i] = 1;
        }
        boatSeat = 0;
        boatPos = 1;
        setPos();
    }

    public void step()
    {
        if (isWin()) return;
        if (isLose()) return;

        for (int i = 0; i < 3; i++)
        {
            if (priestPos[i] == 3 || priestPos[i] == 4)
                moveObj(priest[i]);
            if (devilPos[i] == 3 || devilPos[i] == 4)
                moveObj(devil[i]);
        }

        int p = priestLeft, d = devilLeft;
        bool b = (boatPos == 1);
        if (p == 2 && d == 2 && !b)
        {
            for (int i = 0; i < 3; i++)
            {
                moveObj(priest[i]);
            }
        }
        else if (p == 3 && d == 1 && b ||
                 p == 2 && d == 2 && b)
        {
            for (int i = 0; i < 3; i++)
            {
                moveObj(priest[i]);
            }
        }
        else if (p == 3 && d == 0 && !b ||
                 p == 3 && d == 2 && !b ||
                 p == 0 && d == 1 && !b ||
                 p == 0 && d == 2 && !b)
        {
            for (int i = 0; i < 3; i++)
            {
                if(boatSeat<1)
                    moveObj(devil[i]);
            }
        }
        else if (p == 3 && d == 1 && !b ||
                 p == 3 && d == 2 && b ||
                 p == 0 && d == 2 && b ||
                 p == 0 && d == 3 && b)
        {
            for (int i = 0; i < 3; i++)
            {
                moveObj(devil[i]);
            }
        }
        else if (p == 1 && d == 1 && !b ||
                 p == 3 && d == 3 && b)
        {
            for (int i = 0; i < 3; i++)
            {
                if (boatSeat < 1)
                    moveObj(priest[i]);
            }
            for (int i = 0; i < 3; i++)
            {
                moveObj(devil[i]);
            }
        }

        moveObj(boat);
    }


}
