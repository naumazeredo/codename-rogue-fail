using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActionAnimatorWindow : EditorWindow {
  private class Node {
    public Rect rect;
    public string title;
    public bool isDragged;

    public GUIStyle style;

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle) {
      rect = new Rect(position.x, position.y, width, height);
      style = nodeStyle;
    }

    public void Drag(Vector2 delta) {
      rect.position += delta;
    }

    public void Draw() {
      GUI.Box(rect, title, style);
    }

    public bool ProcessEvents(Event e) {
      switch (e.type) {
        case EventType.MouseDown:
          if (e.button == 0) {
            if (rect.Contains(e.mousePosition)) {
              isDragged = true;
              GUI.changed = true;
            }
          }
        break;

        case EventType.MouseUp:
          isDragged = false;
        break;

        case EventType.MouseDrag:
          if (e.button == 0 && isDragged) {
            Drag(e.delta);
            e.Use();
            return true;
          }
        break;
      }
      return false;
    }
  }

  private List<Node> nodes;
  private GUIStyle nodeStyle;

  [MenuItem("Rogue/Action Animator")]
  private static void Init() {
    ActionAnimatorWindow window = EditorWindow.GetWindow<ActionAnimatorWindow>("Action Animator");
    window.Show();
  }

  private void OnEnable() {
    nodeStyle = new GUIStyle();
    nodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
    nodeStyle.border = new RectOffset(12, 12, 12, 12);

    nodes = new List<Node>();
  }

  private void OnGUI() {
    Draw();
    ProcessEvents(Event.current);
    if (GUI.changed) Repaint();
  }

  private void Draw() {
    DrawNodes();
  }

  private void DrawNodes() {
    if (nodes != null) {
      foreach (var node in nodes)
        node.Draw();
    }
  }

  private void ProcessEvents(Event e) {
    switch (e.type) {
      case EventType.MouseDown:
        if (e.button == 1) {
          ProcessContextMenu(e.mousePosition);
        }
      break;
    }

    ProcessNodeEvents(e);
  }

  private void ProcessNodeEvents(Event e) {
    foreach (var node in nodes) {
      bool guiChanged = node.ProcessEvents(e);
      if (guiChanged)
        GUI.changed = true;
    }
  }

  private void ProcessContextMenu(Vector2 pos) {
    GenericMenu menu = new GenericMenu();
    menu.AddItem(new GUIContent("Add Node"), false, () => OnClickAddNode(pos));
    menu.ShowAsContext();
  }

  private void OnClickAddNode(Vector2 pos) {
    nodes.Add(new Node(pos, 200, 50, nodeStyle));
  }
}
