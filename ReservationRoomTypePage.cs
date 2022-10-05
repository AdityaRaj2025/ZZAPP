 // For editing the time portion of the start date and time
private TimeSpan[] _StartTimeValues { get; set; }
public string[] StartTimeNames { get; set; }
private int _StartTimeIndex = -1;
public int StartTimeIndex
{
	get { return _StartTimeIndex; }
	set
	{
		_StartTimeIndex = value;

		if (value >= 0)
		{
			Item.StartTime = Item.StartTime.Date.Add(_StartTimeValues[value]);
			Item.EndTime = Item.StartTime.AddMinutes(Item.Duration);					  
		}

		FilterCourses();

		OnPropertyChanged(nameof(StartTimeDisp));	
	}
}
public string StartTimeDisp => StartTimeNames != null && StartTimeIndex >= 0 && StartTimeIndex < StartTimeNames.Length ? StartTimeNames[StartTimeIndex] : null;

 // For editing usage time
private TimeSpan[] _DurationValues { get; set; }
public string[] DurationNames { get; set; }
private int _DurationIndex = -1;
public int DurationIndex
{
	get { return _DurationIndex; }
	set
	{
		_DurationIndex = value;

		if (value >= 0)
		{
			Item.Duration = (int)_DurationValues[value].TotalMinutes;
			Item.EndTime = Item.StartTime.AddMinutes(Item.Duration);
		}

		OnPropertyChanged(nameof(DurationDisp));
	}   
}
public string DurationDisp => IsFreeTime ? 
	DurationString(RoomCoursesSel[_RoomCourseIndex].TimeRange.GetEndDateTime(Item.StartTime) - Item.StartTime) :
	DurationNames != null && DurationIndex >= 0 && DurationIndex < DurationNames.Length ? DurationNames[DurationIndex] : null;    
	
	
// For RoomType
public RoomType[] RoomTypesSel { get; set; }
private int _RoomTypeIndex = -1;
public int RoomTypeIndex
{
	get { return _RoomTypeIndex; }
	set
	{
		_RoomTypeIndex = value;

		if (value >= 0)
		{
			Item.RoomType = RoomTypesSel[value].Code;
			Item.Status = RoomTypesSel[value].RStatus;                  
		}
		FilterCourses();

		OnPropertyChanged(nameof(RoomTypeDisp));
	}
}
public string RoomTypeDisp =>
	Config != null && RoomTypeIndex >= 0 && RoomTypeIndex < RoomTypesSel.Length ? RoomTypesSel[RoomTypeIndex].Name : null;	
	

// For RoomCourse
public RoomCourse[] RoomCoursesSel { get; set; }
private int _RoomCourseIndex = -1;
public int RoomCourseIndex
{
	get { return _RoomCourseIndex; }
	set
	{
		_RoomCourseIndex = value;

		Item.RoomCourse = value >= 0 ? RoomCoursesSel[value].Code : 0;
		OnPropertyChanged(nameof(RoomCourseDisp));

		IsFreeTime = Item.RoomCourse > 0 && RoomCoursesSel[value].IsFreeTime;
		FilterDurationValues();

		OnPropertyChanged(nameof(DurationDisp));
	}
}
public string RoomCourseDisp => 
	Config != null && RoomCourseIndex >= 0 && RoomCourseIndex < RoomCoursesSel.Length ? RoomCoursesSel[RoomCourseIndex].Name : null;
public bool IsFreeTime;

	

//Filter Courses
private void FilterCourses()
{
	var current = RoomCoursesSel != null && RoomCourseIndex >= 0 && RoomCourseIndex < RoomCoursesSel.Length ? RoomCoursesSel[RoomCourseIndex] : null;
	var newidx = -1;
	var filtered = new List<RoomCourse>();

	foreach (var rc in Config.RoomCourses)
	{
		// Is the course selectable for the room type being selected?
		if (_RoomTypeIndex < 0 || !RoomTypesSel[_RoomTypeIndex].RoomCourses.Contains(rc.Code))
		{
			continue;
		}

		// Does the start time fall within the valid time?
		if (_StartTimeIndex < 0 || !rc.TimeRange.Contains(_StartTimeValues[_StartTimeIndex]) || rc.TimeRange.EndTime == _StartTimeValues[_StartTimeIndex])
		{
			continue;
		}

		filtered.Add(rc);
		if (rc == current)
		{
			newidx = filtered.Count - 1;
		}
	}

	// // Automatically select if only one course is available for selection
	if (filtered.Count == 1)
	{
		newidx = 0;
	}

	RoomCoursesSel = filtered.ToArray();
	OnPropertyChanged(nameof(RoomCoursesSel));

	RoomCourseIndex = newidx;            
}
