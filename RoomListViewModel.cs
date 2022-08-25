public class RoomListViewModel : BaseViewModel
    {
        public RoomListViewModel()
        {
            GetRoomList();
        }
        public void GetRoomList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<RoomData>));

            using (FileStream stream = File.OpenWrite("Room.xml"))
            {
                List<RoomData> list = new List<RoomData>();
                serializer.Serialize(stream, list);
            }

            using (FileStream stream = File.OpenRead("Room.xml"))
            {
                List<RoomData> dezerializedList = (List<RoomData>)serializer.Deserialize(stream);
               
            }
        }

    } 
