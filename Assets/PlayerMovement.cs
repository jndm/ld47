using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    protected Vector2 tetherPoint;

    [SerializeField]
    protected float speed = 1;

    [SerializeField]
    protected WormholeManager wormholeManager;

    private float currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        tetherPoint = new Vector2(this.transform.position.x, this.transform.position.y + 1);
        currentRotation = Vector2.SignedAngle(Vector2.right, new Vector2(this.transform.position.x, this.transform.position.y) - tetherPoint);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var activeWormhole = wormholeManager.ActiveWormhole;
        if (activeWormhole != null)
        {
            currentRotation = Vector2.SignedAngle(Vector3.right, new Vector3(this.transform.position.x, this.transform.position.y) - activeWormhole.transform.position);
            var rotationVector = Vector2.Perpendicular(this.transform.position - activeWormhole.transform.position);

            var sign = -Mathf.Sign(Vector2.Dot(activeWormhole.transform.position - this.transform.position, transform.right));

            this.transform.rotation = Quaternion.LookRotation(Vector3.forward, sign * rotationVector);

            this.transform.RotateAround(
                activeWormhole.transform.position, 
                Vector3.forward,
                Mathf.Rad2Deg * sign * speed * Time.deltaTime / Vector2.Distance(this.transform.position, activeWormhole.transform.position)
            );
        } 
        else
        {
            this.transform.position += this.transform.up * speed * Time.deltaTime;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(tetherPoint, 0.3f);
        Gizmos.DrawLine(tetherPoint, this.transform.position);
    }
}
