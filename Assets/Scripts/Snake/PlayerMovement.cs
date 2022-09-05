using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _02_Scripts.Snake
{
    //方向类型
    public enum DirectionType
    {
        Horizontal,
        Vertical
    }

    public class PlayerMovement : MonoBehaviour
    {
        private static Vector3 s_direction = new(1, 0, 0); //移动方向
        public float currentTimespeed; //当前移动速度时间（时间越小 速度越快 这里我通过时间来加快速度）
        public float timerSpeedRatio; //每次吃到食物所减去的时间（即：贪吃蛇加速）
        private float _timer; //用于时间计算

        private bool _isDie; //是否死亡
        private int _foodNumber; //吃掉的食物数量

        //食物 食物对象池 
        public FoodData foodPrefab;

        public Transform foodPool;

        //贪吃蛇容器 用于存储吃掉的食物变成的身体
        public Transform snakePool;

        //游戏地图
        public Transform gameBg;

        private void Start()
        {
            //初始化timer
            _timer = currentTimespeed;
            //初始化位置
            transform.position = new Vector3(0.5f, 0.5f, 1);
        }

        private void Update()
        {
            //玩家移动
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            //判断移动方向
            if (h != 0)
            {
                SetDirection(DirectionType.Horizontal, h);
            }

            if (v != 0)
            {
                SetDirection(DirectionType.Vertical, v);
            }

            //未死亡 执行移动
            if (!_isDie)
            {
                Move();
            }
        }

        //修改方向
        private static void SetDirection(DirectionType directionType, float direction)
        {
            //判断水平的左右移动 或 垂直的上下移动
            float value = direction > 0 ? 1f : -1f;

            //判断方向类型  设置移动方向 
            switch (directionType)
            {
                case DirectionType.Horizontal:
                    if (s_direction.x == 0)
                    {
                        s_direction = new Vector2(value, 0);
                    }

                    break;
                case DirectionType.Vertical:
                    if (s_direction.y == 0)
                    {
                        s_direction = new Vector2(0, value);
                    }

                    break;
                default:
                    break;
            }
        }

        //移动
        private void Move()
        {
            _timer -= Time.deltaTime;

            if (_timer < 0)
            {
                //执行移动
                GetComponent<FoodData>().Move(transform.position + s_direction);
                _timer = currentTimespeed;
            }
        }

        //检测
        private void OnTriggerEnter2D(Collider2D col)
        {
            //判断是否触发了墙或者是自己
            if (col.transform.tag.Equals("Wall") || col.transform.tag.Equals("Player"))
            {
                //死亡
                _isDie = true;
                UIManager.uiManager.DieUI();
            }

            //食物
            if (col.transform.tag.Equals("Food"))
            {
                //判断贪吃蛇有没有身体
                if (snakePool.childCount > 0)
                {
                    col.transform.GetComponent<FoodData>().SetDataInt
                        (snakePool.GetChild(snakePool.childCount - 1).GetComponent<FoodData>());
                }

                if (snakePool.childCount <= 0)
                {
                    col.transform.GetComponent<FoodData>().SetDataInt(GetComponent<FoodData>());
                }

                col.transform.SetParent(snakePool);
                if (col.transform == snakePool.GetChild(0))
                {
                    col.transform.tag = "Untagged";
                }
                else
                {
                    col.transform.tag = "Player";
                }

                //创建食物
                CreatFood();
                //加速
                currentTimespeed -= timerSpeedRatio;
                if (currentTimespeed <= 0.1f)
                {
                    currentTimespeed = 0.1f;
                }

                //UI
                _foodNumber++;
                UIManager.uiManager.SetNumber(_foodNumber);
            }
        }

        //创建食物
        private void CreatFood()
        {
            //基础值
            Vector3 localScale = gameBg.localScale;
            
            int hValue = (int) (localScale.x / 2);
            int vValue = (int) (localScale.y / 2);
            int h = Random.Range(-hValue, hValue + 1);
            int v = Random.Range(-vValue, vValue + 1);

            Vector2 pos = new Vector2(h + 0.5f, v + 0.5f);
            if (pos.x > 15)
            {
                pos = new Vector2(hValue - 0.5f, pos.y);
            }

            if (pos.y > 15)
            {
                pos = new Vector2(pos.x, vValue - 0.5f);
            }

            GetFood(0).position = pos;
        }

        //获取食物
        private Transform GetFood(int index)
        {
            // Transform food;
            // if (index <= foodPool.childCount)
            // {
            //     food = Instantiate(foodPrefab.transform, foodPool);
            // }
            // else
            // {
            //     food = foodPool.GetChild(index);
            // }
            // return food;

            Transform food = index <= foodPool.childCount
                ? Instantiate
                    (foodPrefab.transform, foodPool)
                : foodPool.GetChild(index);
            return food;
        }
    }
}