using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    [HideInInspector]
    public UnityEvent tick;
    // In minisecond
    public int tickDuration = 200;
    private CancellationToken token;

    // Start is called before the first frame update
    void Start()
    {
        token = this.GetCancellationTokenOnDestroy();
        GameLoop().Forget();
    }

    public async UniTaskVoid GameLoop()
    {
        while (!token.IsCancellationRequested)
        {
            tick.Invoke();
            await UniTask.Delay(tickDuration, cancellationToken: token);
        }
    }

    public void RegisterTick(UnityAction action)
    {
        tick.AddListener(action);
    }
}
