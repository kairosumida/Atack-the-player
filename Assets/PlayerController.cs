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
    [SerializeField] Camera cam;
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
            gameObject.transform.LookAt(alvo.transform.position);
            alvo.RecebeDano(player.GetStatus().GetDanoPadrao());//Dano do Jogador
            Debug.Log("Aq");
            if (!EstaAlvoNoRange())
            {
                Debug.Log("Alvo fora do range");
                acaoPlayer = Andando();
                StartCoroutine(Andando());
                yield break;
            }
            if (alvo.GetEstaMorto() || alvo == null)
            {
                Debug.Log(alvo.GetEstaMorto());
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
        Debug.Log("Verif");
        if (Vector3.Distance(alvo.transform.position, transform.position) <= player.GetStatus().GetRangeAtaq())
        {
            Debug.Log("No alvo");
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
            while (!VerificarSeEstaNoDestino() && !EstaAlvoNoRange())
            {
                
                nav.speed = player.GetStatus().GetVelocidadeDeMovimento();
                //GetComponent<Animator>().speed = statusPersonagem.GetVelocidadeMovimentoPadrao() / 3.5f;
                yield return new WaitForEndOfFrame();
            }
            nav.stoppingDistance = player.GetStatus().GetRangeAtaq();
            
            if (EstaAlvoNoRange())
            {
                
                acaoPlayer = Atacando();
                StartCoroutine(acaoPlayer);
                yield break;
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
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponent<SerVivo>() != null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Clicou em alvo");
                    MoverPara(hitInfo.collider.gameObject.GetComponent<SerVivo>().transform.position, hitInfo.collider.gameObject.GetComponent<SerVivo>());
                    return;
                }

            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Debug.Log("Sem alvo");
                    MoverPara(hitInfo.point, null);
                }
            }

        }
    }
}
