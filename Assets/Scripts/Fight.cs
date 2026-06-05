using Cysharp.Threading.Tasks;
using System.Threading;

[System.Serializable]
public class Fight
{
    private PlayerData _playerData;

    public void OnLaunch(PlayerData playerData)
    {
        _playerData = playerData;
    }

    /*
    plus na buduschee. Dlya togo chtobi mozhono bilo menyat' order vo vremya zabega
    neobhodimo brat' iz obschego dostupa PlayerData
    poka eto tol'ko copiya
    */
    public async UniTask FightTask(CancellationToken token, A_Entity entity)
    {
        while (true)
        {
            await UniTask.Yield(token);
            if (token.IsCancellationRequested || !entity)
                break;

            foreach (var item in _playerData.CombatOrder) // neobhodimo proveryat' na izmeneniya
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(item.FillingTime), cancellationToken: token);
                item.Use(entity); // eto gavno potomushto esli buget [Player => heal] pridetca iz drugovo mesta brat'
            }
        }
    }
}
