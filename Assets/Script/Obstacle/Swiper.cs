using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    public float rotationSpeed = 50f; // 회전 속도
    public float floatSpeed = 1f; // 위아래 움직이는 속도
    public float floatHeight = 0.1f; // 위아래 움직이는 높이

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
}
