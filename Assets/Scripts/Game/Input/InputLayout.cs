using System.Collections.Generic;
using UnityEngine;

// TODO: Accept joystick controls
// TODO: Accept negative/positive

[CreateAssetMenu(fileName = "InputLayout", menuName = "Input/Input Layout", order = 1)]
public class InputLayout : ScriptableObject {
  public new string name = "Towerfall";

  [Header("General")]
  public List<KeyCode> start = new List<KeyCode>(new[] { KeyCode.Escape });

  // Gameplay
  [Header("Gameplay")]
  public List<KeyCode> left = new List<KeyCode>(new[] { KeyCode.LeftArrow });
  public List<KeyCode> right = new List<KeyCode>(new[] { KeyCode.RightArrow });
  public List<KeyCode> up = new List<KeyCode>(new[] { KeyCode.UpArrow });
  public List<KeyCode> down = new List<KeyCode>(new[] { KeyCode.DownArrow });

  public List<KeyCode> jump = new List<KeyCode>(new[] { KeyCode.C });
  public List<KeyCode> shoot = new List<KeyCode>(new[] { KeyCode.X, KeyCode.Z });
  public List<KeyCode> item = new List<KeyCode>(new[] { KeyCode.S });
  public List<KeyCode> ability = new List<KeyCode>(new[] { KeyCode.LeftShift, KeyCode.Tab });

  public List<KeyCode> map = new List<KeyCode>(new[] { KeyCode.Return });

  // UI
  // XXX: Just use gameplay buttons? (move -> menu, jump -> confirm, item -> cancel)
  /*
  [Header("UI")]
  public List<KeyCode> menuLeft = new List<KeyCode>(new[] { KeyCode.LeftArrow });
  public List<KeyCode> menuRight = new List<KeyCode>(new[] { KeyCode.RightArrow });
  public List<KeyCode> menuUp = new List<KeyCode>(new[] { KeyCode.UpArrow });
  public List<KeyCode> menuDown = new List<KeyCode>(new[] { KeyCode.DownArrow });

  public List<KeyCode> menuConfirm = new List<KeyCode>(new[] { KeyCode.C });
  public List<KeyCode> menuCancel = new List<KeyCode>(new[] { KeyCode.X, KeyCode.Z, KeyCode.Escape });
  */
}
