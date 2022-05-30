using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class SceneChangeComponent : MonoBehaviour
    {
        public void ChangeScene(string _nextScene)
        {
            SceneManager.LoadSceneAsync(_nextScene);
        }
    }
}


