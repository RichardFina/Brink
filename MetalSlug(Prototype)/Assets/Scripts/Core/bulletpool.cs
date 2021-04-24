using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletpool : MonoBehaviour
{
    public static bulletpool poolInstance;

    [SerializeField]
    private GameObject poolBullet;
    private bool notEnoughBullets = true;

    private List<GameObject> pellets;

    private void Awake()
    {
        poolInstance = this;
    }

    private void Start()
    {
        pellets = new List<GameObject>();
    }

    public GameObject GetBullet()
    {
        if(pellets.Count > 0)
        {
            for(int i = 0; i < pellets.Count; i++)
            {
                if (!pellets[i].activeInHierarchy)
                    return pellets[i];
            }
        }

        if(notEnoughBullets)
        {
            GameObject bul = Instantiate(poolBullet);
            bul.SetActive(false);
            pellets.Add(bul);
            return bul;
        }
        return null;
    }
}
