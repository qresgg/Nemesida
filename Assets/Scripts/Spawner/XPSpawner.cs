using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    [SerializeField] GameObject _xpPrefab;
    public void CollectDataAndSpawn(Vector3 position)
    {
        Instantiate(_xpPrefab, position, Quaternion.identity);
    }
}
