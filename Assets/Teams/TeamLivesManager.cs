﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeamLivesManager : MonoBehaviour
{
    public static TeamLivesManager Instance;

    private const int MaxLives = 5;

    private const float RespawnDelay = 1;

    private int _blueLives = MaxLives;
    private int _purpleLives = MaxLives;

    public GameObject blueLivesIcon;
    public GameObject purpleLivesIcon; 

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        UpdateHud();
    }

    public void HandlePlayerDeath(GameObject playerGameObject)
    {
        DisablePlayer(playerGameObject);

        switch (playerGameObject.GetComponent<PlayerController>().Team)
        {
            case Team.Blue:
                _blueLives--;
                if (_blueLives > 0)
                {
                    StartCoroutine(RespawnCoroutine(playerGameObject));
                }
                else
                {
                    Destroy(playerGameObject);
                    EndOfGameManager.Instance.ShowVictoryScreen(Team.Purple);
                }
                break;
            case Team.Purple:
                _purpleLives--;
                if (_purpleLives > 0)
                {
                    StartCoroutine(RespawnCoroutine(playerGameObject));
                }
                else
                {
                    Destroy(playerGameObject);
                    EndOfGameManager.Instance.ShowVictoryScreen(Team.Blue);
                }
                break;
        }

        UpdateHud();
    }

    private void UpdateHud()
    {
        //destroy all icons... there might be a better way?
        GameObject[] objects = GameObject.FindGameObjectsWithTag("LifeIcon");

        for (int i = 0; i < objects.Length; i++)
        {
            Destroy(objects[i]);
        }

        //draw an icon for each life remaining
        float offset = 50f;
        for (int i = 0; i < _purpleLives; i++)
        {
            GameObject ico = Instantiate(purpleLivesIcon);
            ico.transform.SetParent(gameObject.transform, false);
            ico.transform.Translate(i * 30f + offset, 0, 0);
            Debug.Log(ico.transform.position);
        }
        for (int i = 0; i < _blueLives; i++)
        {
            GameObject ico = Instantiate(blueLivesIcon);
            ico.transform.SetParent(gameObject.transform, false);
            ico.transform.Translate(-i * 30f - offset, 0, 0);
            Debug.Log(ico.transform.position);
        }

    }

    private void DisablePlayer(GameObject playerGameObject)
    {
        playerGameObject.SetActive(false);
    }

    private IEnumerator RespawnCoroutine(GameObject playerGameObject)
    {
        yield return new WaitForSeconds(RespawnDelay);
        var team = playerGameObject.GetComponent<PlayerController>().Team;
        playerGameObject.transform.position = BlockColumnManager.Instance.GetRespawnPoint(team);
        playerGameObject.SetActive(true);
    }
}
