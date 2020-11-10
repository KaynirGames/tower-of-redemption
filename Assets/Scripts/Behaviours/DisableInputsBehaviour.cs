using UnityEngine;

public class DisableInputsBehaviour : StateMachineBehaviour
{
    private PlayerCharacter _player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_player == null)
        {
            _player = animator.transform.root.GetComponent<PlayerCharacter>();
        }

        _player.ToggleInput(false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _player.ToggleInput(true);
    }
}
