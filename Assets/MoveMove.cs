using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMove : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector3(transform.position.x + Time.deltaTime, transform.position.y, 0);
    }
}
