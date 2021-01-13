using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngellerSilindir : MonoBehaviour
{
    public GameObject[] engel;
    public GameObject[] donut;

    public float Speed;
    float timee = 0.4f;
    int a;


    private void FixedUpdate()
    {
        
           
        engel[0].transform.Rotate(new Vector3(0, 0, -0.3f));
        engel[1].transform.Rotate(new Vector3(0, 0, 0.3f));
        engel[2].transform.Rotate(new Vector3(0, 0, 0.3f));

        for (a = 1; a < 3; a++)
        {
            Speed = Random.Range(1, 3)*a*timee;

            donut[a-1].transform.Rotate(new Vector3(Speed,0f,0f));
        }
        if (timee < 1)
        {
            timee += Time.deltaTime;
        }
        else
        {
            timee=Random.Range(0,1);
            a = 1;
        }
    }

    







}
