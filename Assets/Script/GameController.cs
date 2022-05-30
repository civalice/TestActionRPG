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
        public Text SkillText;

        public GameObject SettingCanvas;
        public InputField TxtMaxHp;
        public InputField TxtMeleeDamage;
        public InputField TxtMeleeForce;
        public InputField TxtMaxSpawn;

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
            TxtMaxHp.text = PlayerMaxHp.ToString();
            TxtMeleeDamage.text = MeleeDamage.ToString();
            TxtMeleeForce.text = MeleeForce.ToString();
            TxtMaxSpawn.text = MaxSpawn.ToString();
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
            SettingCanvas.SetActive(false);
            Player.Reset();
            SkillText.text = Player.GetSkillListName();
            SpawnManager.Reset();
            SpawnManager.StartSpawn();
            state = GameState.GamePlay;
        }

        private void GameOver()
        {
            SpawnManager.Clear();
            SpawnManager.StopSpawn();
            StartText.gameObject.SetActive(true);
            SettingCanvas.SetActive(true);
            StartText.text = "GameOver\n Press Space to continue...";
            state = GameState.GameOver;
        }

        public void Kill(GameObject obj)
        {
            SpawnManager.Kill(obj);
        }

        public void PlayerHpValueChange(string val)
        {
            Setting.PlayerMaxHp = int.Parse(val);
        }

        public void MeleeDamageValueChange(string val)
        {
            Setting.MeleeDamage = int.Parse(val);
        }
        public void MeleeForceValueChange(string val)
        {
            Setting.MeleeForce = float.Parse(val);
        }
        public void MaxSpawnValueChange(string val)
        {
            Setting.MaxSpawn = int.Parse(val);
        }

    }
}