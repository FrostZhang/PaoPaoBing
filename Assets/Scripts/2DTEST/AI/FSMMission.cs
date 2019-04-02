using FSM;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class GameMission
    {
        public GameMission()
        {
            GameEvent.PlayerData.OnChange_Level += OnPlayerLevel;
        }

        //玩家升级会通知任务系统  发现新任务
        private void OnPlayerLevel(int level)
        {
                
        }

        ~GameMission()
        {
            GameEvent.PlayerData.OnChange_Level -= OnPlayerLevel;
        }
    }
}