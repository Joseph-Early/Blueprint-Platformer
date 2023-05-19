using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    /// <summary>
    /// Store global variables for the game
    /// Only use this for variables that are used in multiple places and are not specific to a single namespace
    /// E.g. Dialogue should not be stored here as dialogue should use its own singleton
    /// </summary>
    public class Globals : MonoBehaviour
    {
        // Singleton
        public static Globals Instance;

        #region Public variables
        // Store a reference to the object information panel
        public GameObject ObjectInformationPanel;
        // Store a reference to the object information panel text for updating
        public TextMeshProUGUI ObjectInformationPanelObjectName, ObjectInformationPanelFunctions, ObjectInformationPanelEnabled;
        #endregion

        // Set the singleton
        private void Awake() {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
        }
    }
}