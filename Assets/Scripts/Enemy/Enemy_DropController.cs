using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DropController : MonoBehaviour
{
    [SerializeField] private GameObject missionKeyObject;

    public void GiveKey(GameObject newkey) => missionKeyObject = newkey;

    public void DropItems()
    {
        if (missionKeyObject != null)
            CreateItem(missionKeyObject);
    }

    private void CreateItem(GameObject go)
    {
        GameObject newItem = Instantiate(go, transform.position + Vector3.up, Quaternion.identity);
    }
}
