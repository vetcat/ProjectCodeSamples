using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

/// <summary>
/// базовая логика сцены 
/// </summary>
public class SceneModel : IInitializable, ITickable, ILateTickable, IDisposable
{
    //сигналы
    [Inject] private SignalItemDragBegin _signalItemDragBegin;
    [Inject] private SignalItemDragEnd _signalItemDragEnd;
    [Inject] private SignalItemDrag _signalItemDrag;

    [Inject] private AsyncProcessor _asyncProcessor;
    [Inject] private SpawnManager _spawnManager;

    private Sprite _spriteRectangleGreen;
    private Sprite _spriteCircleBlue;
    private Sprite _spriteCircleRed;
    
    private GraphicRaycaster _graphicRaycaster;
    private Transform _curentItemCopyTransform;

    private const float RAYCAST_DISTANCE = 100f;
    private const float SPEED_MIN = 5f;
    private const float SPEED_MAX = 10f;
        
    public SceneModel(GraphicRaycaster graphicRaycaster, Sprite spriteRectangleGreen, Sprite spriteCircleBlue, Sprite spriteCircleRed)
    {
        Debug.Log("[SceneModel] construct");
        _graphicRaycaster = graphicRaycaster;
        _spriteRectangleGreen = spriteRectangleGreen;
        _spriteCircleBlue = spriteCircleBlue;
        _spriteCircleRed = spriteCircleRed;
    }
    
    public void Initialize()
    {
        _signalItemDragBegin.Listen(ItemDragBegin);
        _signalItemDragEnd.Listen(ItemDragEnd);
        _signalItemDrag.Listen(ItemDrag);
    }

    public void Tick()
    {
        
    }
    
    public void LateTick()
    {
        SetCurrentItemCopyPosition();
    }

    public void Dispose()
    {
        _signalItemDragBegin.Unlisten(ItemDragBegin);
        _signalItemDragEnd.Unlisten(ItemDragEnd);
        _signalItemDrag.Unlisten(ItemDrag);
    }

    private void SetCurrentItemCopyPosition()
    {
        if (_curentItemCopyTransform)
            _curentItemCopyTransform.position = Input.mousePosition;
    }

    private void ItemDragBegin(PointerEventData eventData)
    {        
        CreateItemTextureCopy(eventData);
    }
    
    private void ItemDragEnd(PointerEventData eventData)
    {     
        if (!_curentItemCopyTransform)
        {
            Debug.LogError("[SceneModel] ItemDragEnd _curentItemCopyTransform is NULL");
            return;
        }

        _asyncProcessor.DestroyGameObject(_curentItemCopyTransform.gameObject);
        
        //проверить нахождение на UI
        List<RaycastResult> resultsUIRaycast = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, resultsUIRaycast);
        //драг завершен на панели UI
        if (resultsUIRaycast.Count != 0)
            return;
        
        //проверить нахождение на 3d
        RaycastHit hitInfo;
        if (CheckRaycast(out hitInfo))
        {
            EActrorType actrorType = EActrorType.None;
            
            if (eventData.pointerDrag.GetComponent<Image>().sprite == _spriteRectangleGreen)
                actrorType = EActrorType.BoxGreen;
            else if (eventData.pointerDrag.GetComponent<Image>().sprite == _spriteCircleBlue)
                actrorType = EActrorType.SphereBlue;
            else if (eventData.pointerDrag.GetComponent<Image>().sprite == _spriteCircleRed)
                actrorType = EActrorType.SphereRed;
            else
                Debug.LogError("[SceneModel] ItemDragEnd sprite type not defined " + eventData.pointerDrag.GetComponent<Image>().sprite);

            if (actrorType != EActrorType.None)
            {
                var forward = Camera.main.transform.forward;
                forward.y = 0f;
                var rndSpeed = UnityEngine.Random.Range(SPEED_MIN, SPEED_MAX);
                _spawnManager.CreateActor(hitInfo.point, forward, rndSpeed, actrorType);
            }
        }         
    }
    
    private void ItemDrag(PointerEventData eventData)
    {        
    }

    void CreateItemTextureCopy(PointerEventData eventData)
    {        
        _curentItemCopyTransform = _asyncProcessor.InstaniateGameObject(eventData.pointerDrag.gameObject).transform;
        _curentItemCopyTransform.parent = eventData.pointerDrag.transform;
    }

    private bool CheckRaycast(out RaycastHit hitInfo)
    {                       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out hitInfo, RAYCAST_DISTANCE);                
    }
}
