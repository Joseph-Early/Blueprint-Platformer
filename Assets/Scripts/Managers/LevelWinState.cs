using System.Collections;
using UnityEngine;
using System.Linq;

namespace Managers
{

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
                if (conditions.All(x => x.conditionMet))
                {
                    // Win condition met
                    Debug.Log("Win condition met");
                }

                // Wait                
                yield return new WaitForSecondsRealtime(.5f);
            }
        }

        #region Condition value checks
        // Check if the rotation of a game object is equal to a certain angle
        private bool CheckConditionRotation(GameObject gm, float angle)
        {
            var trans = gm.GetComponent<Transform>();

            // Check angle (2D)
            if (trans.eulerAngles.z == angle)
                return true;

            return false;

        }

        // Check if the scale of a game object is equal to a certain scale
        private bool CheckConditionScale(GameObject gm, Vector2 scale)
        {
            var trans = gm.GetComponent<Transform>();

            // Check X and Y
            if (trans.localScale.x == scale.x && trans.localScale.y == scale.y)
                return true;

            return false;

        }

        // Check if the position of a game object is equal to a certain position
        private bool CheckConditionPosition(GameObject gm, Vector2 position)
        {
            var trans = gm.GetComponent<Transform>();

            // Check X and Y
            if (trans.position.x == position.x && trans.position.y == position.y)
                return true;

            return false;

        }

        // Check if the colour of a sprite is equal to a certain colour
        private bool CheckConditionColour(GameObject gm, Vector4 colour)
        {
            var sprite = gm.GetComponent<SpriteRenderer>();

            if (sprite.color == new Color(colour.x, colour.y, colour.z, colour.z) && sprite.color.a == colour.w)
                return true;

            return false;
        }

        #endregion

        #region Win condition struct and enums
        // Win condition struct to hold data
        [System.Serializable]
        private struct WinConditions
        {
            [SerializeField] public WinType state;  // Condition type - on value equals or on goal reached
            [SerializeField] public Vector4 value;  // Value to check (e.g. colour, position, scale, angle etc.)
            [SerializeField] public GameObject objectReference; // Reference to game object to check the value of
            [SerializeField] public ValueToCheck toCheck; // What value to check
            [SerializeField] public bool conditionMet; // If the condition has been met or not

            // Constructor
            WinConditions(Vector4 value, WinType state = WinType.OnGoalReached, ValueToCheck toCheck = ValueToCheck.NotApplicable, GameObject objectReference = null)
            {
                this.state = state;
                this.toCheck = toCheck;
                this.value = value;
                this.objectReference = objectReference;
                conditionMet = false;
            }
        }

        // The variable to check
        private enum ValueToCheck
        {
            Colour4,        // RGBA (0-1)
            Position2D,     // X and Y
            Scale2D,        // X and Y
            Angle,          // Z
            NotApplicable,  // Not applicable
        }

        // Win condition types
        enum WinType
        {
            OnValueEquals,  // Check if a value equals a certain value
            OnGoalReached   // Check if the goal has been reached
        }
        #endregion
    }
}