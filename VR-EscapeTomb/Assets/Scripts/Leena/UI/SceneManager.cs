using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TheTomb.UI
{
    public class SceneManager : MonoBehaviour
    {
        [SerializeField] private UIRaycast canvasPointer;
        private void Start()
        {
            if (canvasPointer == null)
                FindObjectOfType<UIRaycast>();

            canvasPointer.gameObject.SetActive(false);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
