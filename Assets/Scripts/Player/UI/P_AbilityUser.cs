using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_AbilityUser : MonoBehaviour
{
    private Fireball Fireball;
    private PlasmaSpheres PlasmaSpheres;
    private Whirligig Whirligig;
    private RicochetStone RicochetStone;
    private LaserBeam LaserBeam;
    private UFORay UFORay;

    Player _player;
    AbilityUICooldowns _abilityUICooldowns;

    [Header("Shooting")]
    private bool _isSpawnedUFO = false;

    private float _lastFireball;
    private float _lastWhirligig;
    private float _lastRicochetStone;
    private float _lastLaserBeam;
    private float _lastUFORay;
    float _plasmaSpheresDurationCounter;
    float _plasmaSpheresCooldownCounter;

    [Header("Abilities")]
    public GameObject[] _abilityPrefabs;
    private List<string> activeAbilities;

    Dictionary<string, int> abilitiesDictionary = new Dictionary<string, int>();

    [Header("Container")]
    [SerializeField] public GameObject SpheresContainer;

    void Start()
    {
        Fireball = new Fireball();
        PlasmaSpheres = new PlasmaSpheres();
        Whirligig = new Whirligig();
        RicochetStone = new RicochetStone();
        LaserBeam = new LaserBeam();
        UFORay = new UFORay();

        _player = GameObject.Find("Player").GetComponent<Player>();
        _abilityUICooldowns = GameObject.Find("Cooldowns").GetComponent<AbilityUICooldowns>();
        string _innateAbilityCode = GameManager.Instance.InnateAbilityCode;
        activeAbilities = new List<string> { _innateAbilityCode };
    }

    void Update()
    {
    }
    private void ManageFireball()
    {
        if (IsAbilityActive("fireball"))
        {
            if (IsEnemyNearby(Fireball.Range))
            {
                if (Time.time > _lastFireball + Fireball.Cooldown)
                {
                    for (int i = 0; i < Fireball.ProjectileCount; i++)
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
    private void ManagePlasmaSpheres()
    {
        if (IsAbilityActive("plasma_spheres"))
        {
            if (_plasmaSpheresDurationCounter <= 0 && _plasmaSpheresCooldownCounter <= 0)
            {
                for (int i = 0; i < PlasmaSpheres.ProjectileCount; i++)
                {
                    //Debug.Log("Perevirka");
                    if (SpheresContainer.transform.childCount < PlasmaSpheres.ProjectileCount)
                    {
                        GameObject plasmaSphere = Instantiate(_abilityPrefabs[1], transform.parent.position, Quaternion.identity);
                        plasmaSphere.transform.SetParent(SpheresContainer.transform);
                        //Debug.Log("plasmaSphere ADDED");
                    }
                }
                SetUICooldown("plasma_spheres", PlasmaSpheres.Cooldown);

                _plasmaSpheresDurationCounter = PlasmaSpheres.Duration;
            }
            if (_plasmaSpheresDurationCounter > 0)
            {
                _plasmaSpheresDurationCounter -= Time.deltaTime;
                if (_plasmaSpheresDurationCounter <= 0)
                {
                    ClearChildren(SpheresContainer.transform);
                    _plasmaSpheresCooldownCounter = PlasmaSpheres.Cooldown;
                }
            }
        }
        if (_plasmaSpheresCooldownCounter > 0)
        {
            _plasmaSpheresCooldownCounter -= Time.deltaTime;
        }
    }
    public void ManagePlasmaSpheresActivator()
    {
        ManagePlasmaSpheres();
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
    private void ManageRicochetStone()
    {
        if(IsAbilityActive("ricochet_stone"))
        {
            if (IsEnemyNearby(RicochetStone.Range))
            {
                if (Time.time > _lastRicochetStone + RicochetStone.Cooldown)
                {
                    for (int i = 0; i < RicochetStone.ProjectileCount; i++)
                    {
                        GameObject ricochetStone = Instantiate(_abilityPrefabs[3], transform.parent.position, Quaternion.identity);
                        //Debug.Log("RICOCHETSTONE ADDED");
                    }
                    _lastRicochetStone = Time.time;

                    SetUICooldown("ricochet_stone", RicochetStone.Cooldown);
                }
            }
        }
    }
    public void ManageRicochetStoneActivator()
    {
        ManageRicochetStone();
    }
    private void ManageLaserBeam()
    {
        if(IsAbilityActive("laser_beam"))
        {
            if (Time.time > _lastLaserBeam + LaserBeam.Cooldown)
            {
                Vector3 spawnPosition = _player.transform.position;

                GameObject laserBeam = Instantiate(_abilityPrefabs[4], transform.parent.position, Quaternion.identity);
                _lastLaserBeam = Time.time;

                SetUICooldown("laser_beam", LaserBeam.Cooldown);
            }
        }
    }
    public void ManageLaserBeamActivator()
    {
        ManageLaserBeam();
    }

    private void ManageUFORay()
    {
        if (IsAbilityActive("ufo_ray"))
        {
            if(!_isSpawnedUFO)
            {
                GameObject ufoSpawn = Instantiate(_abilityPrefabs[5], transform.parent.position, Quaternion.identity);
                _isSpawnedUFO = true;
            }

        }
    }
    public void ManageUFORayActivator()
    {
        ManageUFORay();
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
                return true;
            }
        }
        return false;
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
    public int GetActiveProjectileCount()
    {
        return SpheresContainer.transform.childCount;
    }
}
