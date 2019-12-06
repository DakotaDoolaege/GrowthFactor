using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDelay : MonoBehaviour
{
	private void OnEnable()
	{
		StartCoroutine(DelayRemove(3f));

	}
	private IEnumerator DelayRemove(float seconds)
	{
		for (int i = 0; i < 20; i++)
		{
			yield return new WaitForEndOfFrame();
		}
		this.gameObject.SetActive(false);
	}
}
