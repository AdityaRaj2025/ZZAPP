namespace SSAPP.Models
{
    public class Reservation
    {
        public long ReservationID { get; set; }
        public int StoreCode { get; set; } = 1;
        public string StoreName { get; set; }
        public string MemberCode { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Today;
        public int Duration { get; set; }
        public DateTime EndTime { get; set; }
        public int GuestCount1 { get; set; }
        public int GuestCount2 { get; set; }
        public int GuestCount3 { get; set; }
        public int GuestCount4 { get; set; }
        public int RoomType { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomCourse { get; set; }
        public string RoomCourseName { get; set; }
        public int RoomNumber { get; set; }
        public bool Editable { get; set; } = true;
        public int Status { get; set; }
        public int VisitorID { get; set; }
        public DateTime CheckInReceptedTime { get; set; }
        public bool IsCompleted { get; set; }
     
        public bool AllowInAppReception { get; set; }

        public string Guest1CountDisp { get { return string.Format("{0}名様", GuestCount1); } }
        public string Guest2CountDisp { get { return string.Format("{0}名様", GuestCount2); } }
        public string Guest3CountDisp { get { return string.Format("{0}名様", GuestCount3); } }
        public string Guest4CountDisp { get { return string.Format("{0}名様", GuestCount4); } }
        public string TotalGuestCountDisp { get { return string.Format("{0}名様", GuestCount1 + GuestCount2 + GuestCount3 + GuestCount4); } }
        public string DurationString
        {
            get
            {
                if (Duration < 60)
                {
                    return $"{Duration}分";
                }
                else {
                    return $"{Duration / 60}時間{Duration % 60:00}分";
                }
            }
        }
        public string StartDateDisp { get { return StartTime.ToString("yyyy年MM月dd日"); } }
        public string StartTimeDisp { get { return StartTime.ToString("HH時mm分"); } }
        public string EndTimeDisp { get { return EndTime.ToString("HH時mm分"); } }
    }
}
