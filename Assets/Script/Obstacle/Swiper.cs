using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour
{
    public float rotationSpeed = 50f; // ȸ�� �ӵ�
    public float floatSpeed = 1f; // ���Ʒ� �����̴� �ӵ�
    public float floatHeight = 0.1f; // ���Ʒ� �����̴� ����

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
