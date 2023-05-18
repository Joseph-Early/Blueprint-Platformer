using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Managers
{
    public class Globals : MonoBehaviour
    {
        public static Globals Instance;

        #region Public variables
        public GameObject ObjectInformationPanel;
        public TextMeshProUGUI ObjectInformationPanelObjectName, ObjectInformationPanelFunctions, ObjectInformationPanelEnabled;
        #endregion

        private void Awake() {
            if (Instance != null)
                Destroy(Instance);

            Instance = this;
        }
    }
}