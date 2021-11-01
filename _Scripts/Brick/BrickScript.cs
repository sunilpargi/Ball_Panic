using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private AnimationClip clip;

	public float x, y;

	// Use this for initialization
	void Start()
	{
		transform.position = Camera.main.ViewportToWorldPoint(new Vector3(x, y, 0));
	}

	// Update is called once per frame
	void Update()
	{

	}

	public IEnumerator BreakTheBrick()
	{

		animator.Play("BrickBreak");
		yield return new WaitForSeconds(clip.length);
		gameObject.SetActive(false);

	}
}
