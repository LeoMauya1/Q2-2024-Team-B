//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Leo/PLAYERCONTROLLER.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PLAYERCONTROLLER : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PLAYERCONTROLLER()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PLAYERCONTROLLER"",
    ""maps"": [
        {
            ""name"": ""GamePlay1"",
            ""id"": ""79eccb21-07e4-43ea-aab6-0b9819ebb66f"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""bd9fcd9d-686b-45f7-b390-bbf8bad52479"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0faa8e09-fced-4858-98d3-151ea63f4d7e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""4ed2174c-0c37-48b5-bda2-6197a71ea367"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""409bea37-5d8b-4e7c-89f4-98898e623959"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""f6e4c48f-8642-4b25-89e2-a155de14c705"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""FlashLight"",
                    ""type"": ""Button"",
                    ""id"": ""c7444b7d-24ae-48e9-b367-8ab534e66ce4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""shootSpear"",
                    ""type"": ""Button"",
                    ""id"": ""9e5f7f4e-15e1-43cf-826f-4b662b2b6598"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""29791fd2-7e9c-450d-9e05-f13e8e4b71a9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""861ef3bd-cd4f-4df9-918f-30b095b81639"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1ac730d7-6ad8-46ea-a0b3-06ac1d379524"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c17cf0c8-a9c3-43fd-9bd8-32b04c645752"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b6d6c9af-bc74-421d-8560-27c26164fa33"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3c44aa6e-3b64-468b-a4f9-66a8fea53a7e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f99efd3d-a25b-4a93-b060-11870392022e"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bf9527c-a851-4a4d-8ef7-96ea8421b297"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""17474267-f254-46a6-911f-51ad63150496"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""GamePad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eec2a8f-244e-45ff-a77e-dd554c7e43ba"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c4d7528-0b60-4fa3-84ce-68a8664d0fc4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cab659f-fcd4-461a-ab15-c561bf807051"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f1f7c0d7-2606-4188-aef1-4a489e726afc"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""15fa7ba3-78e1-4f28-b03b-1b8b927de2ac"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlashLight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9a05f14a-6d0d-4204-879d-03a8d16925d0"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""shootSpear"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""GamePad"",
            ""bindingGroup"": ""GamePad"",
            ""devices"": []
        }
    ]
}");
        // GamePlay1
        m_GamePlay1 = asset.FindActionMap("GamePlay1", throwIfNotFound: true);
        m_GamePlay1_Movement = m_GamePlay1.FindAction("Movement", throwIfNotFound: true);
        m_GamePlay1_Aim = m_GamePlay1.FindAction("Aim", throwIfNotFound: true);
        m_GamePlay1_Crouch = m_GamePlay1.FindAction("Crouch", throwIfNotFound: true);
        m_GamePlay1_Jump = m_GamePlay1.FindAction("Jump", throwIfNotFound: true);
        m_GamePlay1_Sprint = m_GamePlay1.FindAction("Sprint", throwIfNotFound: true);
        m_GamePlay1_FlashLight = m_GamePlay1.FindAction("FlashLight", throwIfNotFound: true);
        m_GamePlay1_shootSpear = m_GamePlay1.FindAction("shootSpear", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // GamePlay1
    private readonly InputActionMap m_GamePlay1;
    private IGamePlay1Actions m_GamePlay1ActionsCallbackInterface;
    private readonly InputAction m_GamePlay1_Movement;
    private readonly InputAction m_GamePlay1_Aim;
    private readonly InputAction m_GamePlay1_Crouch;
    private readonly InputAction m_GamePlay1_Jump;
    private readonly InputAction m_GamePlay1_Sprint;
    private readonly InputAction m_GamePlay1_FlashLight;
    private readonly InputAction m_GamePlay1_shootSpear;
    public struct GamePlay1Actions
    {
        private @PLAYERCONTROLLER m_Wrapper;
        public GamePlay1Actions(@PLAYERCONTROLLER wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_GamePlay1_Movement;
        public InputAction @Aim => m_Wrapper.m_GamePlay1_Aim;
        public InputAction @Crouch => m_Wrapper.m_GamePlay1_Crouch;
        public InputAction @Jump => m_Wrapper.m_GamePlay1_Jump;
        public InputAction @Sprint => m_Wrapper.m_GamePlay1_Sprint;
        public InputAction @FlashLight => m_Wrapper.m_GamePlay1_FlashLight;
        public InputAction @shootSpear => m_Wrapper.m_GamePlay1_shootSpear;
        public InputActionMap Get() { return m_Wrapper.m_GamePlay1; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GamePlay1Actions set) { return set.Get(); }
        public void SetCallbacks(IGamePlay1Actions instance)
        {
            if (m_Wrapper.m_GamePlay1ActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnMovement;
                @Aim.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnAim;
                @Crouch.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnCrouch;
                @Jump.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnSprint;
                @FlashLight.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnFlashLight;
                @FlashLight.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnFlashLight;
                @FlashLight.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnFlashLight;
                @shootSpear.started -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnShootSpear;
                @shootSpear.performed -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnShootSpear;
                @shootSpear.canceled -= m_Wrapper.m_GamePlay1ActionsCallbackInterface.OnShootSpear;
            }
            m_Wrapper.m_GamePlay1ActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @FlashLight.started += instance.OnFlashLight;
                @FlashLight.performed += instance.OnFlashLight;
                @FlashLight.canceled += instance.OnFlashLight;
                @shootSpear.started += instance.OnShootSpear;
                @shootSpear.performed += instance.OnShootSpear;
                @shootSpear.canceled += instance.OnShootSpear;
            }
        }
    }
    public GamePlay1Actions @GamePlay1 => new GamePlay1Actions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_GamePadSchemeIndex = -1;
    public InputControlScheme GamePadScheme
    {
        get
        {
            if (m_GamePadSchemeIndex == -1) m_GamePadSchemeIndex = asset.FindControlSchemeIndex("GamePad");
            return asset.controlSchemes[m_GamePadSchemeIndex];
        }
    }
    public interface IGamePlay1Actions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnFlashLight(InputAction.CallbackContext context);
        void OnShootSpear(InputAction.CallbackContext context);
    }
}
