using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    private Snake next;  //Kinda LinkedList

    public void setNext(Snake IN)
    {
        next = IN;
    }

    public Snake getNext()
    {
        return next;
    }

    public void removeTail()
    {
        Destroy(this.gameObject);
    }
}
