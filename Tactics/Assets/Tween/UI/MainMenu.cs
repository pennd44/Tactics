using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
public interface IMainMenu : IDependency<IMainMenu>
{
    void Setup(bool hasSavedGame);
    UniTask TransitionIn();
    UniTask<MainMenuOption> SelectMenuOption();
    UniTask TransitionOut();
}
public enum MainMenuOption
{
    NewGame,
    Continue
}
public class MainMenu : MonoBehaviour, IMainMenu
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] CanvasGroup rootGroup;
    [SerializeField] CanvasGroup menuGroup;
    [SerializeField] Layout offscreen;
    [SerializeField] Layout onscreen;
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;
    CancellationTokenSource cts = new CancellationTokenSource();
    public void Setup(bool hasSavedGame)
    {
        continueButton.gameObject.SetActive(hasSavedGame);
    }
    public async UniTask TransitionIn()
    {
        await UniTask.WhenAny(
            Enter(cts),
            SkipEnter(cts));
    }
    void CancelToken()
    {
        if (cts != null)
        {
            cts.Cancel();
            cts.Dispose();
            cts = null;
        }
    }
    async UniTask Enter(CancellationTokenSource cts)
    {
        rootPanel.SetLayout(offscreen);
        menuGroup.alpha = 0;
        rootGroup.alpha = 1;
        await rootPanel.Layout(offscreen, onscreen, 5).Play(cts.Token);
        await menuGroup.FadeIn(1).Play(cts.Token);
       CancelToken();
    }
    async UniTask SkipEnter(CancellationTokenSource cts)
    {
        while (true)
        {
            await UniTask.NextFrame(cts.Token);
            if (Input.anyKey)
            {
                CancelToken();
                rootPanel.SetLayout(onscreen);
                menuGroup.alpha = 1;
                break;
            }
        }
    }
    public async UniTask<MainMenuOption> SelectMenuOption()
    {
        var result = await UniTask.WhenAny(
            Press(newGameButton),
            Press(continueButton)
            );
        return (MainMenuOption)result;
    }
    async UniTask Press(Button button)
    {
        using (var handler = button.GetAsyncClickEventHandler(this.GetCancellationTokenOnDestroy()))
        {
            await handler.OnClickAsync();
        }
    }
    public async UniTask TransitionOut()
    {
        await rootGroup.FadeOut().Play();
        await rootGroup.FadeOut().Play(this.GetCancellationTokenOnDestroy());
    }
    void OnEnable()
    {
        IMainMenu.Register(this);
    }
    void OnDisable()
    {
        CancelToken();
        IMainMenu.Reset();
    }
}