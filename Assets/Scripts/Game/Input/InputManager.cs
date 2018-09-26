using System.Collections.Generic;
using UnityEngine;

public enum InputKey {
  Up, Down, Left, Right,
  Jump, Shoot, Item, Ability,
  Start, Map
}

public class InputManager : MonoBehaviour {
  public InputLayout[] defaultInputs;

  InputLayout[] inputs = new InputLayout[8];

  void Awake() {
    // TODO: load user inputs (from JSON or cloud save?)
    for (int i = 0; i < inputs.Length; i++)
      inputs[i] = defaultInputs[0];
  }

  List<KeyCode> GetLayoutKeys(int player, InputKey key) {
    string attr = key.ToString();
    attr = attr.Substring(0, 1).ToLower() + attr.Substring(1);
    var keys = typeof(InputLayout).GetField(attr).GetValue(inputs[player]) as List<KeyCode>;
    return keys;
  }

  public bool GetKey(int player, InputKey key) {
    var keys = GetLayoutKeys(player, key);
    foreach (var k in keys) {
      if (Input.GetKey(k))
        return true;
    }

    return false;
  }

  public bool GetKeyDown(int player, InputKey key) {
    var keys = GetLayoutKeys(player, key);
    foreach (var k in keys) {
      if (Input.GetKeyDown(k))
        return true;
    }

    return false;
  }

  public bool GetKeyUp(int player, InputKey key) {
    var keys = GetLayoutKeys(player, key);
    foreach (var k in keys) {
      if (Input.GetKeyUp(k))
        return true;
    }

    return false;
  }

  /*
  // TODO: hold 1s
  // TODO: alternative keys
  public void AssignKey(int player, InputKey key) {
    KeyCode newKey;
    foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
      if (Input.GetKeyDown(kcode)) {
        newKey = kcode;
      }
    }
  }
  */
}
