using Plugin.LocalNotifications;

void OnScheduleClick(object sender, EventArgs e)
{
    var notification = new NotificationRequest
    {
        NotificationId = 100,
        Title = "Concert",
        Description = "Your concert is after one hour.",
        ReturningData = "Dummy data", // Returning data when tapped on notification.
        Schedule =
        {
            NotifyTime = ReservDate.AddHours(-1) 
        }
    };
    NotificationCenter.Current.Show(notification);
}
