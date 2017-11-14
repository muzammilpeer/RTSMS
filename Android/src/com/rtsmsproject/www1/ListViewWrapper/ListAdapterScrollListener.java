package com.rtsmsproject.www1.ListViewWrapper;

import android.widget.AbsListView;
import android.widget.AbsListView.OnScrollListener;

/*
* @author Binil Thomas P R
*/

public class ListAdapterScrollListener implements OnScrollListener {

	private final AbstactListAdapter adapter;

	public ListAdapterScrollListener(AbstactListAdapter adapter) {
		// TODO Auto-generated constructor stub
		this.adapter = adapter;
	}

	public void onScroll(AbsListView view, int firstVisibleItem,int visibleItemCount, int totalItemCount) {
		adapter.setScrollStatus(true);
	}

	public void onScrollStateChanged(AbsListView view, int scrollState) {
		switch (scrollState) {
		case OnScrollListener.SCROLL_STATE_IDLE:
			adapter.scrollIdle(view);
			break;
		case OnScrollListener.SCROLL_STATE_TOUCH_SCROLL:
			adapter.setScrollStatus(true);
			break;
		case OnScrollListener.SCROLL_STATE_FLING:
			adapter.setScrollStatus(true);
			break;
		}
	}


}
