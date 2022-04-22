using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBehaviour : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public bool goLeft=true;
    void Update()
    {
        if (goLeft)
            transform.Translate(Vector3.right * 0.02f);
        else if (!goLeft)
            transform.Translate(Vector3.left * 0.02f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Start"))
        {
            goLeft = false;
        }
        if (other.gameObject.CompareTag("End"))
        {
            goLeft = true;
        }
    }
}
