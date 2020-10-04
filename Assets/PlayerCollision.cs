using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{

    [SerializeField] private ParticleSystem explosionParticlePrefab;

    public delegate void OnDeathDelegate();
    public OnDeathDelegate onDeath;

    public delegate void OnFinishDelegate();
    public OnFinishDelegate onFinish;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (LayerMask.NameToLayer("Hazard") == collision.gameObject.layer) {
            onDeath();
            Instantiate(explosionParticlePrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onFinish();
    }
}
