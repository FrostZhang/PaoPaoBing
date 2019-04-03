using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public class FSMController
    {
        public Transform transform { get; protected set; }

        public GameObject gameObject { get; protected set; }

        public Dictionary<string, Component> variable;

        List<FSM_State> states;

        public FSM_State CurrentState { get; protected set; }

        public FSM_State MonitorState { get; protected set; }

        public Action<FSM_State, Component> onFsmEvent;

        public FSMController(GameObject target, Transform tr)
        {
            transform = tr;
            gameObject = target;
            states = new List<FSM_State>();
            if (variable == null)
            {
                variable = new Dictionary<string, Component>();
            }
        }

        //交由外部初始化
        public void Initialization()
        {
            foreach (var item in states)
            {
                item.Initialization();
            }
            CurrentState.Start();
        }

        public void AddState(FSM_State state)
        {
            if (!states.Contains(state))
            {
                state.fSMController = this;
                states.Add(state);
            }
            else
            {
                Debug.Log("添加  但已有 state ");
            }
            if (CurrentState == null)
            {
                CurrentState = state;
            }
        }

        public void RemoveState(FSM_State state)
        {
            if (states.Contains(state))
            {
                states.Remove(state);
            }
            else
            {
                Debug.Log("删除  找不到 state ");
            }
        }

        public void UpdateState()
        {
            if (CurrentState != null)
            {
                CurrentState.Update();
            }
            if (MonitorState != null)
            {
                MonitorState.Update();
            }
        }

        public void RunState(FSM_State state)
        {
            if (!states.Contains(state))
            {
                return;
            }
            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = state;
            state.Start();
        }

        public void RunState(string statename)
        {
            var st = states.Find((_) => _.StateName == statename);
            if (st == null)
            {
                return;
            }

            if (CurrentState != null)
            {
                CurrentState.Exit();
            }
            CurrentState = st;
            st.Start();
        }

        /// <summary>
        /// 启用监视程序
        /// </summary>
        /// <param name="state"></param>
        public void Monitor(FSM_State state)
        {
            if (!states.Contains(state))
            {
                return;
            }

            if (MonitorState != null)
            {
                MonitorState.Exit();
            }
            MonitorState = state;
            state.Start();
        }

        /// <summary>
        /// 启用监视程序
        /// </summary>
        /// <param name="state"></param>
        public void Monitor(string statename)
        {
            var st = states.Find((_) => _.StateName == statename);
            if (st == null)
            {
                return;
            }

            if (MonitorState != null)
            {
                MonitorState.Exit();
            }
            MonitorState = st;
            st.Start();
        }

        public FSM_State GetState(string statename)
        {
            return states.Find((_) => _.StateName == statename);
        }
    }

    public abstract class FSM_State
    {

        public string StateName;
        public Transform transform { get { return fSMController.transform; } }
        public GameObject gameObject { get { return fSMController.gameObject; } }
        public FSMController fSMController { get; set; }

        public virtual void Initialization() { }

        public abstract void Start();
        public abstract void Update();
        public abstract void Exit();
    }
}