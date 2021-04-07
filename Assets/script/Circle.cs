using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public bool BorderScreen { 
        get => _borderScreen;
        set { _borderScreen = value; }
    }
    bool _borderScreen;
    // Start is called before the first frame update
    void Start()
    {
        _borderScreen = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Se acabo la pantalla");
        _borderScreen = true;
    }
}
