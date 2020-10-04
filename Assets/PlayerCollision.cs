using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public delegate void OnDeathDelegate();
    public OnDeathDelegate onDeath;

    public delegate void OnFinishDelegate();
    public OnFinishDelegate onFinish;

    // Start is called before the first frame update
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(LayerMask.NameToLayer("Hazard") == collision.gameObject.layer)
        {
            onDeath();
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        onFinish();
    }
}
