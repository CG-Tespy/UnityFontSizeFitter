using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using CGTespy.Utils;

/// <summary>
/// Author: CG-Tespy
/// </summary>

namespace CGTespy.UI
{
	public class RectTransformStretchState : ISaveState<RectTransform>, System.IEquatable<RectTransformStretchState>
	{
		public Vector2 offsetMin, offsetMax;
		public float left 
		{
			get  						{ return offsetMin.x; }
			set 						{ offsetMin.x = value; }
		}

		public float right 
		{
			get 						{ return offsetMax.x; }
			set 						{ offsetMax.x = value; }
		}

		public float top 
		{
			get 						{ return offsetMax.y; }
			set 						{ offsetMax.y = value; }
		}

		public float bottom 
		{
			get 						{ return offsetMin.y; }
			set 						{ offsetMin.y = value; }
		}
		
		public RectTransformStretchState(RectTransform toCreateFrom)
		{
			SetFrom(toCreateFrom);
		}

		public void SetFrom(RectTransform rectTransform)
		{
			offsetMin = 				rectTransform.offsetMin;
			offsetMax = 				rectTransform.offsetMax;
		}

		public void ApplyTo(RectTransform rectTransform)
		{
			rectTransform.offsetMin = 	this.offsetMin;
			rectTransform.offsetMax = 	this.offsetMax;
		}

		public bool Equals(RectTransformStretchState other)
		{
			return this.offsetMin.Equals(other.offsetMin) && this.offsetMax.Equals(other.offsetMax);
		}

		public RectTransformStretchState CreateFrom(RectTransform toCreateFrom)
		{
			return new RectTransformStretchState(toCreateFrom);
		}
	}
}