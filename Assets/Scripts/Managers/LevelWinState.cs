namespace Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelWinState : MonoBehaviour
    {
        [SerializeField] WinConditions[] winConditions;

        // Win condition checks
        void Start() => StartCoroutine(CheckAllConditionsAreMet());

        // Every half a second, check all win conditions are met
        private IEnumerator CheckAllConditionsAreMet()
        {
            while (true)
            {
                // Check all conditions are truth
                var i = 0;
                foreach (var winCondition in winConditions)
                {
                    // TODO: Set win condition
                     

                    // Conditions not met
                    if (winCondition.conditionMet)
                        i++;
                }
                // If i is greater than or equal to winConditions length, player has won
                if (i >= winConditions.Length)
                {
                    Debug.Log("Player has Won");
                }

                // Wait                
                yield return new WaitForSecondsRealtime(.5f);
            }
        }

        private bool CheckConditionRotation(GameObject gm, Vector2 rotation)
        {
            var trans = gm.GetComponent<Transform>();

            // Check X and Y
            if (trans.localRotation.x == rotation.x && trans.localRotation.y == rotation.y)
                return true;

            return false;
            
        }

        private bool CheckConditionScale(GameObject gm, Vector2 scale)
        {
            var trans = gm.GetComponent<Transform>();

            // Check X and Y
            if (trans.localScale.x == scale.x && trans.localScale.y == scale.y)
                return true;

            return false;
            
        }

        private bool CheckConditionPosition(GameObject gm, Vector2 position)
        {
            var trans = gm.GetComponent<Transform>();

            // Check X and Y
            if (trans.position.x == position.x && trans.position.y == position.y)
                return true;

            return false;
            
        }

        private bool CheckConditionColour(GameObject gm, Vector4 colour)
        {
            var sprite = gm.GetComponent<SpriteRenderer>();

            if (sprite.color == new Color(colour.x, colour.y, colour.z, colour.z))
                return true;
            
            return false;
        }
        

        enum WinStates
        {
            OnValueEquals,
            OnComplete
        }

        [System.Serializable]
        struct WinConditions
        {
            [SerializeField] WinStates state;
            [SerializeField] Vector3 value;
            [SerializeField] GameObject gm;
            [SerializeField] ValueOptions toCheck;
            public bool conditionMet;

            WinConditions(Vector3 value, WinStates state=WinStates.OnComplete, ValueOptions toCheck=ValueOptions.NA, GameObject gm=null)
            {
                this.state = state;
                this.toCheck = toCheck;
                this.value = value;
                this.gm = gm;
                conditionMet = false;
            }
        }

        enum ValueOptions
        {
            Colour,
            Position,
            Scale,
            NA,
            Rotation,
        }

    }
}