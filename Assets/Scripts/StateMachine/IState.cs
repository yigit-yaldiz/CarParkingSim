﻿using System.Collections;
using UnityEngine;

namespace StateMachineBehaviour
{
    public interface IState
    {
        void OnEnter();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}