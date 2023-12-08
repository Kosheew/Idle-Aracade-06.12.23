using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController: MonoBehaviour
{
    [SerializeField] GameObject[] prefab;
    private int indexD = 0;
    private bool isDeactivated = false;
    private float delayTimer = 6;

    void Update()
    {
        if (isDeactivated)
        {
            delayTimer -= Time.deltaTime;

            if (delayTimer <= 0f)
            {
                ActivateNextPrefab();    
                delayTimer = 6f;
                isDeactivated = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Weapon")
        {
            DeactivatePrefab();
        }
    }

    private void DeactivatePrefab()
    {
        if (indexD < prefab.Length)
        {
            prefab[indexD].SetActive(false);
            indexD++;

            if (indexD > 4)
            {
                gameObject.GetComponent<Collider>().enabled = false;
                isDeactivated = true;
            }

        }

    }

    private void ActivateNextPrefab()
    {
        if (indexD >= 4)
        {
            indexD = 0;
            foreach (GameObject el in prefab)
            {
                el.SetActive(true);
            }
            gameObject.GetComponent<Collider>().enabled = true;
        }
    }
}
