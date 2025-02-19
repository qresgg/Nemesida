using UnityEngine;

public class abilitySixChain : MonoBehaviour
{
    private bool chainRemoved = false;

    void Update()
    {
        if (!chainRemoved && P_Stats.Instance.MaxAbilitiesCount > 5)
        {
            Destroy(this.gameObject);
            chainRemoved = true;
        }
    }
}
