using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Image[] images;
    void Start()
    {
        ItemInitializer();
    }

    void Update()
    {
        
    }

    void ItemInitializer()
    {
        foreach (Image img in images)
        {
            Color newColor = img.color;
            if (img.sprite == null)
            {
                newColor.a = 0;
                img.color = newColor;
            } else
            {
                newColor.a = 1;
                img.color = newColor;
            }
        }
    }
}
    