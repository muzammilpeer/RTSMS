package com.rtsmsproject.www1.ListViewWrapper;

import java.io.IOException;
import java.io.InputStream;
import java.lang.ref.SoftReference;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.HashMap;
import java.util.concurrent.ExecutorService;
import java.util.concurrent.Executors;

import android.graphics.drawable.Drawable;
import android.os.AsyncTask;
import android.widget.BaseAdapter;

public class ImageLoader {
	
	BaseAdapter mAdapter=null;
	private HashMap<Integer, SoftReference<Drawable> >Cache = null;
	private HashMap<Integer, SoftReference<String> >Cache1 = null;
	
	private ExecutorService _exec =null;
	
	public ImageLoader(Adapter arg)
	{
		mAdapter=arg;
		Cache=new HashMap<Integer,  SoftReference<Drawable>>();
		Cache1=new HashMap<Integer,  SoftReference<String>>();
	}
	
	
	
	private class WorkerThread extends AsyncTask<Void, Void, Void> implements Runnable{

		
		String url=null;
		int intex=0;
		public WorkerThread(String url,int pos) {
			this.url=url;
			intex=pos;
		}
		
		@Override
		protected Void doInBackground(Void... params) {
			return null;
		}

		@Override
		protected void onProgressUpdate(Void... values) {
			mAdapter.notifyDataSetChanged();
			super.onProgressUpdate(values);
		}
		public void run() {
			Cache.put(intex,new SoftReference<Drawable>(readDrawableFromNetwork(this.url)));
		//	Cache1.put(intex,new SoftReference<Drawable>(readDrawableFromNetwork(this.url)));
			publishProgress();
			
		}
	}

	public Drawable getDrawble(int pos)
	{
		SoftReference<Drawable> mReference=Cache.get(pos);
		if(mReference!=null)
			return mReference.get();
		else
			return null;
		
	}
	public String getStringable(int pos)
	{
		SoftReference<String> mReference=Cache1.get(pos);
		if(mReference!=null)
			return mReference.get();
		else
			return null;
		
	}
	
	
	/*method to get Image if already or to start new task to download*/
	public void loadImage(int First,int Last) {
		try{
				if(_exec!=null){
					_exec.shutdownNow();_exec=null;
				}
				_exec = Executors.newFixedThreadPool(5);
				
				for(int pos=First;pos<=Last;pos++){
					if(Cache.containsKey(pos)){
						if(Cache.get(pos).get()==null)
							_exec.execute(new  WorkerThread(Dataset.mStrings[pos],pos));
					}else{
						_exec.execute(new  WorkerThread(Dataset.mStrings[pos],pos));
					}
				}
		}catch (Exception e) {
	
		}
				
	}//end of method
	
	
	private static Drawable readDrawableFromNetwork(String url ) {
	   	Drawable drawable=null;
	    	try {
	    		
	    			URL Url = new URL(url);
	    			InputStream is = (InputStream) Url.getContent();
	    			return Drawable.createFromStream(is, "src");
	    		} catch (MalformedURLException e) {
					e.printStackTrace();
				}catch (OutOfMemoryError e) {
		    	   e.printStackTrace();
		    	   drawable= null;
		       }catch (IOException e) {
		            e.printStackTrace();
		            drawable= null;
	        	}
		       catch (Exception e) {
				e.printStackTrace();
				drawable= null;
			}
	    return drawable;
	}//end of method
}
