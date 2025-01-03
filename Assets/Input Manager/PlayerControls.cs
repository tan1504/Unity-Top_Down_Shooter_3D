//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input Manager/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Character"",
            ""id"": ""62b25d6b-11cd-4e9a-97e7-577cfedfd8d3"",
            ""actions"": [
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""0df729fd-3767-401e-8d41-da8ed523c525"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""b7504b02-01b9-4e28-9c5c-800da16f6c1b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""4b612abc-29d8-4205-8e66-ea553d7279e7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""c0146353-5ea3-45ed-9884-abbff609e10b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip Slot - 1"",
                    ""type"": ""Button"",
                    ""id"": ""f3925e45-3497-46e1-b7eb-98efd5b5c2c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip Slot - 2"",
                    ""type"": ""Button"",
                    ""id"": ""09037091-9869-4f6b-b1b8-681f36d3f634"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip Slot - 3"",
                    ""type"": ""Button"",
                    ""id"": ""8002c764-b205-4075-a792-1a7abfde5b39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip Slot - 4"",
                    ""type"": ""Button"",
                    ""id"": ""0813434c-334c-4f92-8998-25c49fdd7a01"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Equip Slot - 5"",
                    ""type"": ""Button"",
                    ""id"": ""5d89dc24-ffc9-405a-aa99-4d0efca6165d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Drop Current Weapon"",
                    ""type"": ""Button"",
                    ""id"": ""580acd5d-5e44-4335-8f7d-3905cedcb1fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""f08ca2fb-fb5e-462b-a286-0027ea5c610f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Toggle Weapon Mode"",
                    ""type"": ""Button"",
                    ""id"": ""9777e2a2-4230-4c02-8f70-4328a29180b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interaction"",
                    ""type"": ""Button"",
                    ""id"": ""b2953c33-b481-443c-9740-15c07bfe4192"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UI Mission Tooltip Switch"",
                    ""type"": ""Button"",
                    ""id"": ""aa04a5e2-3977-4fbf-8d05-21aad49e0086"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UI Pause"",
                    ""type"": ""Button"",
                    ""id"": ""ffef2d50-0722-42ea-8698-8aff6534e30d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""abb662a1-74bc-4fa7-8efd-ae2ac1a2cec4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""36094810-6e8d-48d8-af65-b3a71dad8d07"",
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
                    ""id"": ""ce535e6d-70b6-4582-b2e0-8013efd8faab"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2e1b7ca0-f382-471a-af8b-126afb8977bb"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b5799636-5dd1-427f-84cb-e6e9c0ad72e0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""44a06ccf-db95-487a-a65c-40a3d560f00f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3577d5db-bb25-4631-b97a-c10e93ed69fd"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f27235e3-151a-469b-b990-f716d0b6b77d"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""47f8cb26-189b-48ff-aa94-316d4a8ccfd4"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Slot - 1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8a9135b-13c3-4dd3-81a7-2f50e9cf1abc"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Slot - 2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9dbede47-f65d-433c-9dfb-1b5cb4a0ed07"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drop Current Weapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49cd2a60-f2bd-4765-a937-e6fb14e606f3"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4d3bc924-97d9-47c7-b573-63ef31a3a93d"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Slot - 3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c85762c2-905a-4f55-82af-50ea317206f8"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Slot - 4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46b15d1d-41ab-4138-aecd-1401b7360ebe"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Equip Slot - 5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5300e5bd-1f2d-4565-ae01-6bd8f9058f10"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Toggle Weapon Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de953988-cc43-4c57-9036-e405613cf39c"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interaction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c34dab80-2741-4d45-a239-3435743e8280"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI Mission Tooltip Switch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8819961-f084-4c19-b88a-9bd1129af906"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""db778e8d-5400-413a-917f-989aa521c903"",
            ""actions"": [
                {
                    ""name"": ""UI Pause"",
                    ""type"": ""Button"",
                    ""id"": ""65d74195-58bd-4560-9c5a-5b03fbd59b88"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9dbd9af2-c509-4bdf-87ef-bf950becf35b"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UI Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Car"",
            ""id"": ""98972f33-106a-446b-967d-3f933a0a574e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""f9154586-d534-4ed9-9989-be8fc074307c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""CarExit"",
                    ""type"": ""Button"",
                    ""id"": ""7a0765c5-d0fe-4b07-908d-27c96a32a4db"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Brake"",
                    ""type"": ""Button"",
                    ""id"": ""841e1940-a081-4fa7-b512-4053fc480ad9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b9c12593-5ab3-40ae-9bf4-1eb1ea3c2736"",
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
                    ""id"": ""2f01e056-24af-4125-8cdc-990457d2b8f3"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f7f13d0e-40a3-4d39-8972-ff09e5c18612"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b78e0432-47cd-4d9d-93b7-398e495e8be5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""0e2bcaba-3ee1-478f-98d2-1818234486b5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f7847173-06dc-4122-9b3c-b08c5ffac726"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CarExit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3469d80f-9d04-4883-a1d4-2891a20142bb"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Brake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Character
        m_Character = asset.FindActionMap("Character", throwIfNotFound: true);
        m_Character_Fire = m_Character.FindAction("Fire", throwIfNotFound: true);
        m_Character_Movement = m_Character.FindAction("Movement", throwIfNotFound: true);
        m_Character_Aim = m_Character.FindAction("Aim", throwIfNotFound: true);
        m_Character_Run = m_Character.FindAction("Run", throwIfNotFound: true);
        m_Character_EquipSlot1 = m_Character.FindAction("Equip Slot - 1", throwIfNotFound: true);
        m_Character_EquipSlot2 = m_Character.FindAction("Equip Slot - 2", throwIfNotFound: true);
        m_Character_EquipSlot3 = m_Character.FindAction("Equip Slot - 3", throwIfNotFound: true);
        m_Character_EquipSlot4 = m_Character.FindAction("Equip Slot - 4", throwIfNotFound: true);
        m_Character_EquipSlot5 = m_Character.FindAction("Equip Slot - 5", throwIfNotFound: true);
        m_Character_DropCurrentWeapon = m_Character.FindAction("Drop Current Weapon", throwIfNotFound: true);
        m_Character_Reload = m_Character.FindAction("Reload", throwIfNotFound: true);
        m_Character_ToggleWeaponMode = m_Character.FindAction("Toggle Weapon Mode", throwIfNotFound: true);
        m_Character_Interaction = m_Character.FindAction("Interaction", throwIfNotFound: true);
        m_Character_UIMissionTooltipSwitch = m_Character.FindAction("UI Mission Tooltip Switch", throwIfNotFound: true);
        m_Character_UIPause = m_Character.FindAction("UI Pause", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_UIPause = m_UI.FindAction("UI Pause", throwIfNotFound: true);
        // Car
        m_Car = asset.FindActionMap("Car", throwIfNotFound: true);
        m_Car_Movement = m_Car.FindAction("Movement", throwIfNotFound: true);
        m_Car_CarExit = m_Car.FindAction("CarExit", throwIfNotFound: true);
        m_Car_Brake = m_Car.FindAction("Brake", throwIfNotFound: true);
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

    // Character
    private readonly InputActionMap m_Character;
    private List<ICharacterActions> m_CharacterActionsCallbackInterfaces = new List<ICharacterActions>();
    private readonly InputAction m_Character_Fire;
    private readonly InputAction m_Character_Movement;
    private readonly InputAction m_Character_Aim;
    private readonly InputAction m_Character_Run;
    private readonly InputAction m_Character_EquipSlot1;
    private readonly InputAction m_Character_EquipSlot2;
    private readonly InputAction m_Character_EquipSlot3;
    private readonly InputAction m_Character_EquipSlot4;
    private readonly InputAction m_Character_EquipSlot5;
    private readonly InputAction m_Character_DropCurrentWeapon;
    private readonly InputAction m_Character_Reload;
    private readonly InputAction m_Character_ToggleWeaponMode;
    private readonly InputAction m_Character_Interaction;
    private readonly InputAction m_Character_UIMissionTooltipSwitch;
    private readonly InputAction m_Character_UIPause;
    public struct CharacterActions
    {
        private @PlayerControls m_Wrapper;
        public CharacterActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Fire => m_Wrapper.m_Character_Fire;
        public InputAction @Movement => m_Wrapper.m_Character_Movement;
        public InputAction @Aim => m_Wrapper.m_Character_Aim;
        public InputAction @Run => m_Wrapper.m_Character_Run;
        public InputAction @EquipSlot1 => m_Wrapper.m_Character_EquipSlot1;
        public InputAction @EquipSlot2 => m_Wrapper.m_Character_EquipSlot2;
        public InputAction @EquipSlot3 => m_Wrapper.m_Character_EquipSlot3;
        public InputAction @EquipSlot4 => m_Wrapper.m_Character_EquipSlot4;
        public InputAction @EquipSlot5 => m_Wrapper.m_Character_EquipSlot5;
        public InputAction @DropCurrentWeapon => m_Wrapper.m_Character_DropCurrentWeapon;
        public InputAction @Reload => m_Wrapper.m_Character_Reload;
        public InputAction @ToggleWeaponMode => m_Wrapper.m_Character_ToggleWeaponMode;
        public InputAction @Interaction => m_Wrapper.m_Character_Interaction;
        public InputAction @UIMissionTooltipSwitch => m_Wrapper.m_Character_UIMissionTooltipSwitch;
        public InputAction @UIPause => m_Wrapper.m_Character_UIPause;
        public InputActionMap Get() { return m_Wrapper.m_Character; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterActions set) { return set.Get(); }
        public void AddCallbacks(ICharacterActions instance)
        {
            if (instance == null || m_Wrapper.m_CharacterActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharacterActionsCallbackInterfaces.Add(instance);
            @Fire.started += instance.OnFire;
            @Fire.performed += instance.OnFire;
            @Fire.canceled += instance.OnFire;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Aim.started += instance.OnAim;
            @Aim.performed += instance.OnAim;
            @Aim.canceled += instance.OnAim;
            @Run.started += instance.OnRun;
            @Run.performed += instance.OnRun;
            @Run.canceled += instance.OnRun;
            @EquipSlot1.started += instance.OnEquipSlot1;
            @EquipSlot1.performed += instance.OnEquipSlot1;
            @EquipSlot1.canceled += instance.OnEquipSlot1;
            @EquipSlot2.started += instance.OnEquipSlot2;
            @EquipSlot2.performed += instance.OnEquipSlot2;
            @EquipSlot2.canceled += instance.OnEquipSlot2;
            @EquipSlot3.started += instance.OnEquipSlot3;
            @EquipSlot3.performed += instance.OnEquipSlot3;
            @EquipSlot3.canceled += instance.OnEquipSlot3;
            @EquipSlot4.started += instance.OnEquipSlot4;
            @EquipSlot4.performed += instance.OnEquipSlot4;
            @EquipSlot4.canceled += instance.OnEquipSlot4;
            @EquipSlot5.started += instance.OnEquipSlot5;
            @EquipSlot5.performed += instance.OnEquipSlot5;
            @EquipSlot5.canceled += instance.OnEquipSlot5;
            @DropCurrentWeapon.started += instance.OnDropCurrentWeapon;
            @DropCurrentWeapon.performed += instance.OnDropCurrentWeapon;
            @DropCurrentWeapon.canceled += instance.OnDropCurrentWeapon;
            @Reload.started += instance.OnReload;
            @Reload.performed += instance.OnReload;
            @Reload.canceled += instance.OnReload;
            @ToggleWeaponMode.started += instance.OnToggleWeaponMode;
            @ToggleWeaponMode.performed += instance.OnToggleWeaponMode;
            @ToggleWeaponMode.canceled += instance.OnToggleWeaponMode;
            @Interaction.started += instance.OnInteraction;
            @Interaction.performed += instance.OnInteraction;
            @Interaction.canceled += instance.OnInteraction;
            @UIMissionTooltipSwitch.started += instance.OnUIMissionTooltipSwitch;
            @UIMissionTooltipSwitch.performed += instance.OnUIMissionTooltipSwitch;
            @UIMissionTooltipSwitch.canceled += instance.OnUIMissionTooltipSwitch;
            @UIPause.started += instance.OnUIPause;
            @UIPause.performed += instance.OnUIPause;
            @UIPause.canceled += instance.OnUIPause;
        }

        private void UnregisterCallbacks(ICharacterActions instance)
        {
            @Fire.started -= instance.OnFire;
            @Fire.performed -= instance.OnFire;
            @Fire.canceled -= instance.OnFire;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Aim.started -= instance.OnAim;
            @Aim.performed -= instance.OnAim;
            @Aim.canceled -= instance.OnAim;
            @Run.started -= instance.OnRun;
            @Run.performed -= instance.OnRun;
            @Run.canceled -= instance.OnRun;
            @EquipSlot1.started -= instance.OnEquipSlot1;
            @EquipSlot1.performed -= instance.OnEquipSlot1;
            @EquipSlot1.canceled -= instance.OnEquipSlot1;
            @EquipSlot2.started -= instance.OnEquipSlot2;
            @EquipSlot2.performed -= instance.OnEquipSlot2;
            @EquipSlot2.canceled -= instance.OnEquipSlot2;
            @EquipSlot3.started -= instance.OnEquipSlot3;
            @EquipSlot3.performed -= instance.OnEquipSlot3;
            @EquipSlot3.canceled -= instance.OnEquipSlot3;
            @EquipSlot4.started -= instance.OnEquipSlot4;
            @EquipSlot4.performed -= instance.OnEquipSlot4;
            @EquipSlot4.canceled -= instance.OnEquipSlot4;
            @EquipSlot5.started -= instance.OnEquipSlot5;
            @EquipSlot5.performed -= instance.OnEquipSlot5;
            @EquipSlot5.canceled -= instance.OnEquipSlot5;
            @DropCurrentWeapon.started -= instance.OnDropCurrentWeapon;
            @DropCurrentWeapon.performed -= instance.OnDropCurrentWeapon;
            @DropCurrentWeapon.canceled -= instance.OnDropCurrentWeapon;
            @Reload.started -= instance.OnReload;
            @Reload.performed -= instance.OnReload;
            @Reload.canceled -= instance.OnReload;
            @ToggleWeaponMode.started -= instance.OnToggleWeaponMode;
            @ToggleWeaponMode.performed -= instance.OnToggleWeaponMode;
            @ToggleWeaponMode.canceled -= instance.OnToggleWeaponMode;
            @Interaction.started -= instance.OnInteraction;
            @Interaction.performed -= instance.OnInteraction;
            @Interaction.canceled -= instance.OnInteraction;
            @UIMissionTooltipSwitch.started -= instance.OnUIMissionTooltipSwitch;
            @UIMissionTooltipSwitch.performed -= instance.OnUIMissionTooltipSwitch;
            @UIMissionTooltipSwitch.canceled -= instance.OnUIMissionTooltipSwitch;
            @UIPause.started -= instance.OnUIPause;
            @UIPause.performed -= instance.OnUIPause;
            @UIPause.canceled -= instance.OnUIPause;
        }

        public void RemoveCallbacks(ICharacterActions instance)
        {
            if (m_Wrapper.m_CharacterActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharacterActions instance)
        {
            foreach (var item in m_Wrapper.m_CharacterActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharacterActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharacterActions @Character => new CharacterActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_UIPause;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @UIPause => m_Wrapper.m_UI_UIPause;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @UIPause.started += instance.OnUIPause;
            @UIPause.performed += instance.OnUIPause;
            @UIPause.canceled += instance.OnUIPause;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @UIPause.started -= instance.OnUIPause;
            @UIPause.performed -= instance.OnUIPause;
            @UIPause.canceled -= instance.OnUIPause;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);

    // Car
    private readonly InputActionMap m_Car;
    private List<ICarActions> m_CarActionsCallbackInterfaces = new List<ICarActions>();
    private readonly InputAction m_Car_Movement;
    private readonly InputAction m_Car_CarExit;
    private readonly InputAction m_Car_Brake;
    public struct CarActions
    {
        private @PlayerControls m_Wrapper;
        public CarActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Car_Movement;
        public InputAction @CarExit => m_Wrapper.m_Car_CarExit;
        public InputAction @Brake => m_Wrapper.m_Car_Brake;
        public InputActionMap Get() { return m_Wrapper.m_Car; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CarActions set) { return set.Get(); }
        public void AddCallbacks(ICarActions instance)
        {
            if (instance == null || m_Wrapper.m_CarActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CarActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @CarExit.started += instance.OnCarExit;
            @CarExit.performed += instance.OnCarExit;
            @CarExit.canceled += instance.OnCarExit;
            @Brake.started += instance.OnBrake;
            @Brake.performed += instance.OnBrake;
            @Brake.canceled += instance.OnBrake;
        }

        private void UnregisterCallbacks(ICarActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @CarExit.started -= instance.OnCarExit;
            @CarExit.performed -= instance.OnCarExit;
            @CarExit.canceled -= instance.OnCarExit;
            @Brake.started -= instance.OnBrake;
            @Brake.performed -= instance.OnBrake;
            @Brake.canceled -= instance.OnBrake;
        }

        public void RemoveCallbacks(ICarActions instance)
        {
            if (m_Wrapper.m_CarActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICarActions instance)
        {
            foreach (var item in m_Wrapper.m_CarActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CarActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CarActions @Car => new CarActions(this);
    public interface ICharacterActions
    {
        void OnFire(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnEquipSlot1(InputAction.CallbackContext context);
        void OnEquipSlot2(InputAction.CallbackContext context);
        void OnEquipSlot3(InputAction.CallbackContext context);
        void OnEquipSlot4(InputAction.CallbackContext context);
        void OnEquipSlot5(InputAction.CallbackContext context);
        void OnDropCurrentWeapon(InputAction.CallbackContext context);
        void OnReload(InputAction.CallbackContext context);
        void OnToggleWeaponMode(InputAction.CallbackContext context);
        void OnInteraction(InputAction.CallbackContext context);
        void OnUIMissionTooltipSwitch(InputAction.CallbackContext context);
        void OnUIPause(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnUIPause(InputAction.CallbackContext context);
    }
    public interface ICarActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCarExit(InputAction.CallbackContext context);
        void OnBrake(InputAction.CallbackContext context);
    }
}
