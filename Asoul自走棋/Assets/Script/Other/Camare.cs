using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camare : MonoBehaviour
{
    public Transform command;
    private void Update()
    {
        transform.position = new Vector3(command.position.x, command.position.y, transform.position.z);
    }
}
