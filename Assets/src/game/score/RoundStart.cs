﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RoundStart : MonoBehaviour {

  public GameObject resetObject;
  public Text countdownText;
  public ScoreTracker scoreTracker;

  public GameObject roundObject;

	// Use this for initialization
	void Start () {
    scoreTracker.callback += GoalCallback;

    StartRound();
	}

  void StartRound() {
		var obj = Object.Instantiate(resetObject);
    roundObject = obj;
    obj.SetActive(true);

    var controllers = obj.GetComponentsInChildren<WavePhysicsController>();
    DisableControllers(controllers);
    StartCoroutine(CountdownStart(controllers));
    var originals = resetObject.GetComponentsInChildren<Gibbify>();
    var gibs = obj.GetComponentsInChildren<Gibbify>();
    for (int i = 0; i < gibs.Length; i++) {
      gibs[i].original = originals[i].gameObject;
    }
  }

  void GoalCallback() {
    StartCoroutine(AnimateRoundEnd());
  }

  IEnumerator AnimateRoundEnd() {
    var controllers = roundObject.GetComponentsInChildren<WavePhysicsController>();
    DisableControllers(controllers);
    foreach (var c in controllers) {
      var r = c.GetComponent<Rigidbody>();
      r.useGravity = true;
      r.constraints = RigidbodyConstraints.None;
    }

    scoreTracker.ToggleSize(ScoreTracker.Size.Large);

    yield return new WaitForSeconds(3);

    if (scoreTracker.leftScore >= 5 || scoreTracker.rightScore >= 5) {
      if (scoreTracker.leftScore >= 5) {
        countdownText.text = "Blue Wins!";
      }
      else if (scoreTracker.rightScore >= 5) {
        countdownText.text = "Red Wins!";
      }
      scoreTracker.ToggleSize(ScoreTracker.Size.None);

      yield return new WaitForSeconds(3);

      SceneManager.LoadSceneAsync("scenes/title");

      yield break;
    }

    Object.Destroy(roundObject);

    scoreTracker.ToggleSize(ScoreTracker.Size.Small);

    StartRound();
  }

  void SetCountdown(string text) {
    if (countdownText != null) {
      countdownText.text = text;
    }
  }

  IEnumerator CountdownStart(WavePhysicsController[] controllers) {
    for (int i = 0; i < 3; i++) {
      SetCountdown((3 - i).ToString());
      yield return new WaitForSeconds(1);
    }

    SetCountdown("GO!");
    EnableControllers(controllers);
    yield return new WaitForSeconds(1);

    SetCountdown("");
  }

  void DisableControllers(WavePhysicsController[] controllers) {
    foreach (var c in controllers) {
      c.enabled = false;
    }
  }

  void EnableControllers(WavePhysicsController[] controllers) {
    foreach (var c in controllers) {
      c.enabled = true;
    }
  }

	// Update is called once per frame
	void Update () {
		
	}
}
