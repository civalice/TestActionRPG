using System;
using System.Collections;
using System.Collections.Generic;
using RPGCharacterAnims;
using RPGCharacterAnims.Actions;
using RPGCharacterAnims.Extensions;
using RPGCharacterAnims.Lookups;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Urnique.GamePlay
{
    public class GameInputController : MonoBehaviour
    {
        RPGCharacterController rpgCharacterController;

        private Vector2 inputMovement;
        private bool inputMelee;
        private bool inputRange;
        private bool inputDash;

        // Variables.
        private Vector3 moveInput;
        public bool HasMoveInput() => moveInput.magnitude > 0.1f;
        public bool HasRangeInput() => inputRange;

        //InputSystem
        public @RPGInputs GameInputSystem;

        void Awake()
        {
            rpgCharacterController = GetComponent<RPGCharacterController>();
            GameInputSystem = new @RPGInputs();
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        private void OnEnable()
        { GameInputSystem.Enable(); }

        private void OnDisable()
        { GameInputSystem.Disable(); }

        // Update is called once per frame
        private void Update()
        {
            Inputs();
            Moving();
            Dash();
            Attacking();
            Aiming();
        }

        private void Inputs()
        {
            try
            {
                inputMovement = GameInputSystem.TestGaming.Move.ReadValue<Vector2>();
                inputDash = GameInputSystem.TestGaming.Roll.WasPressedThisFrame();
                inputMelee = GameInputSystem.TestGaming.Melee.WasPressedThisFrame();
                inputRange = GameInputSystem.TestGaming.Range.IsPressed();
            }
            catch (System.Exception)
            {
                Debug.LogError("Inputs not found!  Character must have Player Input component.");
            }
        }

        public void Moving()
        {
            moveInput = new Vector3(inputMovement.x, inputMovement.y, 0f);

            // Filter the 0.1 threshold of HasMoveInput.
            if (HasMoveInput())
            {
                rpgCharacterController.SetMoveInput(moveInput);
            }
            else
            {
                rpgCharacterController.SetMoveInput(Vector3.zero);
            }
        }

        private void Aiming()
        {
            if (rpgCharacterController.hasAimedWeapon)
            {
                if (rpgCharacterController.HandlerExists(HandlerTypes.Aim))
                {
                    if (HasRangeInput())
                    {
                        rpgCharacterController.TryStartAction(HandlerTypes.Aim);
                    }
                    else
                    {
                        rpgCharacterController.TryEndAction(HandlerTypes.Aim);
                    }
                }

                // Finally, set aim location and bow pull.
                rpgCharacterController.SetAimInput(rpgCharacterController.target.position);
            }
        }

        private void Attacking()
        {
            // Check to make sure Attack and Cast Actions exist.
            if (!rpgCharacterController.HandlerExists(HandlerTypes.Attack))
            {
                return;
            }

            // Check to make character can Attack.
            if (!rpgCharacterController.CanStartAction(HandlerTypes.Attack))
            {
                return;
            }

            if (inputMelee)
            {
                rpgCharacterController.StartAction(HandlerTypes.Attack,
                    new AttackContext(HandlerTypes.Attack, Side.Left));
            }
        }

        public void Dash()
        {
            if (!inputDash)
            {
                return;
            }

            if (!rpgCharacterController.CanStartAction("DiveRoll"))
            {
                return;
            }

            rpgCharacterController.StartAction("DiveRoll", 1);
        }
    }
}