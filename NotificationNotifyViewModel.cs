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
                await Application.Current.MainPage.DisplayAlert("予約確定エラー", "恐れ入りますが、もう一度操作してください。", "確認");
                await _Nav.PopAsync();
            }

            IsBusy = false;
        }
