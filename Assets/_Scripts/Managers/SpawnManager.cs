using System;
using UnityEngine;
using Zenject;

public class SpawnManager : IInitializable, IDisposable
{
    [Inject] private Actor.Pool _poolActor;

    private int _actorSpawnCount = 0;
    
    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        
    }
    
    public void CreateActor(Vector3 position, Vector3 forward, float speed, EActrorType actrorType)
    {
        _actorSpawnCount++;
        var actor = _poolActor.Spawn(position, forward, speed, actrorType);
        actor.SetCount(_actorSpawnCount);        
    }
}
