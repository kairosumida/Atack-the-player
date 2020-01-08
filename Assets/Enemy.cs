using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : SerVivo
{
    [SerializeField] private Vector3 posInicial; // usada para retornar o enemy evitando q o jogoador leve ele para muito longe
    [SerializeField] private float distanciaMax;
    [SerializeField] private GrupoInimigo grupoInimigo;//Grupo que comanda esse enemy
    [SerializeField] private Enemy Chefe;//Enemy segue um lider
    
    private bool estaRecuando;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IndoAteOrigem());
    }

    // Update is called once per frame
    void Update()
    {
        RetornarAOrigem();
        
    }
    public void Recuar()
    {
        estaRecuando = true;
    }
    private void RetornarAOrigem()
    {
        if (grupoInimigo == null)//Caso n tenha grupo, o proprio ser controla a retirada
        {
            if (Vector3.Distance(gameObject.transform.position, posInicial) > distanciaMax)
            {
                Recuar();
            }
        }
        else if(grupoInimigo!=null && !grupoInimigo.GetEhComandante())//Verifica se o grupo foge com alguma regra do grupo, ou se eh os proprios membros que decidem quando recuar
        {
            grupoInimigo.Recuar();
        }
    }
    IEnumerator IndoAteOrigem()
    {
        yield return new WaitWhile(()=>!ChegouAOrigem());//Enquanto for verdadeiro espera
    }
    private bool ChegouAOrigem()
    {
        if (Vector3.Distance(gameObject.transform.position, posInicial) > 0.1f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
