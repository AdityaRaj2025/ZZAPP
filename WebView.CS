private async void GetResponse_StoreDetail(string StoreCode)
        {
            _Model.IsProcessing = true;
            var details = await StoreDetails.GetStoreDetail(StoreCode);
          
            _Model.IsProcessing = false;

            if (details != null)
            {    
                address.Text = details.Address;
                address.Text = address.Text ?? "";

                url = details.Url;
                                
                var browser = new MyWebView
                {
                    Source = url,
                };
                if (url == "")
                {
                    browser.IsVisible = false;
                    //height.Height = 0; 
                }
                else
                {
                    browser.Source = url;
                    //browser.HeightRequest = 500;
                    webview.Children.Add(browser);
                    browser.Reload();
                   
                }
  }
