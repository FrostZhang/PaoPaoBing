using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FSM;

namespace SysFlow
{
    /// <summary>
    /// 用于控制系统进程
    /// </summary>
    public class FSMApp
    {
        public FSMController Sysflow { get; private set; }

        public FSMApp(Transform tr)
        {
            Sysflow = new FSMController(tr.gameObject, tr);
            Login login = new Login();
            Sysflow.AddState(login);

            Sysflow.Initialization();
        }

        public void RunFlow()
        {
            Sysflow.UpdateState();
        }
    }

    //登录
    public class Login : FSM_State
    {
        public Login()
        {
            StateName = Define.FSMAI.LOGIN;
        }

        public override void Exit()
        {
            
        }

        public override void Start()
        {
            GameApp.sceneMG.LoadSceneAsync("talk1");
        }

        public override void Update()
        {
            
        }
    }
}
