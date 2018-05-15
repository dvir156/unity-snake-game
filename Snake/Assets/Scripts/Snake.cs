using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Snake : MonoBehaviour {

    public Snake next;//Work like linklist
    public static Action<String> hit;
    public void setNext(Snake IN)
    {
        next = IN;  
    }
    public Snake getNext()
    {
        return next;
    }
    public void RemoveTail()
    {
        Destroy(this.gameObject);
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(hit != null)
        {
            hit(other.tag);
        }
        if(other.tag == "Food")
        {
            Destroy(other.gameObject);
        }
    }
}
