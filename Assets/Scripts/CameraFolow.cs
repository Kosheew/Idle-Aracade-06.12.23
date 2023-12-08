using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraFolow : MonoBehaviour
{
    public float speed = 100f;
    private Transform _target;

    void Start()
    {
        _target = FindObjectOfType<Movement>().GetComponent<Transform>();
    }

    void Update()
    {
        float distance = Vector3.Distance(_target.position, transform.position);

        if (distance > 0.3)
        {
            Vector3 newPos = new Vector3(_target.position.x, transform.position.y, _target.position.z - 7);
            transform.position = Vector3.Lerp(transform.position, newPos, speed);
        }
    }
}
