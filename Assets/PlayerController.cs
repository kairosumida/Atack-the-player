using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent nav;
    private Player player;
    private IEnumerator acaoPlayer;
    private SerVivo alvo;

    public void PararAcoes()
    {
        StopCoroutine(acaoPlayer);
    }

    public void MoverPara(Vector3 posicao, SerVivo al)
    {
        alvo = al;
        if (acaoPlayer != null)
        {
            StopCoroutine(acaoPlayer);
        }
        nav.SetDestination(posicao);
       
        acaoPlayer = Andando();
        StartCoroutine(acaoPlayer);

    }

    public NavMeshAgent GetNav()
    {
        return nav;
    }
    IEnumerator Atacando()
    {
        nav.velocity = Vector3.zero;
        while (true)
        {

            yield return new WaitForSeconds(1f / player.GetStatus().GetVelocidadeAtaq());//aspd
            alvo.RecebeDano(player.GetStatus().GetDanoPadrao());//Dano do Jogador
            if (!EstaAlvoNoRange())
            {
                StopCoroutine(acaoPlayer);
                acaoPlayer = Andando();
                StartCoroutine(Andando());
            }
            if (alvo.GetEstaMorto() || alvo == null)
            {
                PararAcoes();
                alvo = null;

                //GetComponent<Animator>().Play("idle1");
                //GetComponent<Animator>().speed = 1f;
                break;
            }
        }
    }
    private bool EstaAlvoNoRange()
    {
        if (alvo == null)
            return false;

        if (Vector3.Distance(alvo.transform.position, transform.position) <= player.GetStatus().GetRangeAtaq())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    IEnumerator Andando()
    {
        if (player.GetEstaMorto())
        {
            StopAllCoroutines();
            yield break;
        }
        else
        {
            nav.stoppingDistance = 0.5f;
            //GetComponent<Animator>().Play("Walk");
            nav.speed = player.GetStatus().GetVelocidadeDeMovimento();
            //GetComponent<Animator>().speed = statusPersonagem.GetVelocidadeMovimentoPadrao() / 3.5f;
            yield return new WaitWhile(() => !VerificarSeEstaNoDestino() || !EstaAlvoNoRange());
            if (EstaAlvoNoRange())
            {
                StopCoroutine(acaoPlayer);
                acaoPlayer = Atacando();
                StartCoroutine(acaoPlayer);
            }
        }
    }
    private void LateUpdate()
    {
        if (nav.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            Vector3 normalizado = new Vector3(nav.velocity.normalized.x, 0, nav.velocity.normalized.z);

            transform.rotation = Quaternion.LookRotation(normalizado);
        }

    }
 
    private bool VerificarSeEstaNoDestino()
    {
        if (!nav.pathPending)
        {
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {

                    return true;
                }
            }
        }
        return false;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        OperadorJogador();
    }

    public void OperadorJogador()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponent<SerVivo>() != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    MoverPara(hitInfo.collider.gameObject.GetComponent<SerVivo>().transform.position, hitInfo.collider.gameObject.GetComponent<SerVivo>());
                    return;
                }

            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    MoverPara(hitInfo.point, null);
                }
            }

        }
    }
}
