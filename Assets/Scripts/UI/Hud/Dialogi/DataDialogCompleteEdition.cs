using UnityEditor;
using UnityEngine;

namespace Scripts
{
    public class DataDialogCompleteEdition : MonoBehaviour
    {
       
        [SerializeField] private DialogDef[] Dialogues;

        private DialogDef external;

        private GameObject Character;
        private ShowDialogComponent Dialog;  
    }
}