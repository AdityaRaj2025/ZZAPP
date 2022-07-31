// This is written in Code Behind.
url = details.Url;                        
var browser = new WebView
{
    Source = url,                   
};
browser.Reload();
grid.Children.Add(browser);  

//This is written in XAML Page.
<Grid VerticalOptions="FillAndExpand"
          HorizontalOptions="FillAndExpand">
    <Grid.RowDefinitions>
        <RowDefinition Height="1*" />
        <RowDefinition Height="1*"/>
    <RowDefinition Height="0.2*"/>
        </Grid.RowDefinitions>
        <Image x:Name="BgImage"
               Aspect="AspectFill"
               Source="{Binding BgImageSource}"/>

        <Grid RowSpacing="0" ColumnSpacing="0">
            <Grid.RowDefinitions>
                <RowDefinition Height="Auto" />
                <RowDefinition Height="*" />
                <RowDefinition Height="Auto" />
               
            </Grid.RowDefinitions>

            <!-- headerー -->
            <views:Header Grid.Row="0" />

            <ContentView Grid.Row="1">
                <ScrollView>
                    <StackLayout Spacing="0">

                        <!-- Title -->
                        <views:TitleBar x:Name="telephone_title_frame"
                                    IsBackButtonVisible="True"
                                    Command="{Binding BackCommand}"
                                    Text="{Binding TelephoneTitle}" />

                        <!-- Phone number -->
                        <Label Grid.Row="2"
                           x:Name="telephone"
                           Style="{StaticResource LabelStyle}"
                           FontSize="{Binding FontSizeXXLarge}">
                            <Label.GestureRecognizers>
                                <TapGestureRecognizer Tapped="TelephoneLabel_Tapped" />
                            </Label.GestureRecognizers>
                        </Label>

                        <!-- Title. -->
                        <views:TitleBar Grid.Row="3"
                                    x:Name="storedetail_title_frame"
                                    Command="{Binding BackCommand}"
                                    Text="{Binding StoreDetailTitle}" />

                        <!-- Store Information -->
                        <StackLayout Grid.Row="4"
                                 Margin="10"
                                 x:Name="storedetail">
                            <Label x:Name="storename"
                               Style="{StaticResource LabelStyle}"/>
                            <Label x:Name="zip"
                               Style="{StaticResource LabelStyle}"/>
                            <Label x:Name="address"
                               Style="{StaticResource LabelStyle}"/>

                            <Grid>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width="1*" />
                                    <ColumnDefinition Width="4*" />
                                    <ColumnDefinition Width="1*" />
                                </Grid.ColumnDefinitions>
                                <views:CustomImageButton Grid.Column="1" IsVisible="{Binding IsMember}" Tapped="FavButton_Tapped">
                                    <views:CustomImageButton.Triggers>
                                        <DataTrigger TargetType="views:CustomImageButton" Binding="{Binding IsFavorite}" Value="True">
                                            <Setter Property="Image" Value="{local:ImageResource SSAPP.res.button_fav2.png}" />
                                        </DataTrigger>
                                        <DataTrigger TargetType="views:CustomImageButton" Binding="{Binding IsFavorite}" Value="False">
                                            <Setter Property="Image" Value="{local:ImageResource SSAPP.res.button_fav1.png}" />
                                        </DataTrigger>
                                    </views:CustomImageButton.Triggers>
                                </views:CustomImageButton>
                            </Grid>

                        </StackLayout>
                        
                    </StackLayout>
                </ScrollView>
            </ContentView>

        </Grid>
        <WebView x:Name="webview" Grid.Row="1" BackgroundColor="DeepPink" Source="https://github.com/AdityaRaj2025/ZZAPP/blob/main/Demo.cs"/>
            <!-- footer -->
            <views:Futter Grid.Row="2" />
        <!-- Loading screen -->
         <views:LoadingIndicator IsVisible="{Binding IsProcessing}" Grid.Row="2" />

    </Grid>
    
             //Update this
     </StackLayout>

                         
                      
                  <WebView Grid.Row="0" x:Name="browser"  HeightRequest="500"/>
        
    
                    </StackLayout>
                </ScrollView>        
                
                      if (url == "")
                {
                    browser.IsVisible = false;
                    //height.Height = 0;
                }
                else
                {
                    
                    browser.Source=url;
                }

             
             
