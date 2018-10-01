using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Author: CG-Tespy
/// </summary>
/// 
namespace CGTespy.Utils
{
	public interface ISaveState<T>
	{
		void ApplyTo(T toApplyTo);
		void SetFrom(T toSetFrom);
		
	}
}
