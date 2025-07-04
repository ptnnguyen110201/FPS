using Cysharp.Threading.Tasks;
using System;
[Serializable]
public abstract class GameSystemInstaller
{
    public abstract UniTask Install(DIContainer container);

}
