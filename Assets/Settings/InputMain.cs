//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.5.1
//     from Assets/Settings/InputMain.inputactions
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

public partial class @InputMain: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMain()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMain"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""04dfcaf2-9cc0-4d11-85de-1dd24dcc6b78"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""f5bb2530-7091-4539-bc57-27c251dc32aa"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""PrimaryInteract"",
                    ""type"": ""Value"",
                    ""id"": ""e458daa7-2601-459c-a5a7-db595ffed4a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""SecondaryInteract"",
                    ""type"": ""Value"",
                    ""id"": ""9f23376e-a1a7-48f9-b019-2fee414c3b61"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""FlipSelection"",
                    ""type"": ""Value"",
                    ""id"": ""01cf067c-8b40-404c-89c7-e25008cb950b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pan"",
                    ""type"": ""Value"",
                    ""id"": ""cbf1d26a-88e2-4116-82da-88bf9d6a6305"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""4f97ec2f-d77e-4b65-8f26-ca5f13daefe9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""706fb724-cb61-445f-929f-a4bd56cd9acc"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""37f9997e-7bf8-401c-81e4-94be2474589f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3917afd-33c1-4a88-9152-a8be43abe602"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FlipSelection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9df85544-b080-4840-859a-3661ea967a1b"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5d5c8ce-2aa1-4f4d-9a1a-309f9d7be71d"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""828119a2-498d-4cc3-9bfe-d456e1f4ad7d"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_MousePosition = m_Default.FindAction("MousePosition", throwIfNotFound: true);
        m_Default_PrimaryInteract = m_Default.FindAction("PrimaryInteract", throwIfNotFound: true);
        m_Default_SecondaryInteract = m_Default.FindAction("SecondaryInteract", throwIfNotFound: true);
        m_Default_FlipSelection = m_Default.FindAction("FlipSelection", throwIfNotFound: true);
        m_Default_Pan = m_Default.FindAction("Pan", throwIfNotFound: true);
        m_Default_Zoom = m_Default.FindAction("Zoom", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private List<IDefaultActions> m_DefaultActionsCallbackInterfaces = new List<IDefaultActions>();
    private readonly InputAction m_Default_MousePosition;
    private readonly InputAction m_Default_PrimaryInteract;
    private readonly InputAction m_Default_SecondaryInteract;
    private readonly InputAction m_Default_FlipSelection;
    private readonly InputAction m_Default_Pan;
    private readonly InputAction m_Default_Zoom;
    public struct DefaultActions
    {
        private @InputMain m_Wrapper;
        public DefaultActions(@InputMain wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Default_MousePosition;
        public InputAction @PrimaryInteract => m_Wrapper.m_Default_PrimaryInteract;
        public InputAction @SecondaryInteract => m_Wrapper.m_Default_SecondaryInteract;
        public InputAction @FlipSelection => m_Wrapper.m_Default_FlipSelection;
        public InputAction @Pan => m_Wrapper.m_Default_Pan;
        public InputAction @Zoom => m_Wrapper.m_Default_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void AddCallbacks(IDefaultActions instance)
        {
            if (instance == null || m_Wrapper.m_DefaultActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Add(instance);
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @PrimaryInteract.started += instance.OnPrimaryInteract;
            @PrimaryInteract.performed += instance.OnPrimaryInteract;
            @PrimaryInteract.canceled += instance.OnPrimaryInteract;
            @SecondaryInteract.started += instance.OnSecondaryInteract;
            @SecondaryInteract.performed += instance.OnSecondaryInteract;
            @SecondaryInteract.canceled += instance.OnSecondaryInteract;
            @FlipSelection.started += instance.OnFlipSelection;
            @FlipSelection.performed += instance.OnFlipSelection;
            @FlipSelection.canceled += instance.OnFlipSelection;
            @Pan.started += instance.OnPan;
            @Pan.performed += instance.OnPan;
            @Pan.canceled += instance.OnPan;
            @Zoom.started += instance.OnZoom;
            @Zoom.performed += instance.OnZoom;
            @Zoom.canceled += instance.OnZoom;
        }

        private void UnregisterCallbacks(IDefaultActions instance)
        {
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @PrimaryInteract.started -= instance.OnPrimaryInteract;
            @PrimaryInteract.performed -= instance.OnPrimaryInteract;
            @PrimaryInteract.canceled -= instance.OnPrimaryInteract;
            @SecondaryInteract.started -= instance.OnSecondaryInteract;
            @SecondaryInteract.performed -= instance.OnSecondaryInteract;
            @SecondaryInteract.canceled -= instance.OnSecondaryInteract;
            @FlipSelection.started -= instance.OnFlipSelection;
            @FlipSelection.performed -= instance.OnFlipSelection;
            @FlipSelection.canceled -= instance.OnFlipSelection;
            @Pan.started -= instance.OnPan;
            @Pan.performed -= instance.OnPan;
            @Pan.canceled -= instance.OnPan;
            @Zoom.started -= instance.OnZoom;
            @Zoom.performed -= instance.OnZoom;
            @Zoom.canceled -= instance.OnZoom;
        }

        public void RemoveCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDefaultActions instance)
        {
            foreach (var item in m_Wrapper.m_DefaultActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnPrimaryInteract(InputAction.CallbackContext context);
        void OnSecondaryInteract(InputAction.CallbackContext context);
        void OnFlipSelection(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
}
