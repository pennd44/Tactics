using UnityEngine;
using Cysharp.Threading.Tasks;
public class AppFlow : MonoBehaviour
{
    async UniTaskVoid Start()
    {
        DontDestroyOnLoad(gameObject);
        IMainMenuFlow.Register(new MainMenuFlow());
        IDataSerializer.Register(new DataSerializer());
        IDataStore.Register(new DataStore("GameData"));
        IDataSystem.Register(new DataSystem());
        while (true)
        {
            await IMainMenuFlow.Resolve().Play();
            await UniTask.NextFrame(this.GetCancellationTokenOnDestroy());
        }
    }
}