using UnityEngine;

public class OrbitalSpheresActivation : MonoBehaviour
{
    private Player _player;
    private OrbitalSpheres OrbitalSpheres;
    private float currentAngle = 0.0f;

    private void Start()
    {
        OrbitalSpheres = new OrbitalSpheres();
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    private void Update()
    {
        MoveInOrbits();
    }

    public void MoveInOrbits()
    {
        if (_player != null)
        {
            int activeProjectileCount = _player.GetActiveProjectileCount();
            if (activeProjectileCount > 0)
            {
                float angleStep = 360f / activeProjectileCount;
                currentAngle += Time.deltaTime * OrbitalSpheres.ProjectileSpeed * 15;

                for (int i = 0; i < activeProjectileCount; i++)
                {
                    Transform projectile = _player.SpheresContainer.transform.GetChild(i);
                    float angle = currentAngle + i * angleStep;
                    float x = _player.transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * OrbitalSpheres.Radius;
                    float y = _player.transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * OrbitalSpheres.Radius;
                    Vector3 newPosition = new Vector3(x, y, _player.transform.position.z);
                    projectile.position = newPosition;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(OrbitalSpheres.DamageCount);
        }
    }
}
