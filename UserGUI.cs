using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{

    private UserAction action;

    void Start()
    {
        action = Director.getInstance().currentSceneController as UserAction;
    }


    void Update()
    {
        //get the chosen gameObject
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if(hit.collider.gameObject.name=="Boat" ||
                   hit.collider.gameObject.name == "Priest" ||
                   hit.collider.gameObject.name == "Devil")
                action.moveObj(hit.collider.gameObject);
            }
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 80, 60, 30), "AIstep"))
            action.step();
        if (GUI.Button(new Rect(Screen.width / 2 - 30, Screen.height / 2 + 110, 60, 30), "reset"))
            action.reset();
        if (action.isWin())
            GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 50, 80, 30), "You Win!");
        if (action.isLose())
            GUI.Label(new Rect(Screen.width / 2 - 25, Screen.height / 2 + 50, 80, 30), "You Lose!");
    }
}
