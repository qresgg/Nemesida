using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider _slider;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }

}
