using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputPresenter : MonoBehaviour
{
    [Header("Input Elements")]
    [SerializeField] private GameObject InputView;
    [SerializeField] private GameObject InteractButton;

    private void Awake()
    {
#if (UNITY_ANDROID)

        InputView.SetActive(true);

#else

        InputView.SetActive(false);

#endif
    }

    public void UpdateInteractButton()
    {
        InteractButton.SetActive(
            InteractionManager.Instance.CurrentAvailableInteract() > 0
            );
    }
}
