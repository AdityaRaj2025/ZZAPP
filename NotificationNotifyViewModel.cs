using Plugin.LocalNotifications;

private async void Confirm()
        {          
            IsBusy = true;
            ConfirmCommand.RaiseCanExecuteChanged();
    
            Application.Current.Properties["reservDT"] = Item.StartTime;
            await Application.Current.SavePropertiesAsync();
            GlobalVar.ReservDate = (DateTime)Application.Current.Properties["reservDT"];
    
            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Concert",
                Description = "Concert start after one hour",
                ReturningData = "Dummy Data", 
                Schedule =
                {
                    NotifyTime = GlobalVar.ReservDate.AddHours(-1) 
                }
            };
            await NotificationCenter.Current.Show(notification);

            bool ret = false;
            try
            {
                ret = await ReservationService.ConfirmReservation(Item);                         
            }
            catch {}

            if (ret)
            {
               
                await _Nav.PushModalAsync(new ReservationCompletedPage(this));                
            }
            else
            {
               
                await _Nav.PopAsync();
            }

            IsBusy = false;
        }
