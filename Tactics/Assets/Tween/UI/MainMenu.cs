using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;

public enum MainMenuOption
{
    NewGame,
    Continue
}
public class MainMenu : MonoBehaviour
{
    [SerializeField] RectTransform rootPanel;
    [SerializeField] CanvasGroup rootGroup;
    [SerializeField] CanvasGroup menuGroup;
    [SerializeField] Layout offscreen;
    [SerializeField] Layout onscreen;
    [SerializeField] Button continueButton;
    [SerializeField] Button newGameButton;
        //Good example of async either or
    void Start()
    {
        DemoFlow().Forget();
    }
    async UniTask DemoFlow()
    {
        while (true)
        {
            Setup(Random.value > 0.5f);
            await TransitionIn();
            var option = await SelectMenuOption();
            Debug.Log("Selected: " + option.ToString());
            await TransitionOut();
        }
    }
    void Setup(bool hasSavedGame)
    {
        continueButton.gameObject.SetActive(hasSavedGame);
    }
    async UniTask TransitionIn()
    {
        var cts = new CancellationTokenSource();
        await UniTask.WhenAny(
            Enter(cts),
            SkipEnter(cts));
        cts.Dispose();
    }
    async UniTask Enter(CancellationTokenSource cts)
    {
        rootPanel.SetLayout(offscreen);
        menuGroup.alpha = 0;
        rootGroup.alpha = 1;
        await rootPanel.Layout(offscreen, onscreen, 5).Play(cts.Token);
        await menuGroup.FadeIn(1).Play(cts.Token);
        cts.Cancel();
    }
    async UniTask SkipEnter(CancellationTokenSource cts)
    {
        while (true)
        {
            await UniTask.NextFrame(cts.Token);
            if (Input.anyKey)
            {
                cts.Cancel();
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
    async UniTask TransitionOut()
    {
        await rootGroup.FadeOut().Play();
    }
}