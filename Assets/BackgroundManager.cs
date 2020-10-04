using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected float parallaxMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x * parallaxMultiplier, player.transform.position.y * parallaxMultiplier, 10);
    }
}
