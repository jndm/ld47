using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private Vector3 direction;
    [SerializeField]
    private float speed;
    [SerializeField]
    private int maxMovementLength;

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
        this.transform.Rotate(new Vector3(rotationX, rotationY, rotationZ), this.rotationSpeed * Time.deltaTime);

        var distanceMoved = Vector2.Distance(this.transform.position, distanceMeasurePosition);

        var normalizedDistance = direction.normalized;
        if (distanceMoved >= maxMovementLength )
        {
            distanceMeasurePosition = this.transform.position;
            swapDirection = !swapDirection;
        }


        if (swapDirection)
        {
            normalizedDistance = -normalizedDistance;
        }

        this.transform.position = this.transform.position + normalizedDistance * speed * Time.deltaTime;
    }
}
