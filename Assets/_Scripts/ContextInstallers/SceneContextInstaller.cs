using Slash.Unity.DataBind.Core.Presentation;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SceneContextInstaller : MonoInstaller<SceneContextInstaller>
{
    public Sprite SpriteRectangleGreen;
    public Sprite CircleBlue;
    public Sprite CircleRed;
    
    public GameObject Actor;
    
    private const int POOL_ACTOR_COUNT = 30;
    
    [Inject] private ContextHolder _contextHolder;
    
    public override void InstallBindings()
    {
        //декларация сигналов сцены
        Container.DeclareSignal<SignalItemDragBegin>();
        Container.DeclareSignal<SignalItemDragEnd>();
        Container.DeclareSignal<SignalItemDrag>();
        
        var graphicRaycaster = _contextHolder.GetComponent<GraphicRaycaster>();
        //бинды        
        Container.BindInterfacesAndSelfTo<SceneModel>().AsSingle().WithArguments(
            graphicRaycaster,
            SpriteRectangleGreen,
            CircleBlue,
            CircleRed).NonLazy();
        Container.Bind<AsyncProcessor>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SpawnManager>().AsSingle().NonLazy();
        
        //pools
        Container.BindMemoryPool<Actor, Actor.Pool>().WithInitialSize(POOL_ACTOR_COUNT).FromComponentInNewPrefab(Actor).UnderTransformGroup("Pool_Actors");
        
        //создать и назначить контекст гуи
        var uiContext = new SceneUIContext();
        Container.BindInterfacesAndSelfTo(uiContext.GetType()).FromInstance(uiContext).AsSingle();
        Container.Inject(uiContext);
        _contextHolder.Context = uiContext;
    }
}