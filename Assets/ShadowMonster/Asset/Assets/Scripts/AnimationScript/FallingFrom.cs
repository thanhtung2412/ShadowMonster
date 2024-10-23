using UnityEngine;

public class FallingFrom : StateMachineBehaviour
{
   
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetBool("jump", value: true);
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
