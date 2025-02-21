using UnityEngine;

public class AbilityChained : MonoBehaviour
{
    private bool _isChainRemoved = false;

    void Update()
    {
        if (!_isChainRemoved && P_Stats.Instance.MaxAbilitiesCount > 5)
        {
            Destroy(this.gameObject);
            _isChainRemoved = true;
        }
    }
}
