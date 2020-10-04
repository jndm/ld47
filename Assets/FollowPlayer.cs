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
    protected float cameraMoveSpeed = 5.2f;

    [SerializeField]
    protected WormholeManager wormholeManager;

    private Vector3 wormholePosition;
    private float transitionState = 0;

    private void Start() {
        this.transform.position = new Vector3(transform.position.x, transform.position.y, -cameraHeight);
    }

    void LateUpdate()
    {
        var wormhole = wormholeManager.ActiveWormhole;
        if (wormhole != null) {
            wormholePosition = new Vector3(wormhole.transform.position.x, wormhole.transform.position.y, -cameraHeight);
            transitionState += Time.deltaTime * cameraTransitionSpeed;
        } 
        else
        {
            transitionState -= Time.deltaTime * cameraTransitionSpeed;
        }
        transitionState = Mathf.Clamp(transitionState, 0, 0.5f);

        var playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, -cameraHeight);

        var targetPosition = Vector3.Lerp(playerPosition, wormholePosition, transitionState);
        if ((targetPosition - transform.position).sqrMagnitude > 5f * 5f) {
            this.transform.position = targetPosition;
        } else {
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, cameraMoveSpeed * Time.deltaTime);
        }
    }
}
