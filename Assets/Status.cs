using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    [SerializeField] private float rangeAtaq;
    private float rangeDeVisao;
    [SerializeField] private float velocidadeDeMovimento;
    private float velocidadeAtaq;
    private float danoPadrao;
    public float GetDanoPadrao()
    {
        return danoPadrao;
    }
    public float GetRangeAtaq()
    {
        return rangeAtaq;
    }
    public float GetRangeDeVisao()
    {
        return rangeDeVisao;
    }
    public float GetVelocidadeDeMovimento()
    {
        return velocidadeDeMovimento;
    }
    public float GetVelocidadeAtaq()
    {
        return velocidadeAtaq;
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
