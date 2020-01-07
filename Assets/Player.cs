using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SerVivo
{
    Status status;
    public Status GetStatus()
    {
        return status;
    }
    // Start is called before the first frame update
    void Start()
    {
        status = GetComponent<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
