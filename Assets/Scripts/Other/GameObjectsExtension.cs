using UnityEngine;

public static class GameObjectExtensions
{
    // �������������� ����� ��� ��������� ���������� � ������������ ��'����
    public static T GetComponentInParent<T>(this GameObject gameObject) where T : Component
    {
        Transform parentTransform = gameObject.transform.parent;

        while (parentTransform != null)
        {
            T component = parentTransform.GetComponent<T>();

            if (component != null)
            {
                return component;
            }

            parentTransform = parentTransform.parent;
        }

        return null;
    }
}
