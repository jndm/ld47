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

    private void OnCollisionEnter2D(Collision2D collision) {
        if (LayerMask.NameToLayer("Hazard") == collision.gameObject.layer) {
            onDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onFinish();
    }
}
