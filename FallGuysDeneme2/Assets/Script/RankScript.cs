using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankScript : MonoBehaviour
{
    public GameObject score;
    int Ranked = 0;
    public Text Ranks ;
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Girls")
        {
            Ranked++;
            print(Ranked);
        }
        else if (other.gameObject.tag == "Boy1")
        {
            Ranked++;
            score.SetActive(true);
            Ranks.text = Ranked.ToString();
        }
    }
}
