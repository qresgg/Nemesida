using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AbilityUser : MonoBehaviour
{
    private Fireball Fireball;
    private OrbitalSpheres OrbitalSpheres;
    private Whirligig Whirligig;

    AbilityUICooldowns _abilityUICooldowns;
    GameManager _gameManager;

    [Header("Shooting")]
    [SerializeField] private int _projectileCount;
    private float _lastFireball;
    private float _lastWhirligig;
    float _orbitalSpheresDurationCounter;
    float _orbitalSpheresCooldownCounter;

    public bool _isEnemyNearby = false;

    [Header("Abilities")]
    public GameObject[] _abilityPrefabs;
    private List<string> activeAbilities;

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    [Header("Container")]
    [SerializeField] public GameObject SpheresContainer;

    void Start()
    {
        Fireball = new Fireball();
        OrbitalSpheres = new OrbitalSpheres();
        Whirligig = new Whirligig();


        _abilityUICooldowns = GameObject.Find("Cooldowns").GetComponent<AbilityUICooldowns>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        string _innateAbilityCode = _gameManager.GetInnateAbilityCode();
        activeAbilities = new List<string> { _innateAbilityCode };
    }

    void Update()
    {
    }
    private void ManageFireball()
    {
        if (IsAbilityActive("fireball"))
        {
            IsEnemyNearby(Fireball.Range);
            if (_isEnemyNearby)
            {
                if (Time.time > _lastFireball + Fireball.Cooldown)
                {
                    for (int i = 0; i < _projectileCount; i++)
                    {
                        GameObject fireball = Instantiate(_abilityPrefabs[0], transform.parent.position, Quaternion.identity);
                        //Debug.Log("FIREBALL ADDED");
                    }
                    _lastFireball = Time.time;

                    SetUICooldown("fireball", Fireball.Cooldown);
                }
            }
        }
    }
    public void ManageFireballActivator()
    {
        ManageFireball();
    }
    private void ManageOrbitalSphere()
    {
        if (IsAbilityActive("orbital_spheres"))
        {
            if (_orbitalSpheresDurationCounter <= 0 && _orbitalSpheresCooldownCounter <= 0)
            {
                for (int i = 0; i < _projectileCount; i++)
                {
                    Debug.Log("Perevirka");
                    if (SpheresContainer.transform.childCount < _projectileCount)
                    {
                        GameObject orbitalSphere = Instantiate(_abilityPrefabs[1], transform.parent.position, Quaternion.identity);
                        orbitalSphere.transform.SetParent(SpheresContainer.transform);
                        Debug.Log("ORBITAL SPHERES ADDED");
                    }
                }
                abilitiesDictionary.TryGetValue("orbital_spheres", out int orbitalSpheresID);
                _abilityUICooldowns.SetCooldown(orbitalSpheresID, OrbitalSpheres.Cooldown, "orbital_spheres", OrbitalSpheres.Duration);

                _orbitalSpheresDurationCounter = OrbitalSpheres.Duration;
            }
            if (_orbitalSpheresDurationCounter > 0)
            {
                _orbitalSpheresDurationCounter -= Time.deltaTime;
                if (_orbitalSpheresDurationCounter <= 0)
                {
                    ClearChildren(SpheresContainer.transform);
                    _orbitalSpheresCooldownCounter = OrbitalSpheres.Cooldown;
                }
            }
        }
        if (_orbitalSpheresCooldownCounter > 0)
        {
            _orbitalSpheresCooldownCounter -= Time.deltaTime;
        }
    }
    public void ManageOrbitalSphereActivator()
    {
        ManageOrbitalSphere();
    }

    private void ManageWhirligig()
    {
        if (IsAbilityActive("whirligig"))
        {
            if (Time.time > _lastWhirligig + Whirligig.Cooldown)
            {
                GameObject whirligig = Instantiate(_abilityPrefabs[2], transform.parent.position, _abilityPrefabs[2].transform.rotation);
                //Debug.Log("WHIRLIGIG ADDED");
                whirligig.transform.SetParent(this.transform);
                _lastWhirligig = Time.time;

                SetUICooldown("whirligig", Whirligig.Cooldown);
            }
        }
    }

    public void ManageWhirligigActivator()
    {
        ManageWhirligig();
    }

    private bool IsAbilityActive(string abilityName)
    {
        return activeAbilities.Contains(abilityName);
    }

    public void SetAbilityDictionary(Dictionary<string, int> abilityDict)
    {
        abilitiesDictionary = abilityDict;
    }
    void SetUICooldown(string abilityName, float cooldown)
    {
        abilitiesDictionary.TryGetValue(abilityName, out int abilityID);
        _abilityUICooldowns.SetCooldown(abilityID, cooldown, abilityName);
    }
    private bool IsEnemyNearby(float range)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider hitcollider in hitColliders)
        {
            if (hitcollider.CompareTag("Enemy"))
            {
                return _isEnemyNearby = true;
            }
        }
        return _isEnemyNearby = false;
    }

    public void AddAbilityToActiveList(string abilityCode)
    {
        activeAbilities.Add(abilityCode);
    }
    private void ClearChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
