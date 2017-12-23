using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScore : MonoBehaviour {

	[SerializeField]
	int scoreA = 0;

	[SerializeField]
	int scoreB = 0;

	[SerializeField]
	int scoreC = 0;

	[SerializeField]
	int scoreD = 0;

	int[] score;
	// Use this for initialization
	void Start () {
		score = new int[4];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetServerScore(int num, int Score)
	{
		score [num] += Score;

		scoreA += score [0];
		scoreB += score [1];
		scoreC += score [2];
		scoreD += score [3];
	}
}
