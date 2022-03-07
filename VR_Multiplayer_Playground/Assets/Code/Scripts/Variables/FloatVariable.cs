using UnityEngine;
using System;

[CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject, ISerializationCallbackReceiver
{
	public float InitialValue;

	[NonSerialized]
	public float Value;

	public void OnAfterDeserialize()
	{
		Value = InitialValue;
	}

	public void OnBeforeSerialize() { }
}
