using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

namespace JackUtil {

    public static class RewiredHelper {

        public static List<string> GetKeyCodeFromActionDescriptiveName(int playerId, string actionName, bool isShowKeyboard = true) {
            List<string> list = new List<string>();
            Player p = ReInput.players.GetPlayer(playerId);
            var playerMaps = p.controllers.maps.GetAllMaps();
            foreach(ControllerMap map in playerMaps) {
                if (!map.enabled) continue;
                if (map.controllerType == ControllerType.Keyboard) {
                    if (!isShowKeyboard) continue;
                    foreach(ActionElementMap action in map.AllMaps) {
                        if (action.actionDescriptiveName == actionName) {
                            list.Add(action.keyCode.ToNonePrefixString());
                        }
                    }
                } else if (map.controllerType == ControllerType.Joystick) {
                    foreach (ActionElementMap action in map.AllMaps) {
                        if (action.actionDescriptiveName == actionName) {
                            list.Add("Joy " + action.elementIdentifierName.ToJoystickSimpleString());
                        }
                    }
                }
                
            }
            return list;
        }

        public static List<string> GetKeyCodeFromActionDescriptiveNameJustShowController(int playerId, string actionName) {
            List<string> list = new List<string>();
            Player p = ReInput.players.GetPlayer(playerId);
            var playerMaps = p.controllers.maps.GetAllMaps();
            bool isShowKeyboard = true;
            if (p.controllers.joystickCount > 0) {
                isShowKeyboard = false;
            }
            foreach(ControllerMap map in playerMaps) {
                if (!map.enabled) continue;
                if (map.controllerType == ControllerType.Keyboard) {
                    if (!isShowKeyboard) continue;
                    foreach(ActionElementMap action in map.AllMaps) {
                        if (action.actionDescriptiveName == actionName) {
                            list.Add(action.keyCode.ToNonePrefixString());
                        }
                    }
                } else if (map.controllerType == ControllerType.Joystick) {
                    foreach (ActionElementMap action in map.AllMaps) {
                        if (action.actionDescriptiveName == actionName) {
                            list.Add("Joy " + action.elementIdentifierName.ToJoystickSimpleString());
                        }
                    }
                }
                
            }
            return list;
        }

        public static bool HasJoystick(int playerId) {
            Player p = ReInput.players.GetPlayer(playerId);
            return p.controllers.joystickCount > 0;
        }

    }
}