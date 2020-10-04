using UnityEngine;

public class Asteroid : MonoBehaviour
{
    enum MoveMode {
        PingPong,
        Circle
    };

    [SerializeField]
    private MoveMode moveMode = MoveMode.PingPong;

    [SerializeField]
    private Vector3 direction;
    [SerializeField,Tooltip("PingPong: units/s\nCircle: degrees/s")]
    private float speed;
    [SerializeField]
    private int maxMovementLength;

    [Header("Circle Mode Options")]
    [SerializeField]
    private Vector3 tetherPoint;

    private bool swapDirection = false;

    private Vector2 distanceMeasurePosition;

    private int rotationX, rotationY, rotationZ;
    private int rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        this.distanceMeasurePosition = this.transform.position;

        this.rotationX = Random.Range(0, 1);
        this.rotationY = Random.Range(0, 1);
        this.rotationZ = Random.Range(0, 1);

        if (this.rotationX == 0 && this.rotationY == 0 && this.rotationZ == 0)
        {
            this.rotationX = 1;
            this.rotationZ = 1;
        }

        this.rotationSpeed = Random.Range(20, 100);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotation animation
        this.transform.Rotate(new Vector3(rotationX, rotationY, rotationZ), this.rotationSpeed * Time.deltaTime);

        switch(moveMode) {
            case MoveMode.Circle:
                CircleMove();
                break;
            case MoveMode.PingPong:
            default:
                PingPongMove();
                break;
        }
    }

    private void CircleMove() {
        this.transform.RotateAround(
            tetherPoint,
            Vector3.forward,
            speed * Time.deltaTime
        );
    }

    private void PingPongMove() {
        var distanceMoved = Vector2.Distance(this.transform.position, distanceMeasurePosition);

        var normalizedDistance = direction.normalized;
        if (distanceMoved >= maxMovementLength) {
            distanceMeasurePosition = this.transform.position;
            swapDirection = !swapDirection;
        }


        if (swapDirection) {
            normalizedDistance = -normalizedDistance;
        }

        this.transform.position = this.transform.position + normalizedDistance * speed * Time.deltaTime;
    }

    private void OnDrawGizmosSelected() {
        if (moveMode == MoveMode.Circle) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(tetherPoint, .5f);
            Gizmos.DrawWireSphere(tetherPoint, (transform.position - tetherPoint).magnitude);
        }
        if (moveMode == MoveMode.PingPong) {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + direction * maxMovementLength);
        }
    }
}
