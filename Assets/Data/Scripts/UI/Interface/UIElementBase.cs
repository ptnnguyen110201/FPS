using UnityEngine;

public abstract class UIElementBase : MonoBehaviour, IUIElement
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public bool IsActive => gameObject.activeSelf;
}
