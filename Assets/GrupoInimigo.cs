using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrupoInimigo : MonoBehaviour
{
    [SerializeField] private bool ehComandante;
    [SerializeField] private List<Enemy> listaMembros;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public bool GetEhComandante()
    {
        return ehComandante;
    }
    public void Recuar()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
