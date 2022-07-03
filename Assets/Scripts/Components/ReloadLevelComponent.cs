using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class ReloadLevelComponent : MonoBehaviour
    {
        public void Reload()
        {
            var sessiion = FindObjectOfType<GameSession>();
            DestroyImmediate(sessiion);

            var scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
    }
}

