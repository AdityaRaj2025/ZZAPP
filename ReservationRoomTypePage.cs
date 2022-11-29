public ReservationRoomTypePage(ReservationViewModel model)
{
	InitializeComponent();

	_Model = model;
	BindingContext = _Model;
	
	Dictionary<int, List<KeyValuePair<string, string>>> keyValuePairs = _Model.keyValuePairs;        
 
	_Model.IsProcessing = true;

	//For getting times 
	for (int i = 0; i < _Model.StartTimeNames.Length; i++)
	{
		string times = _Model.StartTimeNames[i];
		timelist.ColumnDefinitions.Add(new ColumnDefinition());
		Label time = new Label() { Text = times, WidthRequest = 80, VerticalTextAlignment = TextAlignment.Center, BackgroundColor = Color.Orange };              
		timelist.Children.Add(time, i, 0);
	}
	status.WidthRequest = timelist.Width;

	//For getting RoomTypes 
	for (int i = 0; i < _Model.RoomTypesSel.Length; i++)
	{
		string roomtypes = _Model.RoomTypesSel[i].Name; 
		
		roomtype.RowDefinitions.Add(new RowDefinition() { Height = 50 });
		Label room = new Label() { Text = roomtypes, WidthRequest = 200, BackgroundColor = Color.Orange, TextColor = Color.Black, VerticalTextAlignment = TextAlignment.Center };             
		roomtype.Children.Add(room, 0, i);
	}

	//For getting RoomTypes Status
	int index = 0;
	foreach (var item in _Model.RoomTypesSel)
	{                
		status.RowDefinitions.Add(new RowDefinition() { Height = 50 });

		List<KeyValuePair<string, string>> temp = keyValuePairs[item.Code];
	   
		for (int i = 0; i < _Model.StartTimeNames.Length; i++)
		{
			string filterStartTimeNames = _Model.StartTimeNames[i];
			string statusFinal = "";
		
			foreach (KeyValuePair<string, string> timeValue in temp)
			{                     
				if (filterStartTimeNames == timeValue.Key)
				{
					statusFinal = timeValue.Value;
					//Console.WriteLine("LoopCheck Nested Locked Value: " + timeValue.Key);
				}
				else
				{
					//Console.WriteLine("LoopCheck Nested Not Matched: " + timeValue.Key);
				}
			}

			if (index < 1) { status.ColumnDefinitions.Add(new ColumnDefinition() { Width = 80 }); }

			statuslabel = new Label() { Text = statusFinal, WidthRequest = 80, VerticalTextAlignment = TextAlignment.Center, HorizontalTextAlignment = TextAlignment.Center, BackgroundColor = Color.White, TextColor = Color.DarkRed };              
			status.Children.Add(statuslabel, i, index);                  
			statuslabel.GestureRecognizers.Add((new TapGestureRecognizer
			{
				Command = new Command(async (obj) =>
				{
					Label temp = obj as Label;
					Grid gridtemp = temp.Parent as Grid;

					int position = gridtemp.Children.IndexOf(temp);                           
					int columnindex =  position % timelist.Children.Count;                           
					int rowindex = (position / timelist.Children.Count);                           
					string timevalue = ((Label)timelist.Children[columnindex]).Text;                          
					roomtypealue = ((Label)roomtype.Children[rowindex]).Text;                            
					int indexOfTimeValue = Array.IndexOf(_Model.StartTimeNames, timevalue);                                                     
					int indexRoomType = Array.FindIndex(_Model.RoomTypesSel, t => t.Name == roomtypealue);
																						
											
					if (statuslabel.Text == "TEL")
					{
						statuslabel.BackgroundColor = Color.GreenYellow;
						await DisplayAlert("Info", "There are no vacancies", "OK");
					}
					else
					{					   												
						_Model.StartTimeIndex = indexOfTimeValue;
						_Model.RoomTypeIndex = indexRoomType;
						await Navigation.PopAsync();                                   
					}			   
				}
				
				}),
				CommandParameter = statuslabel,
			}));
		}
		index++;
	}
} 
