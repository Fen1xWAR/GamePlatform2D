using UnityEditor;
using UnityEngine;
using System.IO;
using System;

namespace Scripts
{
    public class DeleteSave: MonoBehaviour
    {
        public void Delete(string path)
        {
            if (path != null)
            {
                path = Application.persistentDataPath + "/player.nya";
                File.Delete(path);
            }     
        }
       
    }
}