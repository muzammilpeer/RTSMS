/**
 * 
 */
package com.rtsmsproject.www1.ListViewWrapper;
/*
 * @author Binil Thomas
 * Date:20/may/2010
 */
import android.content.Context;
import android.widget.AbsListView;
import android.widget.BaseAdapter;

/**
 * @author Binil Thomas
 * 
 */
public abstract class AbstactListAdapter extends BaseAdapter {

	protected Context context;
	protected boolean busy;

	/**
	 * 
	 */
	public AbstactListAdapter(Context context) {
		// TODO Auto-generated constructor stub
		this.context = context;
	}

	public abstract void setScrollStatus(boolean scroll);

	public abstract void scrollIdle(AbsListView view);

}
