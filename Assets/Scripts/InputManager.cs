using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    [SerializeField] private Keybindings keybindings;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;   
        }// end of if
        else if (Instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }// end of awkae

    public KeyCode GetKeyForAction(KeybindsActions keybindsAction)
    {
        // Find Keycode
        /*
         * A foreach that check 
         */
        foreach(Keybindings.KeybindingsCheck keybindingsCheck in keybindings.keybindingsChecks)
        {
            if(keybindingsCheck.keybindsAction == keybindsAction)
            {
                return keybindingsCheck.keyCode;
            }
        }
        return KeyCode.None;
    }// end of keycode

    public bool GetKeyDown(KeybindsActions key)
    {
        // check for key down
        foreach (Keybindings.KeybindingsCheck keybindingsCheck in keybindings.keybindingsChecks)
        {
            if (keybindingsCheck.keybindsAction == key)
            {
                return Input.GetKeyDown(keybindingsCheck.keyCode);
            }
        }
        return false;
    }// end of getkeydown
    public bool GetKey(KeybindsActions key)
    {
        foreach (Keybindings.KeybindingsCheck keybindingsCheck in keybindings.keybindingsChecks)
        {
            if (keybindingsCheck.keybindsAction == key)
            {
                return Input.GetKey(keybindingsCheck.keyCode);
            }
        }
        return false;
       ;
    }// end of getkey
    public bool GetKeyUp(KeybindsActions key)
    {
        foreach (Keybindings.KeybindingsCheck keybindingsCheck in keybindings.keybindingsChecks)
        {
            if (keybindingsCheck.keybindsAction == key)
            {
                return Input.GetKeyUp(keybindingsCheck.keyCode);
            }
        }
        return false;
    }// end of getkeup

}// end of Class
