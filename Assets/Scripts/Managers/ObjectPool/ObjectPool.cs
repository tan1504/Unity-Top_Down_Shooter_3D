using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

	[SerializeField] private int poolSize = 10;

	private Dictionary<GameObject, Queue<GameObject>> poolDictionary = 
			new Dictionary<GameObject, Queue<GameObject>>();

	[Header("To Initialize")]
	[SerializeField] private GameObject weaponPickup;
	[SerializeField] private GameObject ammoPickup;

	private void Awake()
	{
		if (instance == null) 
			instance = this;
		else
			Destroy(gameObject);
	}

	private void Start()
	{
		InitializeNewPool(weaponPickup);
		InitializeNewPool(ammoPickup);
	}

	public GameObject GetObject(GameObject prefab, Transform target)
	{
		if (poolDictionary.ContainsKey(prefab) == false)
		{
			InitializeNewPool(prefab);
		}

        if (poolDictionary[prefab].Count == 0)
			CreateNewObject(prefab);	// if all objects of this are in use, create a new one

        GameObject objectToGet = poolDictionary[prefab].Dequeue();

		objectToGet.transform.position = target.position;
		objectToGet.transform.parent = null;	
		objectToGet.SetActive(true);

		return objectToGet;
	}

	public void ReturnObject(GameObject objectToGet, float delay = 0.001f)
	{
		StartCoroutine(DelayReturn(delay, objectToGet));
	}

	private IEnumerator DelayReturn(float delay, GameObject objectToReturn)
	{
		yield return new WaitForSeconds(delay);

		ReturnToPool(objectToReturn);
	}

	private void ReturnToPool(GameObject objectToReturn)
	{
		GameObject originalPrefab = objectToReturn.GetComponent<PooledObject>().originalPrefab;

		objectToReturn.SetActive(false);
		objectToReturn.transform.parent = transform;

		poolDictionary[originalPrefab].Enqueue(objectToReturn);
	}

	private void InitializeNewPool(GameObject prefab)
	{
		poolDictionary[prefab] = new Queue<GameObject>();

        for (int i = 0; i < poolSize; i++)
		{
			CreateNewObject(prefab);
		}
	}

	private void CreateNewObject(GameObject prefab)
	{
		GameObject newObject = Instantiate(prefab, transform);
		newObject.AddComponent<PooledObject>().originalPrefab = prefab;
		newObject.gameObject.SetActive(false);
		poolDictionary[prefab].Enqueue(newObject);
	}
}
