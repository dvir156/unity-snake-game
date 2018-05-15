using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamecontroller : MonoBehaviour
{
    public int maxSize;
    public int currentSize;
    public int xBound;
    public int yBound;
    public int score;
    public GameObject foodPrefab;
    public GameObject currentFood;
    public GameObject snakePrefabs;
    public Snake Head;
    public Snake Tail;
    public int NESW;
    public Vector2 nexPos;
    // Use this for initialization
    void OnEnable()
    {
        Snake.hit += hit;
    }
    void Start()
    {
        InvokeRepeating("TimerInvoke", 0, .5f);
        FoodFunc();
    }
    void OnDisable()
    {
        Snake.hit -= hit;
    }
    // Update is called once per frame
    void Update()
    {
        ComchangeDir();
    }

    void TimerInvoke()
    {
        Movement();
        if(currentSize >= maxSize)
        {
            TailFunc();
        }
        else
        {
            currentSize++;
        }
    }
    void Movement()
    {
        GameObject temp;
        nexPos = Head.transform.position;
        switch (NESW)
        {
            case 0:
                nexPos = new Vector2(nexPos.x, nexPos.y + 1);
                break;
            case 1:
                nexPos = new Vector2(nexPos.x+1, nexPos.y);
                break;
            case 2:
                nexPos = new Vector2(nexPos.x, nexPos.y-1);
                break;
            case 3:
                nexPos = new Vector2(nexPos.x-1, nexPos.y);
                break;
        }
        temp = (GameObject)Instantiate(snakePrefabs, nexPos, transform.rotation);
        Head.setNext(temp.GetComponent<Snake>());
        Head = temp.GetComponent<Snake>();

        return;
    }
    void ComchangeDir()//Move snake
    {
        if(NESW != 2 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            NESW = 0;

        }
        if (NESW != 3 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            NESW = 1;

        }
        if (NESW != 0 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            NESW = 2;

        }
        if (NESW != 1 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            NESW = 3;

        }
    }

    void TailFunc()//Delete the tail
    {
        Snake tempSanke = Tail;
        Tail = Tail.getNext();
        tempSanke.RemoveTail();
    }

    void FoodFunc()//Place the food
    {
        int xPos = Random.Range(-xBound, xBound);
        int yPos = Random.Range(-yBound, yBound);

        currentFood = (GameObject)Instantiate(foodPrefab,new Vector2(xPos,yPos),transform.rotation);

        StartCoroutine(CheckRender(currentFood));
    }

    IEnumerator CheckRender(GameObject IN)//Food inside the screen
    {
        yield return new WaitForEndOfFrame();
        if(IN.GetComponent<Renderer>().isVisible == false)
        {
            if(IN.tag == "food")
            {
                Destroy(IN);
                FoodFunc();
            }
        }
    }

    void hit(string WhatWasSent)
    {
        if(WhatWasSent == "Food")
        {
            FoodFunc();
            maxSize++;
            score++;
        }
    }
}