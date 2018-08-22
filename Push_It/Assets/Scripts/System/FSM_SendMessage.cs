using UnityEngine;

public class FSM_SendMessage : StateMachineBehaviour
{
    public delegate void OnFSMEvent();
    static public event OnFSMEvent _onEvent;

    public string m_strOnStateEnter  = string.Empty;
    public string m_strOnStateUpdate = string.Empty;
    public string m_strOnStateExit   = string.Empty;
    public string m_strOnStateMove   = string.Empty;
    public string m_strOnStateIK     = string.Empty;

    static private readonly string GAME_START= "GameStart";

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(m_strOnStateEnter))
        {
            animator.SendMessage(m_strOnStateEnter, SendMessageOptions.RequireReceiver);
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(m_strOnStateUpdate))
        {
            animator.SendMessage(m_strOnStateUpdate, SendMessageOptions.RequireReceiver);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(m_strOnStateExit == GAME_START)
        {
            if (_onEvent != null)
                _onEvent();

            return;
        }

        if (!string.IsNullOrEmpty(m_strOnStateExit))
        {
            animator.SendMessage(m_strOnStateExit, SendMessageOptions.RequireReceiver);
        }
    }

    //OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(m_strOnStateMove))
        {
            animator.SendMessage(m_strOnStateMove, SendMessageOptions.RequireReceiver);
        }
    }

    //OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK(inverse kinematics) should be implemented here.
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!string.IsNullOrEmpty(m_strOnStateIK))
        {
            animator.SendMessage(m_strOnStateIK, SendMessageOptions.RequireReceiver);
        }
    }
}
