﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject snakePrefab;
    public Snake head;
    public Snake tail;
    public int NESW;  //directions
    public Vector2 nextPos;

    // Use this for initialization
    void Start () {
        InvokeRepeating("TimerInvoke", 0, .5f);  //(WhatWe Invoke, StartTime, TimeRate)
	}
	
	// Update is called once per frame
	void Update () {
        changeDirections();
	}

    void TimerInvoke()
    {
        Movement();
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
}
