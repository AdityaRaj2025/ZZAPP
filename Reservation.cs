//MODEL PAGE
    public class Reservation
    {
        public long ReservationID { get; set; }
        public int StoreCode { get; set; } = 1;
        public string StoreName { get; set; }
        public string MemberCode { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Today;
        public int Duration { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomType { get; set; }
        public int Status { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomCourse { get; set; }
        public string RoomCourseName { get; set; }
        public int RoomNumber { get; set; }
        public bool Editable { get; set; } = true;     
        public int VisitorID { get; set; }
        public DateTime CheckInReceptedTime { get; set; }
        public bool IsCompleted { get; set; }
        public bool AllowInAppReception { get; set; }   
    }
