public interface IUIManager 
{
    void OpenUI(IUIElement UiElement);
    void CloseUI(IUIElement UiElement);
    void CloseTopUI();
    void CloseAllUI();

    void HandleCursor();
   

}
