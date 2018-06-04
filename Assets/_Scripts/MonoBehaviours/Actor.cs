using System;
using System.Collections;
using System.Collections.Generic;
using Slash.Unity.DataBind.Core.Presentation;
using UnityEngine;
using Zenject;

public class Actor : MonoBehaviour
{
	[SerializeField] private EActrorType _actrorType;
	
	public GameObject BoxGreen;
	public GameObject SphereBlue;
	public GameObject SphereRed;		
	
	[Inject] Pool _actorPool;

	private float _speed;
	private ActorUIContext _contextUI;

	private void Awake()
	{
		var contextHolder = GetComponentInChildren<ContextHolder>();
		if (contextHolder)
		{			
			contextHolder.Context = new ActorUIContext();
			_contextUI = (ActorUIContext) contextHolder.Context;
		}
		else
			Debug.LogError("[Actor] contextHolder not found");
	}

	void Update () 
	{
		transform.position += transform.forward * _speed * Time.deltaTime;
	}
	
	public void Despawn()
	{
		_actorPool.Despawn(this);
	}

	public void SetCount(int count)
	{
		if (_contextUI != null)
			_contextUI.TextCount = count.ToString();
	}
	
	public class Pool : MonoMemoryPool<Vector3, Vector3, float, EActrorType, Actor>
	{
		protected override void Reinitialize(Vector3 position, Vector3 forward, float speed, EActrorType actrorType, Actor actor)
		{
			actor._speed = speed;				
			actor.transform.position = position;
			actor.transform.forward = forward;		

			actor.BoxGreen.SetActive(false);
			actor.SphereBlue.SetActive(false);
			actor.SphereRed.SetActive(false);
			
			switch (actrorType)
			{
				case EActrorType.BoxGreen:
					actor.BoxGreen.SetActive(true);
					break;
				case EActrorType.SphereBlue:
					actor.SphereBlue.SetActive(true);
					break;
				case EActrorType.SphereRed:
					actor.SphereRed.SetActive(true);
					break;
				default:
					throw new ArgumentOutOfRangeException("aType", actrorType, null);
			}
		}		
	}

}
