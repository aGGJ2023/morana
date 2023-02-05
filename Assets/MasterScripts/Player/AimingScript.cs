using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Transform center;

    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Transform spawnPosition;

    // Update is called once per frame
    void Update()
    {

        transform.RotateAround(center.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position);

       
        // aimingDot.Rotate((Input.GetAxis("Mouse X") * RotationSpeed * Time.deltaTime), (Input.GetAxis("Mouse Y") * RotationSpeed * Time.deltaTime), 0, Space.World);
        // transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 90);
    }
    
}
