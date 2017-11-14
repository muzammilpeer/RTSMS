package com.rtsmsproject.www1;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnKeyListener;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.Button;
import android.widget.EditText;

public class Webview extends Activity {
	private class MyWebViewClient extends WebViewClient {
	      @Override
	      public boolean shouldOverrideUrlLoading(WebView view, String url) {
	          view.loadUrl(url);
	          return true;
	      }
	  }
		private WebView webView;
	    private String weburl = "http://www.geo.tv";
	    /** Called when the activity is first created. */
	    @Override
	    public void onCreate(Bundle savedInstanceState) {
	        super.onCreate(savedInstanceState);
	        setContentView(R.layout.webview);

	        Intent nt = getIntent();
	        weburl = nt.getCharSequenceExtra("weburl").toString();
	      
	        // Create reference to UI elements
	        webView  = (WebView) findViewById(R.id.webview_compontent);
	        webView.getSettings().setBuiltInZoomControls(true);
	        
	        WebSettings webSettings = webView.getSettings();
	        webSettings.setCacheMode(WebSettings.LOAD_CACHE_ELSE_NETWORK);
	        // workaround so that the default browser doesn't take over
	        webView.setWebViewClient(new MyWebViewClient());
	        openURL();
	    }
	    
	    /** Opens the URL in a browser */
	    private void openURL() {
	    	webView.loadUrl(weburl);
	    	webView.requestFocus();
	    }    


}
