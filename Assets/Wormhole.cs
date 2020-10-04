using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [SerializeField]
    protected bool isActive;

    public bool IsActive
    {
        get 
        {
            return isActive;
        }
        private set
        {
            isActive = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Disable()
    {
        this.isActive = false;
        this.gameObject.SetActive(false);
    }

    internal void Activate(Vector2 position)
    {
        this.transform.position = position;
        this.isActive = true;
        this.gameObject.SetActive(true);

    }
}
