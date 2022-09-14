  public class RoomType
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int MinGuests { get; set; }
        public int MaxGuests { get; set; }
        public int RStatus { get; set; }  // RStatus added by raj
      
        public int[] RoomCourses { get; set; }
          
    }
