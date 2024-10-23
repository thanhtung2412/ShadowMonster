using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (!CombatManager.instance.player.stunned)
		{
			CombatManager.instance.player.canJump = true;
			CombatManager.instance.canReceiveInput = true;
		}
		animator.SetInteger("JumpState", 0);
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{      
        if (!CombatManager.instance.player.stunned)
		{
			CombatManager.instance.player.canMove = true;
		}
		if (CombatManager.instance.inputReceived)
		{			
			animator.SetTrigger("Attack");
			CombatManager.instance.InputManager();
			CombatManager.instance.inputReceived = false;
		}
	}
}
