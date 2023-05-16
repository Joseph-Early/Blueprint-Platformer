// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// namespace Serialisation
// {
//     public class SaveLoad : MonoBehaviour
//     {
//         /*  BLUE file schema
//         *   The first four bytes are reserved for the file signature encoded in binary of the word BLUE (all-caps)
//         *   The next byte stores the version of the blue file format (this is needed in case later versions change schema to allow more than 266 levels)
//         *   The next byte is reserved for storing how many levels this file has (so limited to 266 - 0 meaning 1)
//         *   Each subsequent bit will signify if the level is beaten (1) or not completed (0)
//         *   At the end of the file, the file signature is repeated
//         */

//         [SerializeField] private UnityEditor.SceneAsset[] scenes; // Move this outside of the level manager class
//         private const string fileName = "levelsComplete.blue";  // File name for complete data

//         private const string fileSignifierString = "BLUE"; // File signifier
//         private byte[] fileSignifierBytes; // File signature and end of file determiner

//         private void Awake() {
            
//             // Convert file signifier (string) to bytes
//             var fileSig = new List<byte>(); 
//             foreach (var i in fileSignifierString) fileSig.Add(System.Convert.ToByte(i));

//             // Convert file signature to an array
//             fileSignifierBytes = fileSig.ToArray();
//         }

//         // Start is called before the first frame update
//         void Start()
//         {
//             // Check if the file exists
//             if (!System.IO.File.Exists(fileName))
//             {
//                 // Write file sig
//                 System.IO.File.WriteAllBytes(fileName, fileSignifierBytes);
//             }
//         }

//         // Update is called once per frame
//         void Update()
//         {

//         }
//     }
// }