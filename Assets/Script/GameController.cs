using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Urxxxxx.Util;

namespace Urxxxxx.GamePlay
{
    public enum GameState
    {
        Start,
        GamePlay,
        GameOver
    }
    public partial class GameController : MonoBehaviour
    {
        public static GameController Instance;
        public Player Player;
        public SpawnManager SpawnManager;
        public GameSetting Setting;

        public Image HpImage;
        public Text StartText;
        public bool IsStart => state == GameState.GamePlay;

        private GameState state = GameState.Start;
        
        void Awake()
        {
            if (Instance == null) Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            StartText.gameObject.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {
            switch (state)
            {
                case GameState.Start:
                {
                }
                    break;
                case GameState.GamePlay:
                {
                    HpImage.fillAmount = Player.CurrentHp / Player.MaxHp;
                    if (Player.IsDead)
                    {
                        GameOver();
                    }
                }
                    break;
                case GameState.GameOver:
                {
                }
                    break;
            }
        }

        public void StartGame()
        {
            StartText.gameObject.SetActive(false);
            Player.Reset();
            SpawnManager.Reset();
            SpawnManager.StartSpawn();
            state = GameState.GamePlay;
        }

        private void GameOver()
        {
            SpawnManager.Clear();
            SpawnManager.StopSpawn();
            StartText.gameObject.SetActive(true);
            StartText.text = "GameOver\n Press Space to continue...";
            state = GameState.GameOver;
        }

        public void Kill(GameObject obj)
        {
            SpawnManager.Kill(obj);
        }
    }
}