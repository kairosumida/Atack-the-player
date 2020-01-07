using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerVivo : MonoBehaviour
{
    private bool estaMorto = false;
    [SerializeField] private float Vida;

    public void RecebeDano(float dano)
    {
        Vida -= dano;
        if (Vida <= 0)
        {
            estaMorto = true;
        }
    }
    public bool GetEstaMorto()
    {
        return estaMorto;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
