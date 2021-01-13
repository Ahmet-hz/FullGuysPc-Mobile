using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerObj;
    public GameObject hedefObje;
    Vector3 YeniKonum;
    bool durum = true;

    private void Start()
    {
        YeniKonum= new Vector3(-33.65f, 1.07f, 0);
    }


    private void Update()
    {
        GameObject tofollow = playerObj;

          
        float playerX= tofollow.transform.position.x;
        float cameraX = transform.position.x;

        // pk = playerX - cameraX;

        //karakterle arasındaki mesafeyi alıp 
        //sabit tutuyorum her defasındaki arasındaki mesafeyi aynı kalması için update de 

        if (durum)
        {
            transform.position = new Vector3(playerX + 3, transform.position.y, transform.position.z);
        }



        if (GameObject.Find("Boy").GetComponent<PlayerController>().DuvarBoyamaDurum)
        {
            durum = false;
            transform.position = Vector3.MoveTowards(transform.position, YeniKonum,5f*Time.deltaTime);
            transform.LookAt(hedefObje.transform);
        }
        //playerX +1 seklinde yazimin yerine yani "1" yerine pk da yazabiliriz. 
        //unityden scene alanından ayarladigim gibi gozukur
    }

}
