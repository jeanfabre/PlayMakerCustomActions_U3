//
// Copyright (c) 2013 Ancient Light Studios 
// All Rights Reserved 
//  
// http://www.ancientlightstudios.com
//

using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class CUListData {

	public Vector2 scrollPosition = Vector2.zero;
	
	private HashSet<int> selection = new HashSet<int>();
	private List<int> selectionList = new List<int>(0);
	private bool changed = false;
	private bool multiSelection;
	private int pivot = -1;
	private CUListDragSource dragSource;
	private CUListDropTarget dropTarget;
	private CUListContextMenuListener contextMenuHandler;
	private CUListKeyListener keyListener;
	private CUListExecutionListener executionListener;
	
	public CUListData() : this(false) {
	}
	
	public CUListData(bool multiSelection) {
		this.multiSelection = multiSelection;
	}
	
	public bool this[int index] {
		get {
			return selection.Contains(index);
		}
		set {
			changed = true;
			if (value) {
				if (!multiSelection) {
					ClearSelection();
				}
				if (Empty) {
					pivot = index;
				}
				selection.Add(index);
			} else {
				selection.Remove(index);
				if (pivot == index) {
					pivot = First;
				}
				if (Empty) {
					pivot = -1;
				}
			}
		}
	}
	
	public bool IsDragSupported {
		get { return dragSource != null; }
	}
	
	public CUListDragSource DragSource {
		get { return dragSource; }
		set { dragSource = value; }
	}
	
	public bool IsDropSupported {
		get { return dropTarget != null; }
	}
	
	public CUListDropTarget DropTarget {
		get { return dropTarget; }
		set { dropTarget = value; }
	}
	
	public bool IsContextMenuSupported {
		get { return contextMenuHandler != null; }
	}
	
	public CUListContextMenuListener ContextMenuHandler {
		get { return contextMenuHandler; }
		set { contextMenuHandler = value; }
	}
	
	public bool IsKeyInputSupported {
		get { return keyListener != null; }
	}
	
	public CUListKeyListener KeyListener {
		get { return keyListener; }
		set { keyListener = value; }
	}
	
	public bool IsExecutionSupported {
		get { return executionListener != null; }
	}
	
	public CUListExecutionListener ExecutionListener {
		get { return executionListener; }
		set { executionListener = value; }
	}
	
	public bool MultiSelection {
		get { return multiSelection; }
	}
	
	public int Pivot {
		get { return pivot; }	
	}
	
	public bool Empty {
		get { return selection.Count == 0; }
	}
	
	public int First {
		get { return Empty ? -1 : Selection[0]; }
	}
	
	public int Last {
		get { return Empty ? -1 : Selection[Selection.Count - 1]; }
	}
	
	public List<int> Selection {
		get {
			if (changed) {
				selectionList = selection.ToList();
				selectionList.Sort();
				changed = false;
			}
			return selectionList;
		}
	}
	
	public IList<T> GetSelectedItems<T>(IList<T> items) {
		IList<T> result = new List<T>();
		if (Empty) {
			return result; 
		}
		
		foreach(int index in Selection) {
			result.Add(items[index]);
		}
		
		return result;
	}
	
	public void SetSelectedItems<T>(IList<T> items, IList<T> itemsToSelect) {
		ClearSelection();
		foreach(T item in itemsToSelect) {
			this[items.IndexOf(item)] = true;
		}
	}
	
	public void SetSelectedItem<T>(IList<T> items, T itemToSelect) {
		ClearSelection();
		this[items.IndexOf(itemToSelect)] = true;
	}
	
	public void ClearSelection() {
		changed = true;
		pivot = -1;
		selection.Clear();
	}

}

