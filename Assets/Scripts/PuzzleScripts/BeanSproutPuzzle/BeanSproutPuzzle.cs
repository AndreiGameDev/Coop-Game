using UnityEngine;
//Implemented by Andrei
public class BeanSproutPuzzle : MonoBehaviour, IInteractable {
    Animator animator;
    public bool canInteract = true;
    [SerializeField] GameObject colliderHolder;
    State CurrentState = State.Withered;
    AudioManager audioManager;
    [SerializeField] AudioClip SFX_Bloom;
    [SerializeField] AudioClip SFX_Wither;
    private void Awake() {
        animator = GetComponentInChildren<Animator>();
    }
    public void Start() {
        audioManager = AudioManager.Instance;
    }
    public void Interact(PlayerRefferenceMaster player, DirFacing? direction = null) {
        if(player.wizzardMagicType == WizardType.Light && canInteract && CurrentState == State.Withered) {
            Grow();
        } else if(player.wizzardMagicType == WizardType.Dark && canInteract && CurrentState == State.Grown) {
            Wither();
        }
    }

    public void Grow() {
        audioManager.PlaySFX(SFX_Bloom);
        animator.SetTrigger("Grow");
        EnableCollider(false);
        CurrentState = State.Grown;
    }

    public void Wither() {
        audioManager.PlaySFX(SFX_Wither);
        animator.SetTrigger("Wither");
        EnableCollider(true);
        CurrentState = State.Withered;
    }

    public void EnableCollider(bool status) {
        colliderHolder.SetActive(status);
    }

    enum State {
        Grown,
        Withered
    }
}
