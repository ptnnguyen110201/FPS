
using System.Collections.Generic;
using UnityEngine;

public class UICrossHair : LoadComponents 
{
    [SerializeField] protected List<Transform> CrosshairLine = new();
    [SerializeField] protected float currentScale;
    [SerializeField] protected float miniScale = 0.15f;
    [SerializeField] protected float maxScale = 1;

    [Inject] protected IInputProvider InputProvider;
    protected void Reset()
    {
        this.LoadCrosshairLine();
    }
    protected void Update()
    {
        this.UpdateCrossHair();
    }
    protected void LoadCrosshairLine() 
    {
        this.CrosshairLine.Clear();
        foreach (Transform obj in this.transform) 
        {
            this.CrosshairLine.Add(obj);
        }
        this.LogOnLoad<Transform>(transform);
    }


    protected void UpdateCrossHair() 
    {
        if (this.InputProvider.IsAiming) this.currentScale -= Time.deltaTime * 3;
        else this.currentScale += Time.deltaTime * 3;
        this.currentScale = Mathf.Clamp(this.currentScale, this.miniScale, this.maxScale);

        RectTransform rect = transform.GetComponent<RectTransform>();
        rect.localScale = Vector3.one * this.currentScale;
;       
        bool isLimitScale = this.currentScale > this.miniScale;
        for (int i = 0; i < this.CrosshairLine.Count; i++) 
        {
            this.CrosshairLine[i].gameObject.SetActive(isLimitScale);
        }
    }

    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);  
    }
}