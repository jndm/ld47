using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    protected GameObject player;

    [SerializeField]
    protected float cameraHeight = 1;

    [SerializeField]
    protected float cameraTransitionSpeed = 1;


    [SerializeField]
    protected float cameraTransitionMoveSpeed = 1;

    [SerializeField]
    protected WormholeManager wormholeManager;

    private Vector3 oldWormholePosition;
    private float transationState = 0;

    // Start is called before the first frame update
    void Start()
    {     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        var wormhole = wormholeManager.ActiveWormhole;
        if (wormhole != null)
        {
            oldWormholePosition = new Vector3(wormhole.transform.position.x, wormhole.transform.position.y, -cameraHeight);
            transationState += Time.deltaTime * cameraTransitionSpeed;
        } 
        else
        {
            transationState -= Time.deltaTime * cameraTransitionSpeed;
        }

        transationState = Mathf.Clamp(transationState, 0, 0.5f);

        var playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -cameraHeight);

        //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -cameraHeight);
        var targetPosition = Vector3.Lerp(playerPosition, oldWormholePosition, transationState);
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, cameraTransitionMoveSpeed);
    }
}
