using UnityEngine;

public abstract class LoadComponents : MonoBehaviour 
{
    protected void LoadComponentInParent<T>(ref T component) where T : Component
    {
        if (component != null) return;
        component = GetComponentInParent<T>(true);
        this.ActionOnLoad();
        this.LogOnLoad<T>(transform);
    }
    protected void LoadComponentInChild<T>(ref T component) where T : Component
    {
        if (component != null) return;
        component = GetComponentInChildren<T>(true);
        this.ActionOnLoad();
        this.LogOnLoad<T>(transform);
    }
    protected void LoadComponent<T>(ref T component, Transform Obj) where T : Component
    {
        if (component != null) return;
        component = Obj.GetComponent<T>();
        this.ActionOnLoad();
        this.LogOnLoad<T>(Obj);
    }
    protected virtual void ActionOnLoad() 
    {
    }
    protected void LogOnLoad<T>(Transform Transform ) 
    {
        Debug.Log($"Load Component {typeof(T).Name} in " + Transform.name);
    
    }
}