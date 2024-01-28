using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySpawn : MonoBehaviour
{
    public GameObject coin;
    public int coins = 0;
    // Start is called before the first frame update
    void Awake()
    {
        Cliente.ClientSatisfiedEvent += SpawnMoney;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnMoney(bool sat, string name)
    {
        int rand = Random.Range(2, 5);
        for(int i = 0; i < rand; i++)
        {      
            Instantiate(coin, new Vector3(transform.position.x, (float)(transform.position.y + 0.05*coins), transform.position.z), transform.rotation, transform);
            coins++;
        }
    }
}
