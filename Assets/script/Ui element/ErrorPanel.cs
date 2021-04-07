using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPanel : MonoBehaviour
{
    // Start is called before the first frame update
    public void close ()
    {
        gameObject.SetActive(false);
    }
}
