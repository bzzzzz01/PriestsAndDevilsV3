using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UserAction
{
    void moveObj(GameObject gameObject);
    bool isWin();
    bool isLose();
    void reset();
    void step();
}

