using System.Collections;
using UnityEngine;

public class LaserBeamActivation : MonoBehaviour
{
    LaserBeam LaserBeam;
    Player _player;
    private Vector3 _initialPlayerPosition;
    private bool _isActivated = false;
    private bool _rightDirection;

    private float _laserScaleSpeed = 0.2f;
    private float _laserSpeedMove = 1f;
    private float _maxLaserScale = 16.0f;


    void Start()
    {
        LaserBeam = new LaserBeam();

        _player = GameObject.Find("Player").GetComponent<Player>();
        this.transform.rotation = Quaternion.Euler(0, 0, 90);

        (bool left, bool right) = _player.GetMoveDirection();
        _rightDirection = right;

        _initialPlayerPosition = _player.transform.position;
        _isActivated = true;

        StartCoroutine(LaserTransformation());
        Invoke("RemoveLaser", 1f);
    }

    void Update()
    {
        if (_isActivated)
        {
            UpdateLaserPosition();
        }
    }

    IEnumerator LaserTransformation()
    {
        this.transform.localScale = new Vector3(0.4f, 0, transform.localScale.z);

        while (this.transform.localScale.y < _maxLaserScale)
        {
            this.transform.localScale += new Vector3(0, _laserScaleSpeed, 0);

            if (_rightDirection)
            {
                this.transform.position += new Vector3(this.transform.localScale.y, 0, 0);
            }
            else
            {
                this.transform.position -= new Vector3(this.transform.localScale.y, 0, 0);
            }
            yield return null;
        }
        yield return StartCoroutine(RemoveLaser());
    }

    void UpdateLaserPosition()
    {
        Vector3 spawnPosition = _player.transform.position;

        transform.position = spawnPosition;
    }

    IEnumerator RemoveLaser()
    {
        Destroy(this.gameObject);
        yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(LaserBeam.DamageCount);
        }
    }
}
