using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSP.UI;
using KSP.UI.Screens;
using UnityEngine;

namespace LieInMustEnsue
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, false)]
    public class ToolbarButton : MonoBehaviour
    {
        // button textures
        public Texture limeOn;
        public Texture limeOff;
        public Texture limeHover;

        // the toolbar button
        public ApplicationLauncherButton limeBtn;

        // is button pressed?
        public bool btnIsPressed = false;

        // does the button exist?
        public bool btnIsPresent = false;

        // menu selection id
        public int selGridInt = 1;

        // menu options
        public string[] selString = new string[] { "Sunrise (Stock)", "Sunny", "Sunset", "Midnight" };
        
        // menu position reference, set for middle of the screen
        private Vector2 menuPR = new Vector2((Screen.width / 2) - 100, (Screen.height / 2) - 93);

        // menu size reference
        private Vector2 menuSR = new Vector2(200, 186);

        // the menu position holder
        private static Rect menuPos;

        

        public void Awake()
        {
            // register game events

            GameEvents.onGUIApplicationLauncherReady.Add(AddButton);
            GameEvents.onGUIApplicationLauncherUnreadifying.Add(RemoveButton);       
        }

        private void RemoveButton(GameScenes gameScenes)
        {
            // remove the button

            if (limeBtn.enabled)
            {
                ApplicationLauncher.Instance.RemoveModApplication(limeBtn);
                btnIsPressed = false;
            }
        }
        private void AddButton()
        {
            // add the button

            if (!btnIsPresent)
            {
                limeBtn = ApplicationLauncher.Instance.AddModApplication(onTrue, onFalse, onHover, onHoverOut, onEnable, onDisable,
                    ApplicationLauncher.AppScenes.SPACECENTER, limeOff);

                btnIsPresent = true;
            }
        }

        private void ItsLimeTime()
        {
            // instantiate the menu

            menuPos = GUILayout.Window(123456, menuPos, MenuWindow,
                "LIME Time Options", new GUIStyle(HighLogic.Skin.window));

        }

      

        private void MenuWindow(int windowID)
        {
            // menu defs
         
            GUILayout.BeginVertical();
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();

            selGridInt = GUI.SelectionGrid(new Rect(20, 50, 200, 186), selGridInt, selString, 1, new GUIStyle(HighLogic.Skin.toggle));
            
            GUILayout.EndHorizontal();
            GUILayout.Space(100);
            GUILayout.EndVertical();
            
            GUI.DragWindow();

        }


        public void Start()
        {
            // get the icons from file, preload menu position

            limeOn = GameDatabase.Instance.GetTexture("FruitKocktail/LIME/Icons/limeon", false);
            limeOff = GameDatabase.Instance.GetTexture("FruitKocktail/LIME/Icons/limeoff", false);
            limeHover = GameDatabase.Instance.GetTexture("FruitKocktail/LIME/Icons/limehover", false);
            menuPos = new Rect(menuPR, menuSR);
            
        }

        public void Update()
        {
            // handles change of mode by player

            if (btnIsPresent)
            {
                LIME.newMode = selGridInt;
            }
        }

        public void OnGUI()
        {
            // handles GUI event (ie button clicked)

            if (btnIsPressed)
            {
                ItsLimeTime();
            }
        }

        // button callbacks

        public void onTrue()
        {
            // ie when clicked on
            limeBtn.SetTexture(limeOn);
            btnIsPressed = true;      

        }

        public void onFalse()
        {
            // ie when clicked off
            limeBtn.SetTexture(limeOff);
            btnIsPressed = false;
        }

        public void onHover()
        {
            // ie on hover when not currently on

            if (!btnIsPressed)
            {
                limeBtn.SetTexture(limeHover);
            }
        }

        public void onHoverOut()
        {
            // ie when leave button when not currently on

            if (!btnIsPressed)
            {
                limeBtn.SetTexture(limeOff);
            }
        }

        public void onEnable()
        {      
            // ie when button first enabled
        }

        public void onDisable()
        {
            // ie when button is disabled

            GameEvents.onGUIApplicationLauncherReady.Remove(AddButton);
            GameEvents.onGUIApplicationLauncherUnreadifying.Remove(RemoveButton);
            OnDestroy();
        }


        public void OnDestroy()
        {
            // die button!

            ApplicationLauncher.Instance.RemoveModApplication(limeBtn);
        }

      



    }
}
