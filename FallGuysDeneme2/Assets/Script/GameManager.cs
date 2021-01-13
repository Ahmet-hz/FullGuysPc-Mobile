using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject Rakip;
    int a = 0,b=0;
    float xEkseni=1.5f, zEkseni=-1f;
    Vector3 olustur;

   public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        //RakipOlustur();
        
    }

    void RakipOlustur()
    {
        for (a=0; a <= 2; a++)
        {
            xEkseni += 0.5f;
            for (b = 0; b <= 2; b++)
            {
                zEkseni += 1;
                olustur = new Vector3(xEkseni, 0.024f, zEkseni);
                Instantiate(Rakip, olustur, Quaternion.identity);
                print("malakakakak");
            }

        }
    }





}
