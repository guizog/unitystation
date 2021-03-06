﻿using UnityEngine;
using System.Collections;
using PlayGroup;

namespace UI {
    public class UIManager: MonoBehaviour {

        public ControlChat chatControl;
        public Hands hands;
        public ControlIntent intentControl;
        public ControlAction actionControl;
        public ControlWalkRun walkRunControl;
        public ControlDisplays displayControl;

        private static UIManager uiManager;

        public static UIManager Instance {
            get {
                if(!uiManager) {
                    uiManager = FindObjectOfType<UIManager>();
                }

                return uiManager;
            }
        }

        public static ControlChat Chat {
            get {
                return Instance.chatControl;
            }
        }

        public static Hands Hands {
            get {
                return Instance.hands;
            }
        }

        public static ControlIntent Intent {
            get {
                return Instance.intentControl;
            }
        }

        public static ControlAction Action {
            get {
                return Instance.actionControl;
            }
        }

        public static ControlWalkRun WalkRun {
            get {
                return Instance.walkRunControl;
            }
        }

        public static ControlDisplays Display {
            get {
                return Instance.displayControl;
            }
        }


        /// <summary>
        /// Current Intent status
        /// </summary>
        public static Intent CurrentIntent { get; set; }

        /// <summary>
        /// What is DamageZoneSeclector currently set at
        /// </summary>
        public static DamageZoneSelector DamageZone { get; set; }

        /// <summary>
        /// Is throw selected?
        /// </summary>
        public static bool IsThrow { get; set; }

        /// <summary>
        /// Is Oxygen On?
        /// </summary>
        public static bool IsOxygen { get; set; }
    }
}