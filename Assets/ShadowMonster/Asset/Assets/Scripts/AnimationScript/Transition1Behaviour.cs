using UnityEngine;

public class Transition1Behaviour : StateMachineBehaviour
{
	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
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
}
