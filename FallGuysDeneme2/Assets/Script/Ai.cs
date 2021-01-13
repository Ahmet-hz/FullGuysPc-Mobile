using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{

    Animator animator;
    NavMeshAgent meshAgent;
    public GameObject[] hedef;
    public Transform silindirParentTransform1, silindirParentTransform2, silindirParentTransform3;
    public Transform RakipPlayer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        meshAgent.SetDestination(hedef[0].transform.position);

       

    }


    

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.tag == "HareketliDuvar")
        {
            transform.position = new Vector3(-2f, 0.018f, Random.Range(-1f, 1f));
        }
        if (collision.gameObject.tag == "DonenCubuk")
        {
            transform.position = new Vector3(-2f, 0.018f, Random.Range(-1f, 1f));
        }

        if (collision.gameObject.tag == "duvar5")
        {
            animator.SetBool("Bekle", true);
        }

        if (collision.gameObject.tag == "Duvar1")
        {
            
            transform.parent = silindirParentTransform1;
            
        }
        if (collision.gameObject.tag == "Duvar2")
        {
            
            transform.parent = silindirParentTransform2;
            
        }
        if (collision.gameObject.tag == "Duvar3")
        {
         
            transform.parent = silindirParentTransform3;
            
        }

        if (collision.gameObject.tag == "Duvar4")
        {
            
           
          transform.parent = RakipPlayer;
        }
    }
}
