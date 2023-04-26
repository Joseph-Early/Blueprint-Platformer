using UnityEngine;
using Managers;
using static Managers.LevelWinState;
using TMPro;
using System.Text;

namespace GUI
{
    public class Objectives : MonoBehaviour
    {
        [SerializeField] GameObject textObjective;

        // Update is called once per frame
        void Update()
        {
            StringBuilder sb = new StringBuilder("");

            sb.AppendLine("Objectives:");
            foreach (var obj in Globals.Instance.GetComponent<Managers.LevelWinState>().conditions)
            {
                var metString = IsCompleteString(obj);

                if (obj.state == WinType.OnGoalReached)
                    sb.AppendLine($"Reach the goal {metString}");

                else if (obj.toCheck == ValueToCheck.Position2D)
                    sb.Append($"Set the position to {obj.value.x}, {obj.value.y} {metString}");

                else if (obj.toCheck == ValueToCheck.Scale2D)
                    sb.Append($"Set the Scale to {obj.value.x}, {obj.value.y} {metString}");

                else if (obj.toCheck == ValueToCheck.Colour4)
                    sb.Append($"Set the colour to {obj.value.x}, {obj.value.y}, {obj.value.z}, {obj.value.w} {metString}");

                else if (obj.toCheck == ValueToCheck.Angle)
                    sb.Append($"Set the angle to {obj.value.x} {metString}");
                    
                else
                    sb.Append($"Error! {metString}");
            }

            textObjective.GetComponent<TextMeshProUGUI>().text = sb.ToString();
        }

        private string IsCompleteString(WinConditions condition)
        {
            return " - " + (condition.conditionMet ? "Complete" : "Not met");
        }
        
    }
}