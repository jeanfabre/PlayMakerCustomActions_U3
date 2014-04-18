//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CUListControl {
	
	private static int ControlHint = "listControl".GetHashCode();

	private static CUItemRenderer<object> defaultItemRenderer;
	
	static CUListControl() {
		defaultItemRenderer = new CUDefaultItemRenderer<object>();
	}
	
	public static CUListData SelectionList(CUListData listData, IList<object> items, params GUILayoutOption[] options) {
		return SelectionList(listData, items, defaultItemRenderer, options);
	}	
	
	public static CUListData SelectionList<T>(CUListData listData, IList<T> items, CUItemRenderer<T> itemRenderer, params GUILayoutOption[] options) {
		return SelectionList(listData, items, itemRenderer, null, options);
	}
	
	public static CUListData SelectionList(CUListData listData, IList<object> items, string caption, params GUILayoutOption[] options) {
		return SelectionList(listData, items, defaultItemRenderer, caption, options);
	}

	public static CUListData SelectionList<T>(CUListData listData, IList<T> items, CUItemRenderer<T> itemRenderer, string caption, params GUILayoutOption[] options) {
		int controlId = GUIUtility.GetControlID(ControlHint, FocusType.Keyboard);
		ListControlState state = (ListControlState) GUIUtility.GetStateObject(typeof(ListControlState), controlId);
		
		if (listData == null) {
			listData = new CUListData();
		}
		
		GUILayout.BeginVertical(options);
		if (!String.IsNullOrEmpty(caption)) {
			GUILayout.Label(caption, CUListStyle.DefaultStyle.titleStyle);
		}

		listData.scrollPosition = EditorGUILayout.BeginScrollView(listData.scrollPosition, CUListStyle.DefaultStyle.scrollViewStyle);
		
		int i = 0;
		int contextMenuItem = -1;
		
		EventType currentEvent = Event.current.GetTypeForControl(controlId);
		Vector2 mousePos = Event.current.mousePosition;
		
		Rect elementRect = new Rect();
		
		float contentHeight = 0;
		Vector2[] itemRects = new Vector2[items.Count];
		
		// reset drag and drop state 
		if (currentEvent == EventType.DragUpdated || currentEvent == EventType.DragPerform || currentEvent == EventType.DragExited) {
			state.isDropTarget = false;
		}
		
		// ignore mouse up event
		if (currentEvent == EventType.MouseDown || currentEvent == EventType.DragUpdated) {
			state.delayedClick = false;
		}
			
		foreach(T item in items) {
            elementRect = GUILayoutUtility.GetRect(50f, itemRenderer.MeasureHeight(item));
			contentHeight += elementRect.height;
			itemRects[i] = new Vector2(elementRect.y, elementRect.height);
			
			// avoid 
            elementRect.x++;
			elementRect.width--;
			bool hover = elementRect.Contains(mousePos);
			// local event handling
			switch(currentEvent) {
				case EventType.ContextClick:
					if (!hover || GUIUtility.hotControl != 0 || !listData.IsContextMenuSupported) {
						break;
					}
				
					listData.ClearSelection();
					listData[i] = true;
					contextMenuItem = i;
					break;
				
				case EventType.MouseDown:
					// ignore context menu click
					if (!hover || GUIUtility.hotControl != 0 || Event.current.button != 0) {
						break;
					}
				
					GUIUtility.keyboardControl = controlId;

					if (EditorGUI.actionKey) {
						// toggle selection for the current item if it is currently not selected
						if (!listData[i]) {
							listData[i] = true;
						} else {
							// delay deselect until mouse up
							state.delayedClick = true;
						}
					} else if (Event.current.shift) {
						if (listData.Empty) {
							// in case nothing is selected, just select the current item
							listData[i] = true;
						} else {
							int index = listData.Pivot;
							listData.ClearSelection();
							
							int direction = index > i ? -1 : 1;
							while(index != i + direction) {
								listData[index] = true;
								index += direction;
							}
						}
					} else {
						// just a simple click, change selection to the current item
						if (!listData[i]) {
							listData.ClearSelection();
							listData[i] = true;
						} else {
							// delay deselect until mouse up
							state.delayedClick = true;
						}						
					}
					
					state.mouseDownPosition = mousePos;
					GUIUtility.hotControl = controlId;			
	                Event.current.Use();
					break;
				
				case EventType.MouseUp:
					if (GUIUtility.hotControl != controlId) {
						break;
					}
				
					if (hover) {
						// now handly any delayed selection if necessary
						if (state.delayedClick) {
							if (EditorGUI.actionKey) {
								listData[i] = false;
							} else if (!Event.current.shift) {
								listData.ClearSelection();
								listData[i] = true;							
							}
							state.delayedClick = false;
							Event.current.Use();
						}
					
						if (Event.current.clickCount == 2 && listData.IsExecutionSupported) {
							listData.ClearSelection();
							listData[i] = true;
							if (listData.ExecutionListener.HandleExecution(listData.Selection)) {
								Event.current.Use();
							}						
						}
					}
					break;
				
				case EventType.MouseDrag:
					if (GUIUtility.hotControl != controlId) {
						break;
					}
									
					if (hover && listData.IsDragSupported) {
						if ((state.mouseDownPosition - mousePos).magnitude < 3) {
							// wait until the mouse was dragged a certain distance before starting the drag operation
							break;
						}
						// mouse down event selects an item, so mouse is over an selected item. 
						// but its not important if the current item is an selected one as long as we are over an item						
						if (listData.DragSource.CanDrag(listData.Selection)) {
							listData.DragSource.InitializeDrag(listData.Selection);
							GUIUtility.hotControl = 0;
							Event.current.Use();
						}
					}
					break;	
				
				case EventType.Repaint:	
					itemRenderer.Arrange(item, i, listData[i], GUIUtility.keyboardControl == controlId, elementRect);
					if (DragAndDrop.activeControlID == controlId && state.isDropTarget && state.itemIndex == i) {
						if (state.dropType == CUDropType.AtPosition) {
							GUI.Box(elementRect, "", CUListStyle.DefaultStyle.dropBeforeHighlight);	
						} else {
							GUI.Box(elementRect, "", CUListStyle.DefaultStyle.dropIntoHighlight);
						}
					}
					break;
				
				case EventType.DragUpdated:
				case EventType.DragPerform:
					if (hover && listData.IsDropSupported) {
						bool canDropInto = listData.DropTarget.CanDrop(i, CUDropType.IntoItem);
						// check if this is a drop between
						if (IsDropBefore(elementRect, mousePos, canDropInto) && listData.DropTarget.CanDrop(i, CUDropType.AtPosition)) {
							state.isDropTarget = true;
							state.itemIndex = i;
							state.dropType = CUDropType.AtPosition;
							if (currentEvent == EventType.DragPerform) {
								listData.DropTarget.AcceptDrop(i, CUDropType.AtPosition);
							}
						}
					
						// now check if this is a drop after
						if (!state.isDropTarget && IsDropAfter(elementRect, mousePos, canDropInto) && listData.DropTarget.CanDrop(i + 1, CUDropType.AtPosition)) {
							state.isDropTarget = true;
							state.itemIndex = i + 1;
							state.dropType = CUDropType.AtPosition;
							if (currentEvent == EventType.DragPerform) {
								listData.DropTarget.AcceptDrop(i + 1, CUDropType.AtPosition);
							}						
						}
					
						// well its a drop into
						if (!state.isDropTarget && canDropInto) {
							state.isDropTarget = true;
							state.itemIndex = i;
							state.dropType = CUDropType.IntoItem;
							if (currentEvent == EventType.DragPerform) {
								listData.DropTarget.AcceptDrop(i, CUDropType.IntoItem);
							}						
						}
					
						if (state.isDropTarget) {
							DragAndDrop.activeControlID = controlId;
							Event.current.Use();
						}
					}
					break;				
			}
			i++;
        }
		
		EditorGUILayout.EndScrollView();
		Rect scrollViewRect = GUILayoutUtility.GetLastRect();
		
		int scrollToItem = -1;
		
		// now check if we have to listen for drop events inside the empty part of the list
		switch(currentEvent) {
			case EventType.ContextClick:
				if (GUIUtility.hotControl != 0 || !scrollViewRect.Contains(Event.current.mousePosition) || !listData.IsContextMenuSupported) {
					break;
				}
					
				state.contextMenuItemIndex = contextMenuItem;
				Event.current.Use();	
			break;
			
			case EventType.Repaint:
				// drop accepted but not by an item
				if (DragAndDrop.activeControlID == controlId && state.isDropTarget) {
					if (state.dropType == CUDropType.AtPosition && state.itemIndex == items.Count) {
						if (items.Count == 0) {
							// no items, use the scrollviewrect
							GUI.Box(scrollViewRect, "", CUListStyle.DefaultStyle.dropBeforeHighlight);							
						} else {
							// add group for clipping otherwise higlight is sometimes visible outside the control
							// beside that we dont have to convert the relative values of the element rect
							GUI.BeginGroup(scrollViewRect);
							GUI.Box(elementRect, "", CUListStyle.DefaultStyle.dropAfterHighlight);	
							GUI.EndGroup();
						}
					} else if (state.dropType == CUDropType.IntoContainer) {
						GUI.Box(scrollViewRect, "", CUListStyle.DefaultStyle.dropIntoHighlight);		
					}	
				}
				if (state.contextMenuItemIndex != -1) {
					listData.ContextMenuHandler.HandleContextMenu(state.contextMenuItemIndex);
					state.contextMenuItemIndex = -1;
				}
			
				break;
			
			case EventType.MouseUp:
				if (GUIUtility.hotControl != controlId) {
					break;
				}
				// release lock if required
				GUIUtility.hotControl = 0;
				Event.current.Use();
				break;
			
			case EventType.DragUpdated:
			case EventType.DragPerform:
				if (listData.IsDropSupported && !state.isDropTarget) {
					if (listData.DropTarget.CanDrop(items.Count, CUDropType.AtPosition)) {
						state.isDropTarget = true;
						state.itemIndex = items.Count;
						state.dropType = CUDropType.AtPosition;
						if (currentEvent == EventType.DragPerform) {
							listData.DropTarget.AcceptDrop(items.Count, CUDropType.AtPosition);
						}						
					} else if (listData.DropTarget.CanDrop(-1, CUDropType.IntoContainer)) {
						state.isDropTarget = true;
						state.itemIndex = -1;
						state.dropType = CUDropType.IntoContainer;
						if (currentEvent == EventType.DragPerform) {
							listData.DropTarget.AcceptDrop(-1, CUDropType.IntoContainer);
						}						
					}

					if (state.isDropTarget) {
						DragAndDrop.activeControlID = controlId;
						Event.current.Use();
					}
				}
				break;
			
			case EventType.KeyDown:
				if (GUIUtility.keyboardControl == controlId) {
					if (Event.current.keyCode != KeyCode.None) {
						if (Event.current.keyCode == KeyCode.UpArrow) {
							// move selection up
							int newSelection = Mathf.Max(listData.Pivot - 1, 0);
							listData.ClearSelection();
							listData[newSelection] = true;
							scrollToItem = newSelection;
							Event.current.Use();
						} else if (Event.current.keyCode == KeyCode.DownArrow) {
							// move selection down
							int newSelection = Mathf.Min(listData.Pivot + 1, items.Count - 1);
							listData.ClearSelection();
							listData[newSelection] = true;
							scrollToItem = newSelection;
							Event.current.Use();
						} else {
							bool handled = false;
							if ((Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter) && listData.IsExecutionSupported) {
								if (listData.ExecutionListener.HandleExecution(listData.Selection)) {
									handled = true;
									Event.current.Use();
								}
							}
						
							// only keycode ist available
							if (!handled && listData.IsKeyInputSupported) {
								if (listData.KeyListener.HandleKeyEvent(Event.current.keyCode, Event.current.character, Event.current.modifiers)) {
									Event.current.Use();
								}
							}					
						}
					} else {
						// only character is available
						if (listData.IsKeyInputSupported) {
							if (listData.KeyListener.HandleKeyEvent(Event.current.keyCode, Event.current.character, Event.current.modifiers)) {
								Event.current.Use();
							}
						}					
					}
				}
				break;
		}
		
		if (scrollToItem != -1) {
			// calculate the new offset (GUI.ScrollTo doesn't seem to work)
			// we have the position and height of the item we want to show, the height of the scrollview content, 
			// the current scroll position and the height of the scrollview itself 
			
			Rect viewport = new Rect(0, listData.scrollPosition.y, 100, scrollViewRect.height); // real width is not important
			Rect itemRect = new Rect(10, itemRects[scrollToItem].x, 40, itemRects[scrollToItem].y);
			
			if (viewport.yMin > itemRect.yMin || viewport.yMax < itemRect.yMax) {
				// we have to change the scroll position
				if (viewport.yMin > itemRect.yMin) {
					// show item at top edge
					listData.scrollPosition.y = itemRect.yMin;	
				} else {
					listData.scrollPosition.y = itemRect.yMax - viewport.height;
				}			
			}
		}			

		GUILayout.EndVertical();
        return listData;	
	}	
						
						
	protected static bool IsDropBefore(Rect bounds, Vector2 mouse, bool canDropInto) {
		int distance = canDropInto ? 5 : (int)Math.Ceiling(bounds.height / 2);
		return bounds.yMin + distance >= mouse.y;
	}
	
	protected static bool IsDropAfter(Rect bounds, Vector2 mouse, bool canDropInto) {
		int distance = canDropInto ? 5 : (int)Math.Ceiling(bounds.height / 2);
		return bounds.yMax - distance <= mouse.y;
	}
	
	internal sealed class ListControlState {
		public Vector2 mouseDownPosition;
		public bool delayedClick;
		public bool isDropTarget;
		public int itemIndex;
		public CUDropType dropType;
		public int contextMenuItemIndex = -1;
	}

}
