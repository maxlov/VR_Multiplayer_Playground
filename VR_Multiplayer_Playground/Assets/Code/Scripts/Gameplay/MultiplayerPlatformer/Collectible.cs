using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private FloatVariable collectibleValue;
    [SerializeField] private FloatVariable score;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            score.value += collectibleValue.value;
            gameObject.SetActive(false);
        }    
    }
}
