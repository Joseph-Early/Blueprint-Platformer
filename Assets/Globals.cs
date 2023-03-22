using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class Globals : MonoBehaviour
    {
        public static Globals Instance;

        #region Public variables
        public GameObject IdentifierNamePopUp;
        #endregion

        private void Awake() => Instance = this;
    }
}