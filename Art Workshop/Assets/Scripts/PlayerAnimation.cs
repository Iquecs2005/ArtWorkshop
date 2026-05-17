using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController controller;

    [SerializeField] private Animator anim;

    [Header("Floats")]
    [SerializeField] private string speedXVariableName = "speed_x";
    [SerializeField] private string speedYVariableName = "speed_y";
    [SerializeField] private string shootInputXVariableName = "shootInput_x";
    [SerializeField] private string shootInputYVariableName = "shootInput_y";
    [SerializeField] private string moveInputXVariableName = "moveInput_x";
    [SerializeField] private string moveInputYVariableName = "moveInput_y";

    [Header("Triggers")]
    [SerializeField] private string onShootCancelTrigger = "shootCancelTrigger";
    [SerializeField] private string onShootTrigger = "shootTrigger";

    private Rigidbody2D rb;

    private Dictionary<string, bool> hasParameter = new Dictionary<string, bool>();

    private void Start()
    {
        rb = controller.GetRigidbody();

        GenerateHasParameter();
    }

    private void FixedUpdate()
    {
        AnimatorSetParameter(speedXVariableName, rb.velocity.x);
        AnimatorSetParameter(speedYVariableName, rb.velocity.y);
    }

    public void OnMoveInputChange(Vector2 moveInput) 
    {
        AnimatorSetParameter(moveInputXVariableName, moveInput.x);
        AnimatorSetParameter(moveInputYVariableName, moveInput.y);
    }

    public void OnShootInputChange(Vector2 shootInput)
    {
        AnimatorSetParameter(shootInputXVariableName, shootInput.x);
        AnimatorSetParameter(shootInputYVariableName, shootInput.y);
    }

    public void OnShootCanceled()
    {
        AnimatorSetTrigger(onShootCancelTrigger);
    }

    public void OnShoot() 
    {
        AnimatorSetTrigger(onShootTrigger);
    }

    private void GenerateHasParameter() 
    {
        if (anim == null)
            return;

        hasParameter.Add(speedXVariableName, false);
        hasParameter.Add(speedYVariableName, false);
        hasParameter.Add(shootInputXVariableName, false);
        hasParameter.Add(shootInputYVariableName, false);
        hasParameter.Add(moveInputXVariableName, false);
        hasParameter.Add(moveInputYVariableName, false);
        hasParameter.Add(onShootCancelTrigger, false);
        hasParameter.Add(onShootTrigger, false);

        for (int i = 0; i < anim.parameterCount; i++) 
        {
            if (hasParameter.ContainsKey(anim.parameters[i].name))
                hasParameter[anim.parameters[i].name] = true;
        }
    }

    private bool AnimatorHasParameter(string parameter) 
    {
        if (hasParameter.TryGetValue(parameter, out var result)) 
        {
            return result;
        }
        return false;
    }

    private void AnimatorSetParameter(string parameter, float value)
    {
        if (anim == null)
            return;

        if (AnimatorHasParameter(parameter))
            anim.SetFloat(parameter, value);
    }

    private void AnimatorSetTrigger(string parameter)
    {
        if (anim == null)
            return;

        if (AnimatorHasParameter(parameter))
            anim.SetTrigger(parameter);
    }
}
