// This is written in Code Behind.
url = details.Url;                        
var browser = new WebView
{
    Source = url,                   
};
browser.Reload();
grid.Children.Add(browser);  

//This is written in XAML Page.
<Grid>
    <Grid.RowDefinitions>
        <RowDefinition Height="Auto" />
    </Grid.RowDefinitions>
    <Grid x:Name="grid" BackgroundColor="Red"  Padding="1"  />
</Grid>
