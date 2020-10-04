using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    protected Vector3 rotationDirection;

    [SerializeField]
    protected float rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotationDirection, this.rotationSpeed * Time.deltaTime);
    }
}
