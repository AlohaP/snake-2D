using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public int xBound;   //We will use it for position range for our food to spawn
    public int yBound;
    public GameObject foodPrefab;
    public GameObject currentFood;

    public int maxSize;
    public int currentSize;  //We will check if they are equal and if they will thats how big we want our snake

    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;
    public int NESW;  //directions
    public Vector2 nextPos;

    // Use this for initialization
    void Start () {
        InvokeRepeating("TimerInvoke", 0, .5f);  //(WhatWe Invoke, StartTime, TimeRate)
        foodFunction();
    }
	
	// Update is called once per frame
	void Update () {
        changeDirections();
	}

    void TimerInvoke()
    {
        Movement();
        if(currentSize >= maxSize)
        {
            tailFunction();  //We keep snake for getting too large
        }else
        {
            currentSize++;
        }
    }

    void Movement()
    {
        GameObject temp;
        nextPos = head.transform.position;

        switch (NESW)
        {
            case 0: //Up
                nextPos = new Vector2(nextPos.x, nextPos.y + 1);
                break;
            case 1: //Rigth
                nextPos = new Vector2(nextPos.x + 1, nextPos.y);
                break;
            case 2: //Down
                nextPos = new Vector2(nextPos.x, nextPos.y - 1);
                break;
            case 3: //Left
                nextPos = new Vector2(nextPos.x - 1, nextPos.y);
                break;
        }
        temp = (GameObject)Instantiate(snakePrefab, nextPos, transform.rotation);
        head.setNext(temp.GetComponent<Snake>());  //We take newly instantiated Snake obj and save to next varaiable of current head
        head = temp.GetComponent<Snake>();

        return;
    }

    void changeDirections()   //Change direction and be sure to not be going to clide inside
    {
        if (NESW != 2 && Input.GetKeyDown(KeyCode.W))
        {
            NESW = 0;
        }

        if (NESW != 3 && Input.GetKeyDown(KeyCode.D))
        {
            NESW = 1;
        }

        if (NESW != 0 && Input.GetKeyDown(KeyCode.S))
        {
            NESW = 2;
        }

        if (NESW != 1 && Input.GetKeyDown(KeyCode.A))
        {
            NESW = 3;
        }
    }

    void tailFunction()
    {
        Snake tempSnake = tail;
        tail = tail.getNext();  //We save current tail in next object so we can remove current object
        tempSnake.removeTail();
    }

    void foodFunction()  //ToDO food shouldnt spawn on snake
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = Random.Range(-yBound, yBound);

        //Instantiate new food from prefab
        currentFood = (GameObject)Instantiate(foodPrefab, new Vector2(xPos, yPos), transform.rotation); //Casting -> (Object we wanna spawn, new Vector, Rotation)
        StartCoroutine(checkRender(currentFood));
    }


    IEnumerator checkRender(GameObject IN)  //It secures us for situation when spawn  food outside of camera view
    {
        yield return new WaitForEndOfFrame();
        if(IN.GetComponent<Renderer>().isVisible == false)
        {
            if(IN.tag == "Food" || IN.tag == "Snake")
            {
                Destroy(IN);
                foodFunction();
            }
        }

    }
}
