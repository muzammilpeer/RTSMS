package com.rtsmsproject.www1.ListViewWrapper;

import com.rtsmsproject.www1.R;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ImageView;
import android.widget.TextView;

public class Adapter extends AbstactListAdapter{
	// layoutname = R.layout.main
	private int layoutname ;

	private LayoutInflater mInflater;
	ImageLoader mLoader=null;
	public Adapter(Context context,int layoutnumber) {
		super(context);
		 // layoutname = R.layout.main
		 layoutname = layoutnumber;
		 
		 mInflater = LayoutInflater.from(context);
		 mLoader=new ImageLoader(this);
		 // Now next task is to take the MenuItems arraylist and send it to Imageloader and call loadimage function to parse the array.
		 mLoader.loadImage(0, 10);
		 
	}
	
	public int getCount() {
		// TODO Auto-generated method stub
		return Dataset.mString.size();
	}

	public Object getItem(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	public long getItemId(int position) {
		// TODO Auto-generated method stub
		return position;
	}

	public View getView(int position, View convertView, ViewGroup parent) {
		 // A ViewHolder keeps references to children views to avoid unnecessary calls
        // to findViewById() on each row.
        ViewHolder holder;
        // When convertView is not null, we can reuse it directly, there is no need
        // to reinflate it. We only inflate a new View when the convertView supplied
        // by ListView is null.
        if (convertView == null) {
            convertView = mInflater.inflate(layoutname, null);
            // Creates a ViewHolder and store references to the two children views
            // we want to bind data to.
            holder = new ViewHolder();
            holder.locationtext = (TextView) convertView.findViewById(R.id.locationtext);
            holder.title = (TextView) convertView.findViewById(R.id.title);
            holder.distancetext = (TextView) convertView.findViewById(R.id.distancetext);
            holder.postedbytext = (TextView) convertView.findViewById(R.id.postedbytext);
            holder.timeagotext = (TextView) convertView.findViewById(R.id.timeagotext);
            holder.description = (TextView) convertView.findViewById(R.id.description);
            holder.icon=(ImageView)convertView.findViewById(R.id.icon);
          
            convertView.setTag(holder);
        } else {
            holder = (ViewHolder) convertView.getTag();
        }
        holder.locationtext.setText("Location");
        holder.title.setText("EventName");
        holder.distancetext.setText("Distance");
        holder.postedbytext.setText("PostedBy");
        holder.timeagotext.setText("TimeAgo");
        holder.description.setText("Description");

        /*

        *
        *
        *
        holder.locationtext.setText(mLoader.getStringableLocationName(position));
        holder.title.setText(mLoader.getStringableEventName(position));
        holder.distancetext.setText(mLoader.getStringableDistance(position));
        holder.postedbytext.setText(mLoader.getStringablePostedBy(position));
        holder.timeagotext.setText(mLoader.getStringableTimeAgo(position));
        holder.description.setText(mLoader.getStringableDescription(position));

        */
        holder.icon.setImageDrawable(mLoader.getDrawble(position));
        return convertView;
	}

	static class ViewHolder {
        TextView locationtext;
        TextView title;
        TextView distancetext;
        TextView postedbytext;
        TextView timeagotext;
        TextView description;
        ImageView icon;
    }
	

	@Override
	public void scrollIdle(AbsListView view) {
		 mLoader.loadImage(view.getFirstVisiblePosition(), view.getLastVisiblePosition());
	}

	@Override
	public void setScrollStatus(boolean scroll) {
	}
}
