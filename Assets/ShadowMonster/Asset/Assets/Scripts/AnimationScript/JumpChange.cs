using UnityEngine;

public class JumpChange : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("jump", value: true);
		CombatManager.instance.player.canMove = true;
		CombatManager.instance.player.canJump = true;
		CombatManager.instance.player.jumpTrigger = false;
		CombatManager.instance.canReceiveInput = true;
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (CombatManager.instance.inputReceived)
		{
			animator.SetTrigger("Attack");
			CombatManager.instance.InputManager();
			CombatManager.instance.inputReceived = false;
		}
	}

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		CombatManager.instance.player.jumping = true;
	}
}
