using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;//加载场景时需引用

namespace _02_Scripts.Snake
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager uiManager;
        public Text numberTxt;//分数
        public Button refreshBtn;//重新开始按钮
        public Transform dieUI;//游戏失败面板

        private void Start()
        {
            //初始化
            uiManager = this;
            dieUI.gameObject.SetActive(false);
            
            refreshBtn.onClick.AddListener(Refresh);
        }

        // Update is called once per frame
        private void Update()
        {
            //按下esc 退出程序
            if (Input.GetKeyDown(KeyCode.Escape))
            {
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
            }
        }

        //修改分数值
        public void SetNumber(int value) 
        {
            numberTxt.text = value.ToString();
        }

        //显示死亡UI
        public void DieUI() 
        {
            dieUI.gameObject.SetActive(true);
        }

        //刷新
        static void Refresh() 
        {
            //重新加载场景
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
