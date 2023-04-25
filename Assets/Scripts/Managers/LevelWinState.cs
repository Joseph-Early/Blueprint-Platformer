namespace Managers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LevelWinState : MonoBehaviour
    {
        // Tracks if the goal has been reached by the player
        public bool goalReached = false;

        // Expose conditions to beat the level to the inspector
        [Header("Win Conditions")]
        [Tooltip("Win conditions for the level")]
        [SerializeField] WinConditions[] conditions;

        // Win condition checks
        void Start() => StartCoroutine(CheckAllConditionsAreMet());

        // Every half a second, check all win conditions are met
        private IEnumerator CheckAllConditionsAreMet()
        {
            while (true)
            {
                // Update each conditions' state
                for (var x = 0; x < conditions.Length; x++)
                {
                    switch (conditions[x].toCheck)
                    {
                        case ValueToCheck.Colour4:
                            conditions[x].conditionMet = CheckConditionColour(conditions[x].objectReference, conditions[x].value);
                            break;
                        case ValueToCheck.Position2D:
                            conditions[x].conditionMet = CheckConditionPosition(conditions[x].objectReference, new Vector2(conditions[x].value.x, conditions[x].value.y));
                            break;
                        case ValueToCheck.Scale2D:
                            conditions[x].conditionMet = CheckConditionScale(conditions[x].objectReference, new Vector2(conditions[x].value.x, conditions[x].value.y));
                            break;
                        case ValueToCheck.Angle:
                            conditions[x].conditionMet = CheckConditionRotation(conditions[x].objectReference, conditions[x].value.x);
                            break;
                    }
                    if (conditions[x].state == WinType.OnGoalReached)
                        conditions[x].conditionMet = goalReached;
                }

                // Check all conditions are truth
                var i = 0;
                foreach (var winCondition in conditions)
                {
                    // Conditions not met
                    if (winCondition.conditionMet)
                        i++;
                }
                // If i is greater than or equal to winConditions length, player has won
                if (i >= conditions.Length)
                {
                    Debug.Log("Player has Won");
                }

                // Wait                
                yield return new WaitForSecondsRealtime(.5f);
            }
        }

        private bool CheckConditionRotation(GameObject gm, float angle)
        {
            var trans = gm.GetComponent<Transform>();

            // Check angle (2D)
            if (trans.eulerAngles.z == angle)
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

            if (sprite.color == new Color(colour.x, colour.y, colour.z, colour.z) && sprite.color.a == colour.w)
                return true;
            
            return false;
        }
        
        // Win condition types
        enum WinType
        {
            OnValueEquals,
            OnGoalReached
        }

        // Win condition struct to hold data
        [System.Serializable]
        struct WinConditions
        {
            [SerializeField] public WinType state;
            [SerializeField] public Vector4 value;
            [SerializeField] public GameObject objectReference;
            [SerializeField] public ValueToCheck toCheck;
            [SerializeField] public bool conditionMet;

            WinConditions(Vector4 value, WinType state=WinType.OnGoalReached, ValueToCheck toCheck=ValueToCheck.NotApplicable, GameObject gm=null)
            {
                this.state = state;
                this.toCheck = toCheck;
                this.value = value;
                this.objectReference = gm;
                conditionMet = false;
            }
        }

        // The variable to check
        enum ValueToCheck
        {
            Colour4,
            Position2D,
            Scale2D,
            Angle,
            NotApplicable,
        }

    }
}