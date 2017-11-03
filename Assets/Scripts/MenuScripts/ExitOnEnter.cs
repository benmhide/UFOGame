using UnityEngine;
using System.Collections;

public class ExitOnEnter : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state.
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exit();
    }

    public void Exit()
        {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
