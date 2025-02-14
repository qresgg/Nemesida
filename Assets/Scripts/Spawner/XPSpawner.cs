using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSpawner : MonoBehaviour
{
    [SerializeField] GameObject _xpPrefab;
    [SerializeField] GameObject xp_container;
    public void CollectDataAndSpawn(Vector3 position)
    {
        GameObject XP_Cont = Instantiate(_xpPrefab, position, Quaternion.identity);
        XP_Cont.transform.SetParent(xp_container.transform);
    }
}
