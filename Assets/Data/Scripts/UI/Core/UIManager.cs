
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour, IUIManager
{
    [Inject] IInputProvider InpurProvider;

    protected Stack<IUIElement> UiContainer = new();
    protected bool isInputBlocked = false;



    protected void OnEnable()
    {
        GameContext.Instance.InjectInto(this);
        this.InpurProvider.ESCPressed += () =>
        { 
            this.CloseTopUI();
            this.HandleCursor();
        };
    }

    public void CloseAllUI()
    {
     
    }

    public void CloseTopUI()
    {
        if (this.UiContainer.Count == 0) return;
        IUIElement topUI = this.UiContainer.Pop();
        if (topUI == null) return;
        this.CloseUI(topUI);

    }
    public void CloseUI(IUIElement uiElement)
    {
        if (uiElement == null || this.UiContainer.Count == 0) return;

        Stack<IUIElement> tempStack = new Stack<IUIElement>();
        while (this.UiContainer.Count > 0)
        {
            IUIElement top = this.UiContainer.Pop();
            if (top == uiElement)
            {
                top.Hide();
                break;
            }
            tempStack.Push(top);
        }

        while (tempStack.Count > 0)
        {
            this.UiContainer.Push(tempStack.Pop());
        }

    }

    public void OpenUI(IUIElement UiElement)
    {
        if (UiElement == null || UiElement.IsActive) return;
        UiElement.Show();
        this.UiContainer.Push(UiElement);
        this.HandleCursor();
    }
    public void HandleCursor()
    {
        bool isAnyUIOpen = this.UiContainer.Count > 0;
        if (isAnyUIOpen)   Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = isAnyUIOpen;
    }


}