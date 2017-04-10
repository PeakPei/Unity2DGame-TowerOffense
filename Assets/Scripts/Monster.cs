﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

	[SerializeField]
	private float speed;
	public int damage;
	public PlayMap playMap;

	private string monsterType;
	private Stack<PlayTile> path;
	private PlayTile startPath;
	public Coord gridPosition;
	private Vector3 destination;

	// Use this for initialization
	void Start () {
//		Debug.Log ("HELLOOOOO");
		this.name = monsterType;
		SetPath(playMap.path);
	}

	// Update is called once per frame
	void Update () {
		Move();
	}

	void OnMouseDown()
	{
        //Object.FindObjectOfType<PlayMap>().DisplayUpgradePanel(monsterType);
	}

	private void Move()
	{
		transform.position = Vector2.MoveTowards (transform.position, destination, speed*Time.deltaTime);

		if (transform.position == destination) {
			if (path != null && path.Count > 0) {
				gridPosition = path.Peek ().tileData.coord; 
				destination = path.Pop ().transform.position;
				Debug.Log (destination);
			}
			else {
				playMap.ownGameState.hp -= this.damage;
				transform.position = startPath.transform.position;
				gridPosition = startPath.tileData.coord;
				Destroy (this.gameObject);
			}
		}
	}

	void SetPath(List<PlayTile> pathList)
	{
//		Debug.Log(pathList);
		if (pathList != null) {
			List<PlayTile> reversedList = new List<PlayTile> (pathList);
			reversedList.Reverse ();
			this.path = new Stack<PlayTile> (reversedList);
			this.gridPosition = path.Peek().tileData.coord;
			this.startPath = path.Peek ();
			this.destination = path.Pop().transform.position;
		}

	}
}
